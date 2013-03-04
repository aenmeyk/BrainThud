define('vm.card', ['jquery', 'ko', 'card-manager', 'dom', 'editor', 'router', 'global', 'model', 'data-service', 'model.mapper'],
    function ($, ko, cardManager, dom, editor, router, global, model, dataService, modelMapper) {
        var
            card = ko.observable(new model.Card()),

            isValid = ko.computed(function () {
                return card().deckName() && card().question() && card().answer();
            }),

            activate = function (routeData) {
                if (routeData.cardId === 'batch') {
                    setupBatchEdit();
                } else {
                    setupSingleCardEdit(routeData);
                }
            },
            
            setupSingleCardEdit = function (routeData) {
                var found;

                dataService.card.getSingle({
                    params: {
                        userId: global.userId,
                        entityId: routeData.cardId
                    },
                    success: function (dto) {
                        found = modelMapper.card.mapResult(dto);

                        if (found) {
                            card(found);
                        } else {
                            card(new model.Card());
                        }

                        editor.refreshPreview('edit');
                    }
                });
            },
            
            setupBatchEdit = function () {
                card(new model.Card());
                
                var batchCards = _.filter(cardManager.cards(), function (item) {
                    return item.isCheckedForBatch();
                });

                if (batchCards && batchCards.length > 0) {
                    var differentValueText = '__________';
                    var firstCard = batchCards[0];
                    card().deckName(firstCard.deckName());
                    card().tags(firstCard.tags());
                    card().question(firstCard.question());
                    card().answer(firstCard.answer());

                    for (var i = 0; i < batchCards.length; i++) {
                        var currentCard = batchCards[i];
                        if (currentCard.deckName() !== card().deckName()) {
                            card().deckName(differentValueText);
                        }
                        if (currentCard.tags() !== card().tags()) {
                            card().tags(differentValueText);
                        }
                        if (currentCard.question() !== card().question()) {
                            card().question(differentValueText);
                        }
                        if (currentCard.answer() !== card().answer()) {
                            card().answer(differentValueText);
                        }
                    }

                    editor.refreshPreview('edit');
                }
            },

            updateAndCloseCommand = ko.asyncCommand({
                execute: function (complete) {
                    var item = ko.toJS(card);
                    dom.getCardValues(item, 'edit');
                    $.when(cardManager.updateCard(item))
                    .done(function() {
                         router.navigateTo(global.previousUrl);
                    })
                    .always(function() {
                        complete();
                    });
                },
                canExecute: function (isExecuting) {
                    return !isExecuting && isValid();
                }
            });

        return {
            activate: activate,
            card: card,
            updateAndCloseCommand: updateAndCloseCommand
        };
    }
);
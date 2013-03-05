define('vm.card', ['jquery', 'ko', 'card-manager', 'dom', 'editor', 'router', 'global', 'model', 'data-service', 'model.mapper'],
    function ($, ko, cardManager, dom, editor, router, global, model, dataService, modelMapper) {
        var
            batchCards,
            isBatch = false,
            card = ko.observable(new model.Card()),
            differentValueText = '********',

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
                isBatch = false;
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
                isBatch = true;
                card(new model.Card());
                
                batchCards = _.filter(cardManager.cards(), function (item) {
                    return item.isCheckedForBatch();
                });

                if (batchCards && batchCards.length > 0) {
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
                    var updateCommand;

                    if (isBatch) {
                        var cards = new Array();
                        var masterCard = card();
                        for (var i = 0; i < batchCards.length; i++) {
                            var batchCard = batchCards[i];
                            if (masterCard.deckName() !== differentValueText) {
                                batchCard.deckName(masterCard.deckName());
                            }
                            if (masterCard.tags() !== differentValueText) {
                                batchCard.tags(masterCard.tags());
                            }
                            if (masterCard.question() !== differentValueText) {
                                batchCard.question(masterCard.question());
                            }
                            if (masterCard.answer() !== differentValueText) {
                                batchCard.answer(masterCard.answer());
                            }

                            var jsCard = ko.toJS(batchCard);
                            cards.push(jsCard);
                        }
                        
                        updateCommand = cardManager.updateCardBatch(cards);
                    } else {
                        var item = ko.toJS(card);
                        dom.getCardValues(item, 'edit');
                        updateCommand = [cardManager.updateCard(item)];
                    }

                    $.when(updateCommand)
                    .done(function () {
                        router.navigateTo(global.previousUrl);
                    })
                    .always(function () {
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
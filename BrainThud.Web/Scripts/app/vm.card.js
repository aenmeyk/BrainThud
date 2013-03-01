define('vm.card', ['jquery', 'ko', 'card-manager', 'dom', 'editor', 'router', 'global', 'model', 'data-service', 'model.mapper'],
    function ($, ko, cardManager, dom, editor, router, global, model, dataService, modelMapper) {
        var
            card = ko.observable(new model.Card()),

            isValid = ko.computed(function () {
                return card().deckName() && card().question() && card().answer();
            }),

            activate = function (routeData) {
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
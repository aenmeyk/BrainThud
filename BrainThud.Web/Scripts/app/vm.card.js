define('vm.card', ['jquery', 'ko', 'card-manager', 'dom', 'editor', 'router', 'global', 'model'],
    function ($, ko, cardManager, dom, editor, router, global, model) {
        var
            card = ko.observable(new model.Card()),

            isValid = ko.computed(function () {
                return card().deckName() && card().question() && card().answer();
            }),

            activate = function (routeData) {
                var found = _.find(cardManager.cards(), function (item) {
                    return item.entityId() === parseInt(routeData.cardId);
                });

                if (found) {
                    card(found);
                } else {
                    card(new model.Card());
                }

                editor.refreshPreview('edit');
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
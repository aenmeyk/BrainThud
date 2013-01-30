define('vm.card', ['jquery', 'ko', 'data-context', 'dom', 'editor', 'router', 'global', 'model', 'amplify', 'config'],
    function ($, ko, dataContext, dom, editor, router, global, model, amplify, config) {
        var
            cardId = ko.observable(0),
            cards = ko.observableArray([]),
            card = ko.computed(function () {
                var found = _.find(cards(), function (item) {
                    return item.entityId() === parseInt(cardId());
                });

                return found ? found : new model.Card();
            }),

            init = function () {
                amplify.subscribe(config.pubs.cardCacheChanged, function (data) {
                    cards(data);
                });
            },

            isValid = ko.computed(function () {
                return card().deckName() && card().question() && card().answer();
            }),

            activate = function (routeData) {
                cardId(parseInt(routeData.cardId));
                editor.refreshPreview('edit');
            },

            updateAndCloseCommand = ko.asyncCommand({
                execute: function (complete) {
                    var item = ko.toJS(card);
                    dom.getCardValues(item, 'edit');
                    dataContext.quizCard.updateCachedItem(card());
                    $.when(dataContext.card.updateData({
                        data: item
                    }))
                    .done(function() {
                         router.navigateTo(global.previousUrl);
                    })
                    .always(function() {
                        complete();
                    });
                    return;
                },
                canExecute: function (isExecuting) {
                    return !isExecuting && card().dirtyFlag().isDirty() && isValid();
                }
            });

        init();

        return {
            activate: activate,
            card: card,
            updateAndCloseCommand: updateAndCloseCommand
        };
    }
);
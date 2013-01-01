define('vm.card', ['jquery', 'ko', 'data-context', 'toastr', 'dom', 'editor', 'router', 'global', 'model', 'amplify', 'config'],
    function ($, ko, dataContext, toastr, dom, editor, router, global, model, amplify, config) {
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

            activate = function (routeData) {
                cardId(parseInt(routeData.cardId));
                editor.refreshPreview('edit');
            },

            updateCard = function () {
                var item = ko.toJS(card);
                dom.getCardValues(item, 'edit');
                $.when(dataContext.card.updateData({
                    data: item
                }))
                .then(function () {
                    toastr.success('Success!');
                    router.navigateTo(global.previousUrl);
                });
            };

        init();

        return {
            activate: activate,
            card: card,
            updateCard: updateCard
        };
    }
);
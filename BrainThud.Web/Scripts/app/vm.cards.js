define('vm.cards', ['jquery', 'ko', 'data-context', 'router'],
    function ($, ko, dataContext, router) {
        var
            cards = ko.observableArray(),
            dataOptions = function() {
                return {
                    results: cards
                };
            },
            editCard = function (card) {
                router.navigateTo('#/cards/' + card.cardId() + '/edit');
            },
            activate = function(routeData) {
                dataContext.cards.getData(dataOptions());
            };

        return {
            cards: cards,
            editCard: editCard,
            activate: activate
        };
    }
);

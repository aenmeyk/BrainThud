define('vm.cards', ['jquery', 'ko', 'data-context', 'router'],
    function ($, ko, dataContext, router) {
        var
            cards = ko.observableArray([]),
            
            dataOptions = function() {
                return {
                    results: cards
                };
            },
            
            editCard = function (card) {
                router.navigateTo('#/cards/' + card.entityId() + '/edit');
            },

            flipCard = function (card) {
                card.questionSideVisible(!card.questionSideVisible());
            },

            activate = function (routeData) {
                dataContext.card.getData(dataOptions());
            };

        return {
            cards: cards,
            editCard: editCard,
            flipCard: flipCard,
            activate: activate
        };
    }
);

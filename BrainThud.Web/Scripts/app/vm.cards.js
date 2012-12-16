define('vm.cards', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var cards = ko.observableArray(),
            dataOptions = function() {
                return {
                    results: cards
                };
            },
            editCard = function(card) {
                window.alert(card.cardId());
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

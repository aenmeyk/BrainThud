define('vm.cards', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var cards = ko.observableArray(),
            dataOptions = function() {
                return {
                    results: cards
                };
            },

            activate = function() {
                dataContext.cards.getData(dataOptions());
            };
        
        return {
            activate: activate,
            cards: cards
        };
    }
);
define('vm.cards', ['jquery', 'ko', 'dataContext'],
    function ($, ko, dataContext) {
        var cards = ko.observableArray(),
            dataOptions = function() {
                return {
                    results: cards
                };
            };
        
        dataContext.cards.getData(dataOptions());
        
        return {
            cards: cards
        };
    }
);
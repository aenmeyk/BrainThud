define('vm.cards', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            cards = ko.observableArray(),
            currentCard = {
                question: ko.observable(''),
                answer: ko.observable('')
            },
        
            dataOptions = function () {
                return {
                    results: cards
                };
            },

            activate = function () {
                $.when(dataContext.cards.getData(dataOptions()))
                    .always(setCurrentCard);
            },

            setCurrentCard = function () {
                ko.utils.arrayFirst(cards(), function (item) {
                    currentCard.question(item.question);
                    currentCard.answer(item.answer);
                });
            };

        return {
            cards: cards,
            currentCard: currentCard,
            activate: activate
        };
    }
);
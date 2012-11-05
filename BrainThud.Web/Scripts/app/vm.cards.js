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

            activate = function (routeData) {
                $.when(dataContext.cards.getData(dataOptions()))
                    .always(setCurrentCard(routeData.rowKey));
            },

            setCurrentCard = function (rowKey) {
                for (var i = 0; i < cards().length; i++) {
                    if(cards()[i].rowKey() === rowKey) {
                        currentCard.question(cards()[i].question());
                        currentCard.answer(cards()[i].answer());
                        return;
                    }
                }
            };

        return {
            cards: cards,
            currentCard: currentCard,
            activate: activate
        };
    }
);

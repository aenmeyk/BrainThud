define('vm.cards', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            cards = ko.observableArray(),
            currentCard = {
                question: ko.observable(''),
                answer: ko.observable(''),
                text: ko.observable('')
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
                        currentCard.text(cards()[i].question());
                        return;
                    }
                }
            },

            flipCard = function () {
                if(currentCard.text() == currentCard.question()) {
                    currentCard.text(currentCard.answer());
                    $('#card').removeClass('btn-warning');
                    $('#card').addClass('btn-info');
                } else {
                    currentCard.text(currentCard.question());
                    $('#card').removeClass('btn-info');
                    $('#card').addClass('btn-warning');
                }
            };

        return {
            cards: cards,
            currentCard: currentCard,
            activate: activate,
            flipCard: flipCard
        };
    }
);

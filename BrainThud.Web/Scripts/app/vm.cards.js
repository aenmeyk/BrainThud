define('vm.cards', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            currentCardIndex = 0,
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
                    .always(setCurrentCard());
            },

            setCurrentCard = function () {
                currentCard.question(cards()[currentCardIndex].question());
                currentCard.answer(cards()[currentCardIndex].answer());
                currentCard.text(cards()[currentCardIndex].question());
            },
            
            showQuestion = function () {
                currentCard.text(currentCard.question());
                $('#card').removeClass('btn-info');
                $('#card').addClass('btn-warning');
            },
            
            showAnswer = function () {
                currentCard.text(currentCard.answer());
                $('#card').removeClass('btn-warning');
                $('#card').addClass('btn-info');
            },

            flipCard = function () {
                if (currentCard.text() == currentCard.question()) {
                    showAnswer();
                } else {
                    showQuestion();
                }
            },

            setNextCard = function () {
                if (currentCardIndex >= cards().length - 1) {
                    currentCardIndex = 0;
                } else {
                    currentCardIndex++;
                }
                
                setCurrentCard();
                showQuestion();
            };

        return {
            cards: cards,
            currentCard: currentCard,
            activate: activate,
            flipCard: flipCard,
            setNextCard: setNextCard
        };
    }
);

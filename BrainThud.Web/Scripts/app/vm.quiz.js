define('vm.quiz', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            questionSideVisible = ko.observable(true),
            nextCard = ko.observable(),
            cards = ko.observableArray([]),
            
            currentCard = {
                question: ko.observable(''),
                answer: ko.observable(''),
            },
            
            currentCardText = ko.computed(function () {
                var cardText = '';
                if (currentCard) {
                    if (questionSideVisible()) {
                        return currentCard.question();
                    } else {
                        return currentCard.answer();
                    }
                }

                return cardText;
            }),

            dataOptions = function () {
                return {
                    results: cards
                };
            },

            activate = function (routeData) {
                $.when(dataContext.quiz.getData(dataOptions()))
                    .then(function () { setCurrentCard(routeData.rowKey); });
            },

            setCurrentCard = function (rowKey) {
                for (var i = 0; i < cards().length; i++) {
                    if(cards()[i].rowKey() === rowKey) {
                        currentCard.question(cards()[i].question());
                        currentCard.answer(cards()[i].answer());

                        var nextCardIndex = i + 1;
                        if (nextCardIndex >= cards().length - 1) {
                            nextCardIndex = 0;
                        }

                        nextCard('#/quiz/' + cards()[nextCardIndex].rowKey());
                        questionSideVisible(true);
                        return;
                    }
                }
            },
            
            flipCard = function () {
                questionSideVisible(!questionSideVisible());
            };

        return {
            cards: cards,
            currentCard: currentCard,
            currentCardText: currentCardText,
            questionSideVisible: questionSideVisible,
            nextCard: nextCard,
            activate: activate,
            flipCard: flipCard
        };
    }
);

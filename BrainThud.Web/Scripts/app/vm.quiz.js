define('vm.quiz', ['jquery', 'ko', 'data-context', 'utils'],
    function ($, ko, dataContext, utils) {
        var
            init = function () {
                datePath = utils.getDatePath();
                
                // Certain browsers highlight the div when clicked.  Remove this highlight.
                var cardElement = $('#card');
                cardElement.click(function () {
                    cardElement.blur();
                });
            },
            datePath,
            questionSideVisible = ko.observable(true),
            quiz = ko.observableArray([]),
            nextCard = ko.observable(),
            previousCard = ko.observable(),
            cards = ko.observableArray([]),

            currentCard = {
                rowKey: ko.observable(''),
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
                    results: quiz,
                    params: {
                        datePath: datePath
                    }
                };
            },

            activate = function (routeData) {
                $.when(dataContext.quiz.getData(dataOptions()))
                    .then(function () {
                        cards(quiz()[0].cards);
                        setCurrentCard(routeData.rowKey);
                    });
            },

            setCurrentCard = function (rowKey) {
                for (var i = 0; i < cards().length; i++) {
                    if (cards()[i].rowKey() === rowKey) {
                        currentCard.rowKey(cards()[i].rowKey());
                        currentCard.question(cards()[i].question());
                        currentCard.answer(cards()[i].answer());

                        var nextCardIndex = i + 1;
                        if (nextCardIndex >= cards().length - 1) {
                            nextCardIndex = 0;
                        }

                        nextCard(getCardUri(nextCardIndex));

                        var previousCardIndex = i - 1;
                        if (previousCardIndex < 0) {
                            previousCardIndex = cards().length - 1;
                        }

                        previousCard(getCardUri(previousCardIndex));
                        
                        questionSideVisible(true);
                        return;
                    }
                }
            },

            flipCard = function () {
                questionSideVisible(!questionSideVisible());
            },
            
            getCardUri = function(cardIndex) {
                return '#/quizzes/' + datePath + '/' + cards()[cardIndex].rowKey();
            },
            
            getQuizResultConfig = function(isCorrect) {
                return {
                    data: {
                        cardId: currentCard.rowKey(),
                        isCorrect: isCorrect
                    },
                    params: {
                        datePath: datePath
                    }
                };
            },

            showNextCard = function () {
                window.location.href = nextCard();
            },

            showPreviousCard = function () {
                window.location.href = previousCard();
            },

            submitCorrect = function () {
                dataContext.quizResult.saveData(getQuizResultConfig(true));
                showNextCard();
            },

            submitIncorrect = function () {
                dataContext.quizResult.saveData(getQuizResultConfig(false));
                showNextCard();
            };

        init();

        return {
            cards: cards,
            currentCard: currentCard,
            currentCardText: currentCardText,
            questionSideVisible: questionSideVisible,
            activate: activate,
            showNextCard: showNextCard,
            showPreviousCard: showPreviousCard,
            submitCorrect: submitCorrect,
            submitIncorrect: submitIncorrect,
            flipCard: flipCard
        };
    }
);

define('vm.quiz', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            init = function () {
                var today = new Date(),
                    year = today.getFullYear(),
                    month = today.getMonth() + 1,
                    day = today.getDate();

                datePath = year + '/' + month + '/' + day;
                
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

                        nextCard('#/quizzes/' + datePath + '/' + cards()[nextCardIndex].rowKey());

                        var previousCardIndex = i - 1;
                        if (previousCardIndex < 0) {
                            previousCardIndex = cards().length - 1;
                        }

                        previousCard('#/quizzes/' + datePath + '/' + cards()[previousCardIndex].rowKey());
                        
                        questionSideVisible(true);
                        return;
                    }
                }
            },

            flipCard = function () {
                questionSideVisible(!questionSideVisible());
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

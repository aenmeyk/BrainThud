define('vm.quiz', ['jquery', 'ko', 'data-context'],
    function ($, ko, dataContext) {
        var
            init = function () {
                var today = new Date(),
                    year = today.getFullYear(),
                    month = today.getMonth() + 1,
                    day = today.getDate();

                datePath = year + '/' + month + '/' + day;
            },
            datePath,
            questionSideVisible = ko.observable(true),
            quiz = ko.observableArray([]),
            nextCard = ko.observable(),
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

            submitCorrect = function () {
                dataContext.quizResult.saveData(getQuizResultConfig(true));
                window.location.href = nextCard();
            },

            submitIncorrect = function () {
                dataContext.quizResult.saveData(getQuizResultConfig(false));
                window.location.href = nextCard();
            };

        init();

        return {
            cards: cards,
            currentCard: currentCard,
            currentCardText: currentCardText,
            questionSideVisible: questionSideVisible,
            nextCard: nextCard,
            activate: activate,
            submitCorrect: submitCorrect,
            submitIncorrect: submitIncorrect,
            flipCard: flipCard
        };
    }
);

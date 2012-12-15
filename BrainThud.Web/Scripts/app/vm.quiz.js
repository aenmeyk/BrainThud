define('vm.quiz', ['jquery', 'ko', 'data-context', 'utils', 'markdown'],
    function ($, ko, dataContext, utils, markdown) {
        var
            init = function () {
                datePath = utils.getDatePath();
                markDownConverter = markdown.getSanitizingConverter();
                // Certain browsers highlight the div when clicked.  Remove this highlight.
                var cardElement = $('#card');
                cardElement.click(function () {
                    cardElement.blur();
                });
            },
            datePath,
            markDownConverter,
            questionSideVisible = ko.observable(true),
            quiz = ko.observableArray([]),
            nextCard = ko.observable(),
            previousCard = ko.observable(),
            cards = ko.observableArray([]),
            userId = ko.observable(),

            currentCard = {
                cardId: ko.observable(''),
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
                        datePath: datePath,
                        userId: userId()
                    }
                };
            },

            activate = function (routeData) {
                $.when(dataContext.quiz.getData(dataOptions()))
                    .then(function () {
                        cards(quiz()[0].cards);
                        userId(quiz()[0].userId);
                        setCurrentCard(routeData.cardId);
                    });
            },

            setCurrentCard = function (cardId) {
                for (var i = 0; i < cards().length; i++) {
                    if (cards()[i].cardId() === parseInt(cardId)) {
                        currentCard.cardId(cards()[i].cardId());
                        
                        var questionText = cards()[i].question();
                        var sanitizedQuestion = markDownConverter.makeHtml(questionText);
                        currentCard.question(sanitizedQuestion);
                        
                        var answerText = cards()[i].answer();
                        var sanitizedAnswer = markDownConverter.makeHtml(answerText);
                        currentCard.answer(sanitizedAnswer);

                        var nextCardIndex = i + 1;
                        if (nextCardIndex >= cards().length) {
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
                return '#/quizzes/' + userId() + '/' + datePath + '/' + cards()[cardIndex].cardId();
            },
            
            getQuizResultConfig = function(isCorrect) {
                return {
                    data: {
                        cardId: currentCard.cardId(),
                        isCorrect: isCorrect
                    },
                    params: {
                        datePath: datePath,
                        userId: userId()
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
            userId: userId,
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

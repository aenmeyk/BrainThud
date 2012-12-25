define('vm.quiz-card', ['jquery', 'ko', 'data-context', 'utils', 'router'],
    function ($, ko, dataContext, utils, router) {
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
            nextCardIndex = ko.observable(),
            previousCardIndex = ko.observable(),
            questionSideVisible = ko.observable(true),
            quiz = ko.observableArray([]),
            nextCard = ko.observable(),
            previousCard = ko.observable(),
            cards = ko.observableArray([]),
            userId = ko.observable(),
            hasNextCard = ko.computed(function () {
                return nextCardIndex() <= cards().length - 1;
            }),
            hasPreviousCard = ko.computed(function () {
                return previousCardIndex() >= 0;
            }),

            currentCard = {
                cardId: ko.observable(''),
                question: ko.observable(''),
                answer: ko.observable(''),
                deckName: ko.observable(''),
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

            dataOptions = {
                results: quiz,
                params: {
                    datePath: datePath,
                    userId: userId()
                }
            },

            activate = function (routeData) {
                // TODO: The activate method fires for each navigation to a new card.  I think we need a vm for each card since each card has it's own route.
                if (cards().length === 0) {
                    var existingCards = ko.observableArray([]);

                    // TODO: Find a better way of ensuring that the ViewModels use the same store for cards. 
                    // (We need the same store because if a card is updated on one ViewModel we need the value to be updated on the other ViewModels too.)
                    $.when(dataContext.quizCard.getData(dataOptions), dataContext.card.getData({ results: existingCards }))
                        .then(function () {
                            var quizCards = quiz()[0].cards;
                            cards = ko.observableArray([]);
                            for (var i = 0; i < quizCards.length; i++) {
                                for (var j = 0; j < existingCards().length; j++) {
                                    if (quizCards[i].partitionKey() == existingCards()[j].partitionKey() && quizCards[i].rowKey() == existingCards()[j].rowKey()) {
                                        cards().push(existingCards()[j]);
                                        break;
                                    }
                                }
                            }

                            userId(quiz()[0].userId);
                        });
                }
                setCurrentCard(routeData.cardId);
            },

            setCurrentCard = function (cardId) {
                for (var i = 0; i < cards().length; i++) {
                    var card = cards()[i];
                    if (card.cardId() === parseInt(cardId)) {
                        currentCard.cardId(card.cardId());

                        var questionText = card.questionHtml();
                        currentCard.question(questionText);

                        var answerText = card.answerHtml();
                        currentCard.answer(answerText);
                        currentCard.deckName(card.deckName);

                        nextCardIndex(i + 1);
                        if (hasNextCard()) {
                            nextCard(getCardUri(nextCardIndex()));
                        }

                        previousCardIndex(i - 1);
                        if (hasPreviousCard()) {
                            previousCard(getCardUri(previousCardIndex()));
                        }

                        questionSideVisible(true);
                        return;
                    }
                }
            },

            flipCard = function () {
                questionSideVisible(!questionSideVisible());
            },

            getCardUri = function (cardIndex) {
                return '#/quizzes/' + userId() + '/' + datePath + '/' + cards()[cardIndex].cardId();
            },

            getQuizResultConfig = function (isCorrect) {
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
                router.navigateTo(nextCard());
            },

            showPreviousCard = function () {
                router.navigateTo(previousCard());
            },

            submitCorrect = function () {
                dataContext.quizResult.createData(getQuizResultConfig(true));
                showNextCard();
            },

            submitIncorrect = function () {
                dataContext.quizResult.createData(getQuizResultConfig(false));
                showNextCard();
            };

        editCard = function () {
            router.navigateTo('#/cards/' + currentCard.cardId() + '/edit');
        },

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
            editCard: editCard,
            flipCard: flipCard,
            hasNextCard: hasNextCard,
            hasPreviousCard: hasPreviousCard
        };
    }
);

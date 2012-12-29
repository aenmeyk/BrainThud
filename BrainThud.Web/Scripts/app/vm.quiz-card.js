define('vm.quiz-card', ['jquery', 'ko', 'data-context', 'utils', 'router', 'amplify', 'config', 'model'],
    function ($, ko, dataContext, utils, router, amplify, config, model) {
        var
            init = function () {
                datePath = utils.getDatePath();
                // Certain browsers highlight the div when clicked.  Remove this highlight.
                var cardElement = $('#card');
                cardElement.click(function () {
                    cardElement.blur();
                });
                
                amplify.subscribe(config.pubs.updateCard, function (data) {
                    var cardsArray = cards();
                    for (var i = 0; i < cardsArray.length; i++) {
                        if (cardsArray[i].entityId() === data.entityId()) {
                            cardsArray.splice(i, 1);
                            break;
                        }
                    }

                    cardsArray.push(data);
                });
            },
            datePath,
            nextCardIndex = ko.observable(),
            previousCardIndex = ko.observable(),
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

            currentCard = ko.observable(new model.Card()),

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

                    // TODO: Find a better way of ensuring that the ViewModels use the same store for cards. Perhaps use pub/sub like the QuizResults
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
                    if (card.entityId() === parseInt(cardId)) {
                        currentCard(card);
                        currentCard().questionSideVisible(true);

                        nextCardIndex(i + 1);
                        if (hasNextCard()) {
                            nextCard(getCardUri(nextCardIndex()));
                        }

                        previousCardIndex(i - 1);
                        if (hasPreviousCard()) {
                            previousCard(getCardUri(previousCardIndex()));
                        }

                        return;
                    }
                }
            },

            flipCard = function () {
                currentCard().questionSideVisible(!currentCard().questionSideVisible());
            },

            getCardUri = function (cardIndex) {
                return '#/quizzes/' + userId() + '/' + datePath + '/' + cards()[cardIndex].entityId();
            },

            getQuizResultConfig = function (isCorrect) {
                return {
                    data: {
                        cardId: currentCard().entityId(),
                        isCorrect: isCorrect
                    },
                    params: {
                        datePath: datePath,
                        userId: userId()
                    }
                };
            },

            showNextCard = function () {
                if (hasNextCard()) {
                    router.navigateTo(nextCard());
                } else {
                    router.navigateTo('#/quizzes/32/2012/12/27');
                }
            },

            showPreviousCard = function () {
                if (hasPreviousCard()) {
                    router.navigateTo(previousCard());
                } else {
                    router.navigateTo('#/quizzes/32/2012/12/27');
                }
            },
            
            publishQuizResult = function(isCorrect) {
                amplify.publish(config.pubs.createQuizResult, {
                    cardId: currentCard().entityId(),
                    isCorrect: isCorrect
                });
            },

            submitCorrect = function () {
                dataContext.quizResult.createData(getQuizResultConfig(true));
                publishQuizResult(true);
                showNextCard();
            },

            submitIncorrect = function () {
                dataContext.quizResult.createData(getQuizResultConfig(false));
                publishQuizResult(false);
                showNextCard();
            };

        editCard = function () {
            router.navigateTo('#/cards/' + currentCard().entityId() + '/edit');
        },

    init();

        return {
            userId: userId,
            cards: cards,
            currentCard: currentCard,
            activate: activate,
            showNextCard: showNextCard,
            showPreviousCard: showPreviousCard,
            submitCorrect: submitCorrect,
            submitIncorrect: submitIncorrect,
            editCard: editCard,
            flipCard: flipCard
        };
    }
);

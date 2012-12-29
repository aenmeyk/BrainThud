define('vm.quiz-card', ['jquery', 'ko', 'data-context', 'utils', 'router', 'amplify', 'config', 'model'],
    function ($, ko, dataContext, utils, router, amplify, config, model) {
        var
            init = function () {
                datePath = utils.getDatePath();
                
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
                
                amplify.subscribe(config.pubs.deleteCard, function () {
                    cards([]);
                });
            },
            datePath,
            currentRouteData,
            quiz = ko.observableArray([]),
            nextCard = ko.observable(),
            previousCard = ko.observable(),
            cards = ko.observableArray([]),
            userId = ko.observable(),
            currentCard = ko.observable(new model.Card()),
            currentCardIndex = ko.observable(0),
            cardCount = ko.computed(function () {
                return cards().length;
            }),
            displayIndex = ko.computed(function () {
                return currentCardIndex() + 1;
            }),
            nextCardIndex = ko.computed(function () {
                return currentCardIndex() + 1;
            }),
            previousCardIndex = ko.computed(function () {
                return currentCardIndex() - 1;
            }),
            hasNextCard = ko.computed(function () {
                return nextCardIndex() <= cards().length - 1;
            }),
            hasPreviousCard = ko.computed(function () {
                return previousCardIndex() >= 0;
            }),


            dataOptions = {
                results: quiz,
                params: {
                    datePath: datePath,
                    userId: userId()
                }
            },

            activate = function (routeData) {
                currentRouteData = routeData;
                // TODO: The activate method fires for each navigation to a new card.  I think we need a vm for each card since each card has it's own route.
                if (cards().length === 0) {
                    var existingCards = ko.observableArray([]);

                    // TODO: Find a better way of ensuring that the ViewModels use the same store for cards. Perhaps use pub/sub like the QuizResults
                    // (We need the same store because if a card is updated on one ViewModel we need the value to be updated on the other ViewModels too.)
                    $.when(dataContext.quizCard.getData(dataOptions), dataContext.card.getData({ results: existingCards }))
                        .then(function () {
                            var quizCards = quiz()[0].cards;
                            cards([]);
                            for (var i = 0; i < quizCards.length; i++) {
                                for (var j = 0; j < existingCards().length; j++) {
                                    if (quizCards[i].partitionKey() == existingCards()[j].partitionKey() && quizCards[i].rowKey() == existingCards()[j].rowKey()) {
                                        cards.push(existingCards()[j]);
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
                        currentCardIndex(i);
                        
                        if (hasNextCard()) {
                            nextCard(getCardUri(nextCardIndex()));
                        }

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
            
            deleteCard = function () {
                $.when(dataContext.card.deleteData({
                    params: {
                        userId: userId(),
                        partitionKey: currentCard().partitionKey(),
                        rowKey: currentCard().rowKey(),
                        entityId: currentCard().entityId()
                    }
                })).then(function () {
                    $("#quiz-card-view .deleteDialog").modal('hide');
                    showNextCard();
                    amplify.publish(config.pubs.deleteCard);
                    cards([]);
                    activate(currentRouteData);
                });
            },

            showDeleteDialog = function () {
                $("#quiz-card-view .deleteDialog").modal('show');
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
            displayIndex: displayIndex,
            cardCount: cardCount,
            activate: activate,
            showNextCard: showNextCard,
            showPreviousCard: showPreviousCard,
            submitCorrect: submitCorrect,
            submitIncorrect: submitIncorrect,
            editCard: editCard,
            flipCard: flipCard,
            showDeleteDialog: showDeleteDialog,
            deleteCard: deleteCard
        };
    }
);

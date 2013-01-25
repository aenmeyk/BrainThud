define('vm.quiz-card', ['underscore', 'ko', 'data-context', 'utils', 'amplify', 'config', 'global', 'quiz-navigator'],
    function (_, ko, dataContext, utils, amplify, config, global, quizNavigator) {
        var
            quizResults = ko.observableArray([]),

            displayIndex = ko.computed(function () {
                return quizNavigator.cardIndex() + 1;
            }),

            cardCount = ko.computed(function () {
                return quizNavigator.cardCount();
            }),
            
            activate = function (routeData) {
                if (!quizNavigator.isActivated) {
                    quizNavigator.activate(routeData);
                }
                dataContext.quizResult.getData({
                    results: quizResults,
                    params: {
                        datePath: utils.getDatePath(),
                        userId: global.userId
                    }
                });
            },
            
            card = ko.computed(function() {
                return quizNavigator.currentCard();
            }),

            getCreateConfig = function (isCorrect) {
                return {
                    data: {
                        cardId: card().entityId(),
                        isCorrect: isCorrect
                    },
                    params: {
                        datePath: utils.getDatePath(),
                        userId: global.userId
                    }
                };
            },

            getUpdateConfig = function (quizResult) {
                return {
                    data: quizResult,
                    params: {
                        datePath: utils.getDatePath(),
                        userId: global.userId
                    }
                };
            },
//
//            getUpdateConfig = function (quizResult) {
//                return {
//                    data: {
//                        cardId: card().entityId(),
//                        quizResult: quizResult
//                    },
//                    params: {
//                        datePath: utils.getDatePath(),
//                        userId: global.userId
//                    }
//                };
//            },

            submitQuizResult = function (isCorrect) {
                var currentCard = card();
                
                var existingQuizResult = _.find(quizResults(), function (item) {
                    return item.cardId() === currentCard.entityId();
                });

                if (existingQuizResult) {
                    var jsQuizResult = ko.toJS(existingQuizResult);
                    jsQuizResult.isCorrect = isCorrect;
                    dataContext.quizResult.updateData(getUpdateConfig(jsQuizResult));
                } else {
                    dataContext.quizResult.createData(getCreateConfig(isCorrect));
                }

                amplify.publish(config.pubs.createQuizResult, {
                    cardId: card().entityId(),
                    isCorrect: isCorrect
                });
                
                quizNavigator.showNextCard();
            },

            flipCard = function () {
                card().questionSideVisible(!card().questionSideVisible());
            },

            submitCorrect = function () {
                submitQuizResult(true);
            },

            submitIncorrect = function () {
                submitQuizResult(false);
            },

            editCard = function () {
                amplify.publish(config.pubs.showEditCard, card().entityId());
            },

            showDeleteDialog = function () {
                amplify.publish(config.pubs.showDeleteCard, card(), quizNavigator.showNextCard);
            },

            showCardInfoDialog = function () {
                amplify.publish(config.pubs.showCardInfo, card());
            };

        return {
            activate: activate,
            card: card,
            showNextCard: quizNavigator.showNextCard,
            showPreviousCard: quizNavigator.showPreviousCard,
            flipCard: flipCard,
            submitCorrect: submitCorrect,
            submitIncorrect: submitIncorrect,
            editCard: editCard,
            showDeleteDialog: showDeleteDialog,
            showCardInfoDialog: showCardInfoDialog,
            displayIndex: displayIndex,
            cardCount: cardCount
        };
    }
);

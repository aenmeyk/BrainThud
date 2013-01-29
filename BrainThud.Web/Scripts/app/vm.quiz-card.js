define('vm.quiz-card', ['underscore', 'ko', 'data-context', 'utils', 'amplify', 'config', 'global', 'quiz-navigator', 'moment', 'data-service', 'model.mapper'],
    function (_, ko, dataContext, utils, amplify, config, global, quizNavigator, moment, dataService, modelMapper) {
        var
            displayIndex = ko.computed(function () {
                return quizNavigator.cardIndex() + 1;
            }),
            
            activate = function (routeData) {
                quizNavigator.activate(routeData);
            },

            getCreateConfig = function (isCorrect) {
                return {
                    data: {
                        cardId: quizNavigator.currentCard().entityId(),
                        isCorrect: isCorrect
                    },
                    params: {
                        datePath: quizNavigator.quizDatePath(),
                        userId: global.userId
                    }
                };
            },

            getUpdateConfig = function (quizResult) {
                return {
                    data: quizResult,
                    params: {
                        datePath: quizNavigator.quizDatePath(),
                        userId: global.userId
                    }
                };
            },

            getCard = function (entityId) {
                dataService.card.getSingle({
                    params: {
                        userId: global.userId,
                        entityId: entityId
                    },
                    success: function(dto) {
                        var result = modelMapper.card.mapResult(dto);
                        dataContext.quizCard.updateCachedItem(result);
                        dataContext.card.updateCachedItem(result);
                    }
                });
            },

            submitQuizResult = function (isCorrect) {
                var currentCard = quizNavigator.currentCard(),
                    deferredSave;
                
                var existingQuizResult = _.find(quizNavigator.quizResults(), function (item) {
                    return item.cardId() === currentCard.entityId();
                });

                if (existingQuizResult) {
                    var jsQuizResult = ko.toJS(existingQuizResult);
                    jsQuizResult.isCorrect = isCorrect;
                    deferredSave = dataContext.quizResult.updateData(getUpdateConfig(jsQuizResult));
                } else {
                    deferredSave = dataContext.quizResult.createData(getCreateConfig(isCorrect));
                }

                $.when(deferredSave)
                    .done(function() {
                        getCard(currentCard.entityId());
                    });

                amplify.publish(config.pubs.createQuizResult, {
                    cardId: currentCard.entityId(),
                    isCorrect: isCorrect
                });
                
                quizNavigator.showNextCard();
            },

            flipCard = function () {
                quizNavigator.currentCard().questionSideVisible(!card().questionSideVisible());
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
            card: quizNavigator.currentCard,
            showNextCard: quizNavigator.showNextCard,
            showPreviousCard: quizNavigator.showPreviousCard,
            flipCard: flipCard,
            submitCorrect: submitCorrect,
            submitIncorrect: submitIncorrect,
            editCard: editCard,
            showDeleteDialog: showDeleteDialog,
            showCardInfoDialog: showCardInfoDialog,
            displayIndex: displayIndex,
            cardCount: quizNavigator.cardCount
        };
    }
);

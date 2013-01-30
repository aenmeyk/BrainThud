define('vm.quiz-card', ['underscore', 'ko', 'data-context', 'amplify', 'config', 'global', 'quiz-navigator', 'data-service', 'model.mapper'],
    function (_, ko, dataContext, amplify, config, global, quizNavigator, dataService, modelMapper) {
        var
            displayIndex = ko.computed(function () {
                return quizNavigator.cardIndex() + 1;
            }),
            
            borderCss = ko.computed(function () {
                var card = quizNavigator.currentCard();
                
                if (!card.questionSideVisible()) return 'answer';

                if (quizNavigator.currentQuizResult()) {
                    return quizNavigator.currentQuizResult().isCorrect() ? 'correct' : 'incorrect';
                }

                return 'question';
            }),

            isSubmitResultDisabled = ko.computed(function () {
                return !quizNavigator.isQuizToday();
            }),

            activate = function (routeData) {
                if (!quizNavigator.isActivated()) {
                    quizNavigator.activate(routeData);
                }
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
                    existingQuizResult = quizNavigator.currentQuizResult(),
                    deferredSave;

                if (existingQuizResult) {
                    var jsQuizResult = ko.toJS(existingQuizResult);
                    jsQuizResult.isCorrect = isCorrect;
                    deferredSave = dataContext.quizResult.updateData(getUpdateConfig(jsQuizResult));
                } else {
                    deferredSave = dataContext.quizResult.createData(getCreateConfig(isCorrect));
                }

                $.when(deferredSave)
                    .done(function () {
                        getCard(currentCard.entityId());
                    });

                amplify.publish(config.pubs.createQuizResult, {
                    cardId: currentCard.entityId(),
                    isCorrect: isCorrect
                });
                
                quizNavigator.showNextCard();
            },

            flipCard = function () {
                var card = quizNavigator.currentCard();
                card.questionSideVisible(!card.questionSideVisible());
            },

            submitCorrect = function () {
                submitQuizResult(true);
            },

            submitIncorrect = function () {
                submitQuizResult(false);
            },

            editCard = function () {
                amplify.publish(config.pubs.showEditCard, quizNavigator.currentCard().entityId());
            },

            showDeleteDialog = function () {
                amplify.publish(config.pubs.showDeleteCard, quizNavigator.currentCard(), quizNavigator.showNextCard);
            },

            showCardInfoDialog = function () {
                amplify.publish(config.pubs.showCardInfo, quizNavigator.currentCard());
            };

        return {
            activate: activate,
            card: quizNavigator.currentCard,
            isSubmitResultDisabled: isSubmitResultDisabled,
            showNextCard: quizNavigator.showNextCard,
            showPreviousCard: quizNavigator.showPreviousCard,
            flipCard: flipCard,
            submitCorrect: submitCorrect,
            submitIncorrect: submitIncorrect,
            editCard: editCard,
            showDeleteDialog: showDeleteDialog,
            showCardInfoDialog: showCardInfoDialog,
            displayIndex: displayIndex,
            cardCount: quizNavigator.cardCount,
            borderCss: borderCss
        };
    }
);

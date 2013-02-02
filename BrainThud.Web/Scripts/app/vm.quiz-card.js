define('vm.quiz-card', ['underscore', 'ko', 'data-context', 'amplify', 'config', 'global', 'quiz-navigator', 'data-service', 'model.mapper', 'router', 'card-manager', 'moment'],
    function (_, ko, dataContext, amplify, config, global, quizNavigator, dataService, modelMapper, router, cardManager, moment) {
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
                if (routeData.year !== quizNavigator.quizYear() 
                    || routeData.month !== quizNavigator.quizMonth() 
                    || routeData.day !== quizNavigator.quizDay()) {
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

            submitQuizResult = function (isCorrect) {
                var existingQuizResult = quizNavigator.currentQuizResult(),
                    deferredSave;

                if (existingQuizResult) {
                    var jsQuizResult = ko.toJS(existingQuizResult);
                    jsQuizResult.isCorrect = isCorrect;
                    deferredSave = dataContext.quizResult.updateData(getUpdateConfig(jsQuizResult));
                } else {
                    deferredSave = dataContext.quizResult.createData(getCreateConfig(isCorrect));
                }

                $.when(deferredSave)
                .done(function (quizResult) {
                    cardManager.applyQuizResult(quizResult);
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
                router.navigateTo(global.routePrefix + 'cards/' + quizNavigator.currentCard().entityId() + '/edit');
            },

            showDeleteDialog = function () {
                cardManager.deleteCard(quizNavigator.currentCard());
            },

            showCardInfoDialog = function () {
                cardManager.showCardInfo(quizNavigator.currentCard());
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
            cardCount: cardManager.quizCardCount,
            borderCss: borderCss
        };
    }
);

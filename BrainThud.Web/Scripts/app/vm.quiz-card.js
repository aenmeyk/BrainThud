define('vm.quiz-card', ['ko', 'data-context', 'global', 'quiz-navigator', 'router', 'card-manager'],
    function (ko, dataContext, global, quizNavigator, router, cardManager) {
        var
            isExecuting = ko.observable(false),
            displayIndex = ko.computed(function () {
                return quizNavigator.cardOrderIndex() + 1;
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
                    || routeData.day !== quizNavigator.quizDay()
                    || parseInt(routeData.cardId) !== quizNavigator.currentCard().entityId()) {
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
                var existingQuizResult = quizNavigator.currentQuizResult();

                if (existingQuizResult) {
                    var jsQuizResult = ko.toJS(existingQuizResult);
                    jsQuizResult.isCorrect = isCorrect;
                    return dataContext.quizResult.updateData(getUpdateConfig(jsQuizResult));
                } else {
                    return dataContext.quizResult.createData(getCreateConfig(isCorrect));
                }
            },

            flipCard = function () {
                var card = quizNavigator.currentCard();
                card.questionSideVisible(!card.questionSideVisible());
            },

            editCard = function () {
                router.navigateTo(global.routePrefix + 'cards/' + quizNavigator.currentCard().entityId() + '/edit');
            },

            showDeleteDialog = function () {
                cardManager.deleteCard(quizNavigator.currentCard(), function() {
                    quizNavigator.removeCurrentCardIndex();
                });
            },

            showCardInfoDialog = function () {
                cardManager.showCardInfo(quizNavigator.currentCard());
            },

            getQuizResultCommandConfig = function (isCorrect) {
                return {
                    execute: function (complete) {
                        isExecuting(true);
                        $.when(submitQuizResult(isCorrect))
                            .done(function (quizResult) {
                                cardManager.applyQuizResult(quizResult);
                                quizNavigator.showNextCard();
                            })
                            .always(function () {
                                complete();
                                isExecuting(false);
                            });
                    },
                    canExecute: function () {
                        return !isExecuting() && !isSubmitResultDisabled();
                    }
                };
            },

            correctCommand = ko.asyncCommand(getQuizResultCommandConfig(true)),

            incorrectCommand = ko.asyncCommand(getQuizResultCommandConfig(false));

        return {
            activate: activate,
            card: quizNavigator.currentCard,
            showNextCard: quizNavigator.showNextCard,
            correctCommand: correctCommand,
            incorrectCommand: incorrectCommand,
            showPreviousCard: quizNavigator.showPreviousCard,
            flipCard: flipCard,
            editCard: editCard,
            showDeleteDialog: showDeleteDialog,
            showCardInfoDialog: showCardInfoDialog,
            displayIndex: displayIndex,
            cardCount: cardManager.quizCardCount,
            borderCss: borderCss
        };
    }
);

define('vm.quiz-card', ['ko', 'data-context', 'utils', 'amplify', 'config', 'global', 'quiz-navigator'],
    function (ko, dataContext, utils, amplify, config, global, quizNavigator) {
        var
            displayIndex = ko.computed(function () {
                return quizNavigator.cardIndex() + 1;
            }),

            cardCount = ko.computed(function () {
                return quizNavigator.cardCount();
            }),
            
            activate = function () { },
            
            card = ko.computed(function() {
                return quizNavigator.currentCard();
            }),

            getQuizResultConfig = function (isCorrect) {
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

            publishQuizResult = function (isCorrect) {
                amplify.publish(config.pubs.createQuizResult, {
                    cardId: card().entityId(),
                    isCorrect: isCorrect
                });
            },

            flipCard = function () {
                card().questionSideVisible(!card().questionSideVisible());
            },

            submitCorrect = function () {
                dataContext.quizResult.createData(getQuizResultConfig(true));
                publishQuizResult(true);
                quizNavigator.showNextCard();
            },

            submitIncorrect = function () {
                dataContext.quizResult.createData(getQuizResultConfig(false));
                publishQuizResult(false);
                quizNavigator.showNextCard();
            },

            editCard = function () {
                amplify.publish(config.pubs.showEditCard, card().entityId());
            },

            showDeleteDialog = function () {
                amplify.publish(config.pubs.showDeleteCard, card());
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
            displayIndex: displayIndex,
            cardCount: cardCount
        };
    }
);

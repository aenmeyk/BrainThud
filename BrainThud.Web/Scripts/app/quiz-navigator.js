define('quiz-navigator', ['ko', 'router', 'data-context', 'utils', 'amplify', 'config'],
    function (ko, router, dataContext, utils, amplify, config) {
        var
            cardIds,
            cardIndex = 0,
            quizzes = ko.observableArray([]),

            init = function () {
                quizzes = ko.observableArray([]);
                $.when(dataContext.quiz.getData(dataOptions()))
                    .then(function () {
                        var quiz = quizzes()[0];
                        cardIds = quiz.cardIds;
                    });
            },

            dataOptions = function () {
                return {
                    results: quizzes,
                    params: {
                        datePath: utils.getDatePath(),
                        userId: global.userId
                    }
                };
            },

            getQuizPath = function () {
                return '#/quizzes/' + global.userId + '/' + utils.getDatePath();
            },

            getCardUri = function () {
                return getQuizPath() + '/' + cardIds[cardIndex];
            },

            showNextCard = function () {
                if (cardIndex < cardIds.length - 1) {
                    cardIndex++;
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },

            showPreviousCard = function () {
                if (cardIndex > 0) {
                    cardIndex--;
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },
            
            showQuizSummary = function() {
                cardIndex = 0;
                router.navigateTo(getQuizPath());
            };

        amplify.subscribe(config.pubs.showNextCard, function () {
            showNextCard();
        });

        amplify.subscribe(config.pubs.showPreviousCard, function () {
            showPreviousCard();
        });

        return {
            init: init
        };
    }
);
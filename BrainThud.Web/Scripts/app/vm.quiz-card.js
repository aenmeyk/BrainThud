define('vm.quiz-card', ['jquery', 'underscore', 'ko', 'data-context', 'utils', 'router', 'amplify', 'config', 'model', 'global'],
    function ($, _, ko, dataContext, utils, router, amplify, config, model, global) {
        var
            cards = ko.observableArray([]),
            cardCount = ko.observable(0),
            card = ko.observable(new model.Card()),

            activate = function (routeData) {
                var quizzes = ko.observableArray([]),
                    options = {
                    results: quizzes,
                    params: {
                        datePath: utils.getDatePath(),
                        userId: global.userId
                    }
                };
                
                $.when(dataContext.card.getData({ results: cards }), dataContext.quiz.getData(options))
                    .then(function () {
                        var matchingCard = _.find(cards(), function (item) {
                            return item.entityId() === parseInt(routeData.cardId);
                        });
                        card(matchingCard);
                        cardCount(quizzes()[0].cardIds.length);
                    });
            },

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

            showNextCard = function () {
                amplify.publish(config.pubs.showNextCard);
            },

            showPreviousCard = function () {
                amplify.publish(config.pubs.showPreviousCard);
            },

            flipCard = function () {
                card().questionSideVisible(!card().questionSideVisible());
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
            },

            editCard = function () {
                amplify.publish(config.pubs.showEditCard, card().entityId());
            },

            showDeleteDialog = function () {
                amplify.publish(config.pubs.showDeleteCard, {
                    data: card(),
                    callback: showNextCard
                });
            },

            displayIndex = ko.computed(function () {
                return _.indexOf(cards(), card()) + 1;
            });

        return {
            activate: activate,
            card: card,
            showNextCard: showNextCard,
            showPreviousCard: showPreviousCard,
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

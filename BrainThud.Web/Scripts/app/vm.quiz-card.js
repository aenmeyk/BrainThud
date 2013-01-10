define('vm.quiz-card', ['jquery', 'underscore', 'ko', 'data-context', 'utils', 'router', 'amplify', 'config', 'model', 'global', 'quiz-navigator'],
    function ($, _, ko, dataContext, utils, router, amplify, config, model, global, quizNavigator) {
        var
            cards = ko.observableArray([]),
            cardId = ko.observable(0),
            
            card = ko.computed(function () {
                var found = _.find(cards(), function (item) {
                    return item.entityId() === parseInt(cardId());
                });

                return found ? found : new model.Card();
            }),

            displayIndex = ko.computed(function () {
                return quizNavigator.cardIndex() + 1;
            }),

            cardCount = ko.computed(function () {
                return quizNavigator.cardCount();
            }),
            
            init = function() {
                amplify.subscribe(config.pubs.cardCacheChanged, function (data) {
                    cards(data);
                });
                amplify.subscribe(config.pubs.deleteCard, function () {
                    showNextCard();
                });
            },

            activate = function (routeData) {
                cardId(parseInt(routeData.cardId));
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
                amplify.publish(config.pubs.showDeleteCard, card());
            };

        init();

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

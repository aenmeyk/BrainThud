define('vm.quiz-card', ['jquery', 'underscore', 'ko', 'data-context', 'utils', 'router', 'amplify', 'config', 'model'],
    function ($, _, ko, dataContext, utils, router, amplify, config, model) {
        var
            init = function () {
            },

            cards = ko.observableArray([]),
            card = ko.observable(new model.Card()),

            activate = function(routeData) {
                $.when(dataContext.card.getData({ results: cards }))
                    .then(function () {
                        var matchingCard = _.find(cards(), function (item) {
                            return item.entityId() === parseInt(routeData.cardId);
                        });
                        card(matchingCard);
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

            getQuizPath = function () {
                return '#/quizzes/' + global.userId + '/' + utils.getDatePath();
            },

            getCardUri = function (cardIndex) {
                return getQuizPath() + '/' + cards()[cardIndex].entityId();
            },

            publishQuizResult = function (isCorrect) {
                amplify.publish(config.pubs.createQuizResult, {
                    cardId: card().entityId(),
                    isCorrect: isCorrect
                });
            },

            showNextCard = function() {
                var index = displayIndex();
                if (index < cards().length - 1) {
                    router.navigateTo(getCardUri(index + 1));
                } else {
                    router.navigateTo(getQuizPath());
                }
            },
            
            showPreviousCard = function() {
                var index = displayIndex();
                if (index > 0) {
                    router.navigateTo(getCardUri(index - 1));
                } else {
                    router.navigateTo(getQuizPath());
                }
            },
            
            flipCard = function() {
                card().questionSideVisible(!card().questionSideVisible());
            },
            
            submitCorrect = function() {
                dataContext.quizResult.createData(getQuizResultConfig(true));
                publishQuizResult(true);
                showNextCard();
            },
            
            submitIncorrect = function() {
                dataContext.quizResult.createData(getQuizResultConfig(false));
                publishQuizResult(false);
                showNextCard();
            },
            
            editCard = function() {
                router.navigateTo('#/cards/' + card().entityId() + '/edit');
            },
            
            showDeleteDialog = function() {
                $("#quiz-card-view .deleteDialog").modal('show');
            },
            
            deleteCard = function() {
                $.when(dataContext.card.deleteData({
                    params: {
                        userId: global.userId,
                        partitionKey: card().partitionKey(),
                        rowKey: card().rowKey(),
                        entityId: card().entityId()
                    }
                })).then(function () {
                    $("#quiz-card-view .deleteDialog").modal('hide');
                    showNextCard();
                    amplify.publish(config.pubs.deleteCard);
                });
            },
            
            displayIndex = ko.computed(function() {
                return _.indexOf(cards(), card());
            }),
            
            cardCount = ko.computed(function () {
                return cards().length;
            });
        
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
            deleteCard: deleteCard,
            displayIndex: displayIndex,
            cardCount: cardCount
        };
    }
);

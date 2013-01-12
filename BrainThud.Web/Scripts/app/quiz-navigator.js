define('quiz-navigator', ['ko', 'router', 'data-context', 'utils', 'amplify', 'config', 'global', 'model'],
    function (ko, router, dataContext, utils, amplify, config, global, model) {
        var
            cardIndex = ko.observable(0),
            cards = ko.observableArray([]),
            
            cardCount = ko.computed(function () {
                return cards().length;
            }),
            
            currentCard = ko.computed(function () {
                var card = cards()[cardIndex()];
                if (card) return card;
                return new model.Card();
            }),

             init = function () {
                 amplify.subscribe(config.pubs.cardCacheChanged, function () {
                     dataContext.quizCards.refreshCache();
                 });

                 amplify.subscribe(config.pubs.quizCardCacheChanged, function (data) {
                     cards(data);
                     
                     if (cardIndex() >= cards().length) {
                         showQuizSummary();
                     }
                 });
             },

           activate = function () {
                dataContext.quizCards.getData({
                    results: cards,
                    params: {
                        datePath: utils.getDatePath(),
                        userId: global.userId
                    }
                });
            },

            getQuizPath = function () {
                return '#/quizzes/' + global.userId + '/' + utils.getDatePath();
            },

            getCardUri = function () {
                return getQuizPath() + '/' + currentCard().entityId();
            },

            showLastCard = function () {
                cardIndex(cards().length - 1);
                if (cardIndex() > 0) {
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },

            showCurrentCard = function () {
                if (cardIndex() < cards().length - 1) {
                    router.navigateTo(getCardUri());
                } else {
                    showLastCard();
                }
            },

            showNextCard = function () {
                if (cardIndex() < cards().length - 1) {
                    cardIndex(cardIndex() + 1);
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },

            showPreviousCard = function () {
                if (cardIndex() > 0) {
                    cardIndex(cardIndex() - 1);
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },

            showQuizSummary = function () {
                cardIndex(0);
                router.navigateTo(getQuizPath());
            };

        amplify.subscribe(config.pubs.showCurrentCard, function () {
            showCurrentCard();
        });

        amplify.subscribe(config.pubs.showNextCard, function () {
            showNextCard();
        });

        amplify.subscribe(config.pubs.showPreviousCard, function () {
            showPreviousCard();
        });

        init();

        return {
            activate: activate,
            currentCard: currentCard,
            cardIndex: cardIndex,
            cardCount: cardCount,
            showNextCard: showNextCard,
            showPreviousCard: showPreviousCard
        };
    }
);
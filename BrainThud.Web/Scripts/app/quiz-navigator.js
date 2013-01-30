define('quiz-navigator', ['jquery', 'ko', 'underscore', 'router', 'data-context', 'amplify', 'config', 'global', 'model', 'moment'],
    function ($, ko, _, router, dataContext, amplify, config, global, model, moment) {
        var
            isActivated = ko.observable(false),
            cardIndex = ko.observable(0),
            cards = ko.observableArray([]),
            quizResults = ko.observableArray([]),
            quizYear = ko.observable(0),
            quizMonth = ko.observable(0),
            quizDay = ko.observable(0),

            quizDate = ko.computed(function () {
                return moment([quizYear(), quizMonth() - 1, quizDay()]).format('L');
            }),
            
            quizDatePath = ko.computed(function () {
                return moment(quizDate()).format('YYYY/M/D');
            }),
            
            isQuizToday = ko.computed(function() {
                return quizDate() === moment().format('L');
            }),
            
            isQuizInFuture = ko.computed(function () {
                return new Date(quizYear(), quizMonth() - 1, quizDay()) > new Date();
            }),

            cardCount = ko.computed(function () {
                return cards().length;
            }),

            completedCardCount = ko.computed(function () {
                return quizResults().length;
            }),

            correctCardCount = ko.computed(function () {
                return _.filter(quizResults(), function (quizResult) {
                    return quizResult.isCorrect();
                }).length;
            }),

            incorrectCardCount = ko.computed(function () {
                return _.filter(quizResults(), function (quizResult) {
                    return !quizResult.isCorrect();
                }).length;
            }),

            currentCard = ko.computed(function () {
                var card = cards()[cardIndex()];
                if (card) return card;
                return new model.Card();
            }),

            currentQuizResult = ko.computed(function () {
                var card = currentCard();
                var quizResult = _.find(quizResults(), function (item) {
                    return item.cardId() === card.entityId();
                });

                return quizResult;
            }),

            init = function () {
                amplify.subscribe(config.pubs.quizCardCacheChanged, function (data) {
                    _.each(cards(), function(existingCard) {
                        var updatedCard = _.find(data, function (item) {
                            return item.entityId() === existingCard.entityId();
                        });

                        var existingCardIndex = _.indexOf(cards(), existingCard);
                        cards.splice(existingCardIndex, 1, updatedCard);
                    });
                });

                amplify.subscribe(config.pubs.cardUpdated, function(data) {
                    var existingCard = _.find(cards(), function (item) {
                        return item.entityId() === data.entityId;
                    });

                    var existingCardIndex = _.indexOf(cards(), existingCard);
                    cards.splice(existingCardIndex, 1, data);
                });

                amplify.subscribe(config.pubs.quizResultCacheChanged, function (data) {
                    quizResults(data);
                });

                amplify.subscribe(config.pubs.showCurrentCard, function () {
                    showCurrentCard();
                });

                amplify.subscribe(config.pubs.showNextCard, function () {
                    showNextCard();
                });

                amplify.subscribe(config.pubs.showPreviousCard, function () {
                    showPreviousCard();
                });
            },

            activate = function (routeData) {
                isActivated(true);
                
                // If the new route doesn't match the current route then we need to get the cards and quizResults for the new route
                if (quizYear() !== routeData.year || quizMonth() !== routeData.month || quizDay() !== routeData.day) {
                    dataContext.quizCard.setCacheInvalid();
                    dataContext.quizResult.setCacheInvalid();
                    quizYear(routeData.year);
                    quizMonth(routeData.month);
                    quizDay(routeData.day);
                }

                $.when(getQuizCards(), getQuizResults())
                .done(function() {
                    if (routeData && routeData.cardId) {
                        var cardItems = cards();
                        var routeCard = _.find(cardItems, function(item) {
                            return item.entityId() === parseInt(routeData.cardId);
                        });

                        cardIndex(_.indexOf(cardItems, routeCard));
                    } else {
                        cardIndex(0);
                    }
                });
            },

            getQuizCards = function () {
                return dataContext.quizCard.getData({
                    results: cards,
                    params: {
                        datePath: quizDatePath(),
                        userId: global.userId
                    }
                });
            },

            getQuizResults = function () {
                return dataContext.quizResult.getData({
                    results: quizResults,
                    params: {
                        datePath: quizDatePath(),
                        userId: global.userId
                    }
                });
            },

            getQuizPath = function () {
                return global.routePrefix + 'quizzes/' + quizDatePath();
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
                if (cardIndex() < cards().length) {
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
            },
            
            shuffleCards = function() {
                cards(_.shuffle(cards()));
                cardIndex(0);
            };

        init();

        return {
            isActivated: isActivated,
            activate: activate,
            quizDate: quizDate,
            quizDatePath: quizDatePath,
            isQuizToday: isQuizToday,
            isQuizInFuture: isQuizInFuture,
            currentCard: currentCard,
            currentQuizResult: currentQuizResult,
            cardIndex: cardIndex,
            cardCount: cardCount,
            completedCardCount: completedCardCount,
            correctCardCount: correctCardCount,
            incorrectCardCount: incorrectCardCount,
            showCurrentCard: showCurrentCard,
            showNextCard: showNextCard,
            showPreviousCard: showPreviousCard,
            shuffleCards: shuffleCards
        };
    }
);
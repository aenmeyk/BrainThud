define('quiz-navigator', ['jquery', 'ko', 'card-manager', 'underscore', 'router', 'data-context', 'global', 'model', 'moment'],
    function ($, ko, cardManager, _, router, dataContext, global, model, moment) {
        var
            cardIndex = ko.observable(0),
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
                var card = cardManager.quizCards()[cardIndex()];
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

            activate = function (routeData) {
                var isQuizDateChanged = false;
                
                // If the new route doesn't match the current route then we need to get the cards and quizResults for the new route
                if (quizYear() !== routeData.year || quizMonth() !== routeData.month || quizDay() !== routeData.day) {
                    quizYear(routeData.year);
                    quizMonth(routeData.month);
                    quizDay(routeData.day);
                    isQuizDateChanged = true;
                }
                
                $.when(getQuizCards(routeData), getQuizResults(isQuizDateChanged))
                .done(function() {
                    if (routeData && routeData.cardId) {
                        var cards = cardManager.quizCards(),
                            routeCard = _.find(cards, function (item) {
                            return item.entityId() === parseInt(routeData.cardId);
                        });

                        cardIndex(_.indexOf(cards, routeCard));
                    } else {
                        cardIndex(0);
                    }
                });
            },

            getQuizCards = function () {
                return cardManager.getQuizCards(quizYear(), quizMonth(), quizDay());
            },

            getQuizResults = function (isQuizDateChanged) {
                // If the quiz date has changed, invalidate the cache
                if (isQuizDateChanged) {
                    dataContext.quizResult.setCacheInvalid();
                }
                
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
                cardIndex(cardManager.quizCards().length - 1);
                if (cardIndex() > 0) {
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },

            showCurrentCard = function () {
                if (cardIndex() < cardManager.quizCards().length) {
                    router.navigateTo(getCardUri());
                } else {
                    showLastCard();
                }
            },

            showNextCard = function () {
                if (cardIndex() < cardManager.quizCards().length - 1) {
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
                cardManager.shuffleQuizCards();
                cardIndex(0);
            };

        return {
            activate: activate,
            quizDate: quizDate,
            quizYear: quizYear,
            quizMonth: quizMonth,
            quizDay: quizDay,
            quizDatePath: quizDatePath,
            isQuizToday: isQuizToday,
            isQuizInFuture: isQuizInFuture,
            currentCard: currentCard,
            currentQuizResult: currentQuizResult,
            cardIndex: cardIndex,
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
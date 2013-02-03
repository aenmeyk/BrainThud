define('quiz-navigator', ['jquery', 'ko', 'card-manager', 'underscore', 'router', 'data-context', 'global', 'model', 'moment'],
    function ($, ko, cardManager, _, router, dataContext, global, model, moment) {
        var
            cardOrderIndex = ko.observable(0),
            cardOrder = ko.observableArray([]),
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
                var index = cardOrder()[cardOrderIndex()],
                    card = cardManager.quizCards()[index];
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
                    var cards = cardManager.quizCards(),
                        cardCount = cards.length;
                    
                    if (cardOrder().length !== cardCount) {
                        var indexes = [];
                        for (var i = 0; i < cardCount; i++) {
                            indexes.push(i);
                        }
                        cardOrder(indexes);
                    }

                    if (routeData && routeData.cardId) {
                        var routeCard = _.find(cards, function(item) {
                                return item.entityId() === parseInt(routeData.cardId);
                            }),
                            cardIndex = _.indexOf(cards, routeCard),
                            orderIndex = _.indexOf(cardOrder(), cardIndex);

                        cardOrderIndex(orderIndex);
                    } else {
                        cardOrderIndex(0);
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
                cardOrderIndex(cardOrder().length - 1);
                if (cardOrderIndex() > 0) {
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },

            showCurrentCard = function () {
                if (cardOrderIndex() < cardManager.quizCards().length) {
                    router.navigateTo(getCardUri());
                } else {
                    showLastCard();
                }
            },

            showNextCard = function () {
                if (cardOrderIndex() < cardManager.quizCards().length - 1) {
                    cardOrderIndex(cardOrderIndex() + 1);
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },

            showPreviousCard = function () {
                if (cardOrderIndex() > 0) {
                    cardOrderIndex(cardOrderIndex() - 1);
                    router.navigateTo(getCardUri());
                } else {
                    showQuizSummary();
                }
            },

            showQuizSummary = function () {
                cardOrderIndex(0);
                router.navigateTo(getQuizPath());
            },

            removeCurrentCardIndex = function () {
                var currentIndex = cardOrderIndex(),
                    cardOrderIndexes = cardOrder();
                
                cardOrderIndexes = _.without(cardOrderIndexes, currentIndex);
                
                // Since a card has been removed, we need to reduce the index of
                // every card that had a higher index of the one removed.
                for (var i = 0; i < cardOrderIndexes.length; i++) {
                    if (cardOrderIndexes[i] > currentIndex) {
                        cardOrderIndexes[i] = cardOrderIndexes[i] - 1;
                    }
                }

                cardOrder(cardOrderIndexes);
                showCurrentCard();
            },

            shuffleCards = function () {
                cardOrder(_.shuffle(cardOrder()));
                cardOrderIndex(0);
                toastr.success('Cards Shuffled');
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
            cardOrderIndex: cardOrderIndex,
            removeCurrentCardIndex: removeCurrentCardIndex,
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
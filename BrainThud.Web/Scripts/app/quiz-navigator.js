﻿define('quiz-navigator', ['jquery', 'ko', 'underscore', 'router', 'data-context', 'utils', 'amplify', 'config', 'global', 'model', 'moment'],
    function ($, ko, _, router, dataContext, utils, amplify, config, global, model, moment) {
        var
            isActivated = ko.observable(false),
            cardIndex = ko.observable(0),
            cards = ko.observableArray([]),
            quizResults = ko.observableArray([]),
            quizDate = ko.observable(''),
            quizYear = ko.observable(0),
            quizMonth = ko.observable(0),
            quizDay = ko.observable(0),
            
            quizDatePath = ko.computed(function () {
                return moment([quizYear(), quizMonth() - 1, quizDay()]).format('YYYY/M/D');
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

            init = function () {
                amplify.subscribe(config.pubs.quizCardCacheChanged, function (data) {
                    cards(data);
                    var cardsLength = cards().length;
                    if (cardIndex() >= cardsLength) {
                        cardIndex(0);
                    }
                });
            },

            activate = function (routeData) {
                // If the new route doesn't match the current route then we need to get the cards and quizResults for the new route
                if (quizYear() !== routeData.year || quizMonth() !== routeData.month || quizDay() !== routeData.day) {
//                    isActivated(false);
                    dataContext.quizCard.setCacheInvalid();
                    dataContext.quizResult.setCacheInvalid();
                    quizYear(routeData.year);
                    quizMonth(routeData.month);
                    quizDay(routeData.day);
                }

                if (!isActivated()) {
//                    isActivated(true);

                    var year = quizYear(),
                        month = quizMonth(),
                        day = quizDay();

                    quizDate(moment([year, month - 1, day]).format('L'));

                    $.when(getQuizCards(), getQuizResults())
                        .done(function () {
//                            if (routeData) {
//                                var cardItems = cards();
//                                var routeCard = _.find(cardItems, function (item) {
//                                    return item.entityId() === parseInt(routeData.cardId);
//                                });
//
//                                cardIndex(_.indexOf(cardItems, routeCard));
//                                showCurrentCard();
//                            }
                        });
                };
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
                return global.routePrefix + 'quizzes/' + utils.getDatePath();
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
            isActivated: isActivated,
            activate: activate,
            quizDate: quizDate,
            quizDatePath: quizDatePath,
            currentCard: currentCard,
            cardIndex: cardIndex,
            cardCount: cardCount,
            completedCardCount: completedCardCount,
            correctCardCount: correctCardCount,
            incorrectCardCount: incorrectCardCount,
            showCurrentCard: showCurrentCard,
            showNextCard: showNextCard,
            showPreviousCard: showPreviousCard
        };
    }
);
define('vm.quiz', ['jquery', 'ko', 'underscore', 'moment', 'amplify', 'config', 'data-context', 'utils', 'global'],
    function ($, ko, _, moment, amplify, config, dataContext, utils, global) {
        var quizDate = ko.observable(''),
            quizResults = ko.observableArray([]),
            cards = ko.observableArray([]),
            quizYear = ko.observable(0),
            quizMonth = ko.observable(0),
            quizDay = ko.observable(0),

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

            init = function () {
                amplify.subscribe(config.pubs.quizCardCacheChanged, function (data) {
                    cards(data);
                });

                amplify.subscribe(config.pubs.quizResultCacheChanged, function (data) {
                    quizResults(data);
                });
            },

            activate = function (routeData) {
                // If the new route doesn't match the current route then we need to get the cards and quizResults for the new route
                if (quizYear() !== routeData.year || quizMonth() !== routeData.month || quizDay() !== routeData.day) {
                    dataContext.quizCard.setCacheInvalid();
                    dataContext.quizResult.setCacheInvalid();
                    quizYear(routeData.year);
                    quizMonth(routeData.month);
                    quizDay(routeData.day);
                }

                var year = quizYear(),
                    month = quizMonth(),
                    day = quizDay();

                quizDate(moment([year, month - 1, day]).format('L'));
                getQuizCards(year, month, day);
                getQuizResults(year, month, day);
            },

            getQuizCards = function (year, month, day) {
                return dataContext.quizCard.getData({
                    results: cards,
                    params: {
                        datePath: moment([year, month - 1, day]).format('YYYY/M/D'),
                        userId: global.userId
                    }
                });
            },

            getQuizResults = function (year, month, day) {
                return dataContext.quizResult.getData({
                    results: quizResults,
                    params: {
                        datePath: moment([year, month - 1, day]).format('YYYY/M/D'),
                        userId: global.userId
                    }
                });
            },

            startQuiz = function () {
                amplify.publish(config.pubs.showCurrentCard);
            };

        init();

        return {
            info: '',
            quizDate: quizDate,
            cardCount: cardCount,
            completedCardCount: completedCardCount,
            correctCardCount: correctCardCount,
            incorrectCardCount: incorrectCardCount,
            activate: activate,
            startQuiz: startQuiz
        };
    }
);

define('vm.quiz', ['jquery', 'ko', 'underscore', 'moment', 'amplify', 'config', 'data-context', 'utils', 'global', 'dom'],
    function ($, ko, _, moment, amplify, config, dataContext, utils, global, dom) {
        var quizDate = moment().format('L'),
            quizResults = ko.observableArray([]),
            cards = ko.observableArray([]),
            
            cardCount = ko.computed(function() {
                return cards().length;
            }),
            
            completedCardCount = ko.computed(function () {
                return quizResults().length;
            }),

            correctCardCount = ko.computed(function () {
                return _.filter(quizResults(), function(quizResult) {
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
                    dataContext.quizResult.refreshCache();
                });
                
                amplify.subscribe(config.pubs.quizResultCacheChanged, function (data) {
                    quizResults(data);
                });
            },

            activate = function () {
                getQuizCards();
                getQuizResults();
            },
            
            getQuizCards = function () {
                return dataContext.quizCards.getData({
                        results: cards,
                        params: {
                            datePath: utils.getDatePath(),
                            userId: global.userId
                        }
                    });
            },
            
            getQuizResults = function() {
                return dataContext.quizResult.getData({
                    results: quizResults,
                    params: {
                        datePath: utils.getDatePath(),
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

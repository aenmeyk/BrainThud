define('vm.quiz', ['ko', 'underscore', 'moment', 'amplify', 'config', 'data-context', 'utils', 'global'],
    function (ko, _, moment, amplify, config, dataContext, utils, global) {
        var
            init = function() {
//                amplify.subscribe(config.pubs.quizResultCacheChanged, function (data) {
//
//                    var cardIds = _.map(data, function(item) {
//                        return item.cardId();
//                    });
//
//                    var results = quizResults();
//                    _.each(cardIds, function(cardId) {
//                        for (var i = 0; i < results.length; i++) {
//                            if (results[i].cardId == cardId) {
//                                quizResults.splice(i, 1);
//                                break;
//                            }
//                        }
//                    });
//
//                    _.each(data, function (item) {
//                        quizResults.push(ko.toJS(item));
//                    });
//                });
            },

            quizDate = moment().format('L'),
            quizResults = ko.observableArray([]),
            cards = ko.observable([]),
            
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

            activate = function() {
                dataContext.quizCards.getData({
                    results: cards,
                    params: {
                        datePath: utils.getDatePath(),
                        userId: global.userId
                    }
                });
                dataContext.quizResult.getData({
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

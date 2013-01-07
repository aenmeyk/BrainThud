﻿define('vm.quiz', ['ko', 'underscore', 'moment', 'quiz-navigator', 'amplify', 'config'],
    function (ko, _, moment, quizNavigator, amplify, config) {
        var
            // How to handl quiz result changes??
            init = function() {
                amplify.subscribe(config.pubs.quizResultCacheChanged, function (data) {

                    var cardIds = _.map(data, function(item) {
                        return item.cardId();
                    });

                    var results = quizResults();
                    _.each(cardIds, function(cardId) {
                        for (var i = 0; i < results.length; i++) {
                            if (results[i].cardId == cardId) {
                                quizResults.splice(i, 1);
                                break;
                            }
                        }
                    });

                    _.each(data, function (item) {
                        quizResults.push(ko.toJS(item));
                    });
                });
            },

            quizDate = ko.observable(),
            cardCount = ko.observable(0),
            quizResults = ko.observableArray([]),
            
            completedCardCount = ko.computed(function () {
                return quizResults().length;
            }),
            
            correctCardCount = ko.computed(function () {
                return countQuizResults(function(quizResult) {
                    return quizResult.isCorrect;
                });
            }),
            
            incorrectCardCount = ko.computed(function () {
                return countQuizResults(function (quizResult) {
                    return !quizResult.isCorrect;
                });
            }),

            activate = function () { },

            startQuiz = function () {
                amplify.publish(config.pubs.showCurrentCard);
            };
        
        init();
                    
        function countQuizResults(shouldCount) {
            var correct = 0;
            var results = quizResults();
            for (var i = 0; i < quizResults().length; i++) {
                if (shouldCount(results[i])) correct++;
            }
            return correct;
        };


        return {
            startQuiz: startQuiz,
            activate: activate,
            quizDate: quizDate,
            cardCount: cardCount,
            completedCardCount: completedCardCount,
            correctCardCount: correctCardCount,
            incorrectCardCount: incorrectCardCount
        };
    }
);

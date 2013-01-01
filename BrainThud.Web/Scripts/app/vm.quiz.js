define('vm.quiz', ['ko', 'data-context', 'utils', 'moment', 'quiz-navigator', 'amplify', 'config', 'global'],
    function (ko, dataContext, utils, moment, quizNavigator, amplify, config, global) {
        var
            
            init = function() {
                amplify.subscribe(config.pubs.deleteCard, function () {
                    invalidateCache = true;
                });
            },

            invalidateCache = false,
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

            quizzes = ko.observableArray([]),

            dataOptions = function() {
                return {
                    invalidateCache: invalidateCache,
                    results: quizzes,
                    params: {
                        datePath: utils.getDatePath(),
                        userId: global.userId
                    }
                };
            },

           activate = function () {
               $.when(dataContext.quiz.getData(dataOptions()))
                   .then(function () {
                       var quiz = quizzes()[0];
                       quizDate(moment(quiz.quizDate).format('L'));
                       cardCount(quiz.cardIds.length);
                       quizResults(quiz.quizResults);
                       invalidateCache = false;
                       quizNavigator.activate();
                   });
           },

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

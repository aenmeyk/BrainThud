define('vm.quiz', ['ko', 'data-context', 'utils', 'moment', 'router', 'quiz-navigator', 'amplify', 'config'],
    function (ko, dataContext, utils, moment, router, quizNavigator, amplify, config) {
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
               // TODO: Find a better way of ensuring that the ViewModels use the same store for cards. 
               // (We need the same store because if a card is updated on one ViewModel we need the value to be updated on the other ViewModels too.)
               $.when(dataContext.quiz.getData(dataOptions()))
                   .then(function () {
                       var quiz = quizzes()[0];
                       quizDate(moment(quiz.quizDate).format('L'));
                       cardCount(quiz.cardIds.length);
                       quizResults(quiz.quizResults);
                       invalidateCache = false;
                       quizNavigator.init();
                   });
           },

            startQuiz = function () {
                var url = '#/quizzes/' + global.userId + '/' + utils.getDatePath() + '/' + quizzes()[0].cardIds[0];
                router.navigateTo(url);
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

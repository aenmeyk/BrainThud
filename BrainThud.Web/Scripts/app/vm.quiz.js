﻿define('vm.quiz', ['ko', 'data-context', 'utils', 'moment', 'router', 'amplify', 'config'],
    function (ko, dataContext, utils, moment, router, amplify, config) {
        var
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

            dataOptions = {
                results: quizzes,
                params: {
                    datePath: utils.getDatePath(),
                    userId: global.userId // TODO: I can use global.userId directly from wherever this is being passed
                }
            },

           activate = function () {
               // TODO: Find a better way of ensuring that the ViewModels use the same store for cards. 
               // (We need the same store because if a card is updated on one ViewModel we need the value to be updated on the other ViewModels too.)
               $.when(dataContext.quizCard.getData(dataOptions))
                   .then(function () {
                       var quiz = quizzes()[0];
                       quizDate(moment(quiz.quizDate).format('L'));
                       cardCount(quiz.cards.length);
                       quizResults(quiz.quizResults);
                   });
           },

            startQuiz = function () {
                var url = '#/quizzes/' + global.userId + '/' + utils.getDatePath() + '/' + quizzes()[0].cards[0].cardId();
                router.navigateTo(url);
            };
        

        amplify.subscribe(config.pubs.addQuizResult, function (data) {
            quizResults().push(data);
        });

        
                    
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

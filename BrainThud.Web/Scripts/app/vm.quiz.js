define('vm.quiz', ['ko', 'data-context', 'utils', 'moment', 'router'],
    function (ko, dataContext, utils, moment, router) {
        var
            quizDate = ko.observable(),
            cardCount = ko.observable(0),
            completedCardCount = ko.observable(0),
            correctCardCount = ko.observable(0),
            incorrectCardCount = ko.observable(0),
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
                   });
           },

            startQuiz = function () {
                var url = '#/quizzes/' + global.userId + '/' + utils.getDatePath() + '/' + quizzes()[0].cards[0].cardId();
                router.navigateTo(url);
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

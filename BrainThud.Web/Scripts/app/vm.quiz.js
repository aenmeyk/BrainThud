define('vm.quiz', ['jquery', 'ko', 'underscore', 'moment', 'amplify', 'config', 'data-context', 'utils', 'global', 'quiz-navigator'],
    function ($, ko, _, moment, amplify, config, dataContext, utils, global, quizNavigator) {
        var
            activate = function (routeData) {
                quizNavigator.activate(routeData);
            };

        return {
            quizDate: quizNavigator.quizDate,
            cardCount: quizNavigator.cardCount,
            completedCardCount: quizNavigator.completedCardCount,
            correctCardCount: quizNavigator.correctCardCount,
            incorrectCardCount: quizNavigator.incorrectCardCount,
            activate: activate,
            startQuiz: quizNavigator.showCurrentCard
        };
    }
);

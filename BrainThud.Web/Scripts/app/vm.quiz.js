define('vm.quiz', ['quiz-navigator'],
    function (quizNavigator) {
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

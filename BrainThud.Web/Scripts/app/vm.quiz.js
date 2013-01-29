define('vm.quiz', ['quiz-navigator', 'moment'],
    function (quizNavigator, moment) {
        var
            pageTitle = ko.observable("Today's Quiz");
            startQuizLabel = ko.observable('Start Quiz');

            activate = function (routeData) {
                quizNavigator.activate(routeData);

                if (quizNavigator.quizDate() === moment().format('L')) {
                    pageTitle("Today's Quiz");
                    startQuizLabel(' Start Quiz');
                } else {
                    pageTitle('Quiz History');
                    startQuizLabel(' Review Quiz');
                }
            };

        return {
            quizDate: quizNavigator.quizDate,
            cardCount: quizNavigator.cardCount,
            completedCardCount: quizNavigator.completedCardCount,
            correctCardCount: quizNavigator.correctCardCount,
            incorrectCardCount: quizNavigator.incorrectCardCount,
            activate: activate,
            pageTitle: pageTitle,
            startQuizLabel: startQuizLabel,
            startQuiz: quizNavigator.showCurrentCard
        };
    }
);

define('vm.quiz', ['ko', 'quiz-navigator'],
    function (ko, quizNavigator) {
        var
            pageTitle = ko.observable("Today's Quiz"),
            startQuizLabel = ko.observable('Start Quiz'),
            
            isStartDisabled = ko.computed(function() {
                return quizNavigator.cardCount() === 0 || quizNavigator.isQuizInFuture();
            }),
        
            activate = function (routeData) {
                quizNavigator.activate(routeData);

                if (quizNavigator.isQuizToday()) {
                    pageTitle("Today's Quiz");
                    startQuizLabel(' Start Quiz');
                } else {
                    pageTitle('Quiz Review');
                    startQuizLabel(' Start Review');
                }
            };

        return {
            quizDate: quizNavigator.quizDate,
            cardCount: quizNavigator.cardCount,
            isStartDisabled: isStartDisabled,
            completedCardCount: quizNavigator.completedCardCount,
            correctCardCount: quizNavigator.correctCardCount,
            incorrectCardCount: quizNavigator.incorrectCardCount,
            activate: activate,
            pageTitle: pageTitle,
            startQuizLabel: startQuizLabel,
            startQuiz: quizNavigator.showCurrentCard,
            shuffleCards: quizNavigator.shuffleCards
        };
    }
);

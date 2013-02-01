define('vm.quiz', ['ko', 'quiz-navigator', 'card-manager'],
    function (ko, quizNavigator, cardManager) {
        var
            pageTitle = ko.observable("Today's Quiz"),
            startQuizLabel = ko.observable('Start Quiz'),
            
            isStartDisabled = ko.computed(function() {
                return cardManager.quizCardCount() === 0 || quizNavigator.isQuizInFuture();
            }),
            
            isShuffleDisabled = ko.computed(function () {
                return !quizNavigator.isQuizToday();
            }),
        
            activate = function (routeData) {
                // quizNavigator.activate(routeData);
                cardManager.getQuizCards(routeData.year, routeData.month, routeData.day);

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
            cardCount: cardManager.quizCardCount,
            isStartDisabled: isStartDisabled,
            isShuffleDisabled: isShuffleDisabled,
            completedCardCount: quizNavigator.completedCardCount,
            correctCardCount: quizNavigator.correctCardCount,
            incorrectCardCount: quizNavigator.incorrectCardCount,
            activate: activate,
            pageTitle: pageTitle,
            startQuizLabel: startQuizLabel,
            startQuiz: quizNavigator.showCurrentCard,
            shuffleCards: cardManager.shuffleQuizCards
        };
    }
);

define('model', ['model.card', 'model.quiz-result', 'model.user-configuration'],
    function (card, quizResult, userConfiguration) {
        return {
            Card: card,
            QuizResult: quizResult,
            UserConfiguration: userConfiguration
        };
    }
);
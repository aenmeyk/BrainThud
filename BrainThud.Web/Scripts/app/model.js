define('model', ['model.card', 'model.quiz-result'],
    function (card, quizResult) {
        return {
            Card: card,
            QuizResult: quizResult
        };
    }
);
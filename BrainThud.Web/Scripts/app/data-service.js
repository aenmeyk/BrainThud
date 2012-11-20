define('data-service', ['data-service.card', 'data-service.quiz', 'data-service.quiz-result'],
    function (cards, quiz, quizResult) {
        return {
            cards: cards,
            quiz: quiz,
            quizResult: quizResult
        };
    }
);
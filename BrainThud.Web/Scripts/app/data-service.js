define('data-service', ['data-service.card','data-service.quiz'],
    function (cards, quiz) {
        return {
            cards: cards,
            quiz: quiz
        };
    }
);
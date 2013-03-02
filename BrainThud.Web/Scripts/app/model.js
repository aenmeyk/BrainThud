define('model', ['model.card', 'model.card-deck', 'model.quiz-result', 'model.user-configuration'],
    function (card, cardDeck, quizResult, userConfiguration) {
        return {
            Card: card,
            CardDeck: cardDeck,
            QuizResult: quizResult,
            UserConfiguration: userConfiguration
        };
    }
);
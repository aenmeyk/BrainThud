define('vm', ['vm.card', 'vm.create-card', 'vm.cards', 'vm.quiz'],
    function (card, createCard, cards, quiz) {
        return {
            card: card,
            createCard: createCard,
            cards: cards,
            quiz: quiz
        };
    }
);
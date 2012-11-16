define('vm', ['vm.card', 'vm.cards', 'vm.quiz'],
    function (card, cards, quiz) {
        return {
            card: card,
            cards: cards,
            quiz: quiz
        };
    }
);
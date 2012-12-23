define('vm', ['vm.card', 'vm.create-card', 'vm.cards', 'vm.quiz', 'vm.nav'],
    function (card, createCard, cards, quiz, nav) {
        return {
            card: card,
            createCard: createCard,
            cards: cards,
            quiz: quiz,
            nav: nav
        };
    }
);
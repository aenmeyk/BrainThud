define('vm', ['vm.card', 'vm.cards'],
    function (card, cards) {
        return {
            card: card,
            cards: cards
        };
    }
);
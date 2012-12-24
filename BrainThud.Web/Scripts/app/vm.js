define('vm', ['vm.card', 'vm.create-card', 'vm.cards', 'vm.quiz', 'vm.quiz-card', 'vm.nav'],
    function (card, createCard, cards, quiz, quizCard, nav) {
        return {
            card: card,
            createCard: createCard,
            cards: cards,
            quiz: quiz,
            quizCard: quizCard,
            nav: nav
        };
    }
);
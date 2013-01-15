define('vm', ['vm.card', 'vm.create-card', 'vm.library', 'vm.quiz', 'vm.quiz-card', 'vm.nav'],
    function (card, createCard, library, quiz, quizCard, nav) {
        return {
            card: card,
            createCard: createCard,
            library: library,
            quiz: quiz,
            quizCard: quizCard,
            nav: nav
        };
    }
);
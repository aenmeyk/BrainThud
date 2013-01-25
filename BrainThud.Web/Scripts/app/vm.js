define('vm', ['vm.card', 'vm.create-card', 'vm.library', 'vm.quiz', 'vm.quiz-card', 'vm.nav', 'vm.card-info', 'vm.user-configuration'],
    function (card, createCard, library, quiz, quizCard, nav, cardInfo, userConfiguration) {
        return {
            card: card,
            createCard: createCard,
            library: library,
            quiz: quiz,
            quizCard: quizCard,
            cardInfo: cardInfo,
            userConfiguration: userConfiguration,
            nav: nav
        };
    }
);
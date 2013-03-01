define('config', ['toastr'],
    function (toastr) {
        var
            hashes = {
                cardEdit: '#/:userId/cards/:cardId/edit',
                createCard: '#/:userId/cards/new',
                library: '#/:userId/library',
                deck: '#/:userId/library/:deckNameSlug',
                quiz: '#/:userId/quizzes/:year/:month/:day',
                quizCard: '#/:userId/quizzes/:year/:month/:day/:cardId',
                userConfiguration: '#/:userId/user-configuration'
            },

            viewIds = {
                card: '#card-view',
                createCard: '#create-card-view',
                library: '#library-view',
                quiz: '#quiz-view',
                quizCard: '#quiz-card-view',
                userConfiguration: '#user-configuration-view',
                cardInfo: '#card-info-dialog',
                nav: '#nav-view'
            },

            routes = {
                userConfiguration: '/api/config',
                cards: '/api/cards',
                cardDecks: '/api/card-decks/{userIdPath}',
                card: '/api/cards/{userIdPath}/{entityIdPath}',
                quizResults: '/api/quiz-results/{userIdPath}/{datePath}',
                quizResult: '/api/quiz-results/{userIdPath}/{datePath}/{cardIdPath}',
                quizCards: '/api/cards/{userIdPath}/{datePath}'
            };

        toastr.options.timeOut = 1500;
        toastr.options.positionClass = 'toast-bottom-full-width';

        return {
            hashes: hashes,
            viewIds: viewIds,
            routes: routes
        };
    }
);
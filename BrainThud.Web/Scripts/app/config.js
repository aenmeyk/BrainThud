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
                card: '/api/cards/{userIdPath}/{entityIdPath}',
                quizResults: '/api/quiz-results/{userIdPath}/{datePath}',
                quizResult: '/api/quiz-results/{userIdPath}/{datePath}/{cardIdPath}',
                quizCards: '/api/cards/{userIdPath}/{datePath}'
            },

            pubs = {
                createQuizResult: 'create-quiz-result',
                updateCard: 'update-card',
                cardUpdated: 'card-updated',
                deleteCard: 'delete-card',
                cardDeleted: 'card-deleted',
                showCurrentCard: 'show-current-card',
                showNextCard: 'show-next-card',
                showPreviousCard: 'show-previous-card',
                showEditCard: 'show-edit-card',
                showDeleteCard: 'show-delete-card',
                showCardInfo: 'show-card-info',
                createNewCard: 'create-new-card',
                cardCacheChanged: 'card-cache-changed',
                quizCardCacheChanged: 'quiz-card-cache-changed',
                quizResultCacheChanged: 'quiz-result-cache-changed'
            };

        toastr.options.timeOut = 1500;

        return {
            hashes: hashes,
            viewIds: viewIds,
            pubs: pubs,
            routes: routes
        };
    }
);
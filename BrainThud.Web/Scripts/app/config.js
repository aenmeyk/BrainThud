define('config', ['toastr'],
    function (toastr) {
        var
            hashes = {
                cardEdit: '#/cards/:cardId/edit',
                createCard: '#/cards/new',
                cards: '#/cards',
                quiz: '#/quizzes/:userId/:year/:month/:day',
                quizCard: '#/quizzes/:userId/:year/:month/:day/:cardId'
            },

            viewIds = {
                card: '#card-view',
                createCard: '#create-card-view',
                cards: '#cards-view',
                quiz: '#quiz-view',
                quizCard: '#quiz-card-view',
                nav: '#nav-view'
            },
            
            routes = {
                cards: '/api/cards',
                card: '/api/cards/{userIdPath}/{entityIdPath}',
                quizResults: '/api/quiz-results/{userIdPath}/{datePath}',
                quizResult: '/api/quiz-results/{userIdPath}/{datePath}/{cardIdPath}',
                quizCards: '/api/cards/{userIdPath}/{datePath}'
            },

            pubs = {
                createQuizResult: 'create-quiz-result',
                createCard: 'create-card',
                updateCard: 'update-card',
                deleteCard: 'delete-card',
                showCurrentCard: 'show-current-card',
                showNextCard: 'show-next-card',
                showPreviousCard: 'show-previous-card',
                showEditCard: 'show-edit-card',
                showDeleteCard: 'show-delete-card',
                createNewCard: 'create-new-card',
                cardCacheChanged: 'card-cache-changed',
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
define('config', [],
    function () {
        var hashes = {
                createCard: '#/create-card',
                todaysCards: '#/todays-cards',
                quiz: '#/quizzes/:year/:month/:day/:rowKey'
            },

            viewIds = {
                createCard: '#create-card-view',
                todaysCards: '#todays-cards-view',
                quiz: '#quiz-view'
            };

        return {
            hashes: hashes,
            viewIds: viewIds
        };
    }
);
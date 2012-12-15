define('config', ['toastr'],
    function (toastr) {
        var hashes = {
                createCard: '#/create-card',
                todaysCards: '#/cards',
                quiz: '#/quizzes/:userId/:year/:month/:day/:cardId'
            },

            viewIds = {
                createCard: '#create-card-view',
                todaysCards: '#cards-view',
                quiz: '#todays-quiz-view'
            };

        toastr.options.timeOut = 1500;

        return {
            hashes: hashes,
            viewIds: viewIds
        };
    }
);
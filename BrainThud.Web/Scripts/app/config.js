define('config', ['toastr'],
    function (toastr) {
        var hashes = {
                createCard: '#/create-card',
                todaysCards: '#/todays-cards',
                quiz: '#/quizzes/:userId/:year/:month/:day/:rowKey'
            },

            viewIds = {
                createCard: '#create-card-view',
                todaysCards: '#todays-cards-view',
                quiz: '#quiz-view'
            };

        toastr.options.timeOut = 1500;

        return {
            hashes: hashes,
            viewIds: viewIds
        };
    }
);
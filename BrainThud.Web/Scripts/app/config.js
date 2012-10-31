define('config', [],
    function () {
        var hashes = {
                todaysCards: '#/todays-cards',
                createCard: '#/create-card'
            },

            viewIds = {
                todaysCards: '#todays-cards-view',
                createCard: '#create-card-view'
            };

        return {
            hashes: hashes,
            viewIds: viewIds
        };
    }
);
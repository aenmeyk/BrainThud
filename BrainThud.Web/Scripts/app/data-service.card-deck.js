define('data-service.card-deck', ['amplify', 'config'],
    function (amplify, config) {
        var
            init = function() {
                amplify.request.define('getCardDecks', 'ajax', {
                    url: config.routes.cardDecks,
                    dataType: 'json',
                    type: 'GET'
                });
            },

            get = function (options) {
                return amplify.request({
                    resourceId: 'getCardDecks',
                    data: {
                        userIdPath: options.params.userId
                    },
                    success: options.success,
                    error: options.error
                });
            };
        
        init();

        return {
            get: get
        };
    }
);
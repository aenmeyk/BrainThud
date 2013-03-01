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

            get = function (callbacks) {
                return amplify.request({
                    resourceId: 'getCardDecks',
                    data: {
                        userIdPath: callbacks.params.userId
                    },
                    success: callbacks.success,
                    error: callbacks.error
                });
            };
        
        init();

        return {
            get: get
        };
    }
);
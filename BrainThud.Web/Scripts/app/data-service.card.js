define('data-service.card', ['amplify'],
    function (amplify) {
        var
            init = function() {
                amplify.request.define('getCards', 'ajax', {
                    url: '/api/cards',
                    dataType: 'json',
                    type: 'GET'
                }),
                amplify.request.define('createCard', 'ajax', {
                    url: '/api/cards',
                    dataType: 'json',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8'
                });
            },

            get = function (callbacks) {
                return amplify.request({
                    resourceId: 'getCards',
                    success: callbacks.success,
                    error: callbacks.error
                });
            },

            save = function (data, callbacks) {
                return amplify.request({
                    resourceId: 'createCard',
                    data: JSON.stringify(data),
                    success: callbacks.success,
                    error: callbacks.error
                });
            };
        
        init();

        return {
            get: get,
            save: save
        };
    }
);
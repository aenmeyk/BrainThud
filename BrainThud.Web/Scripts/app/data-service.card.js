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
                }),
                amplify.request.define('updateCard', 'ajax', {
                    url: '/api/cards',
                    dataType: 'json',
                    type: 'PUT',
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

            create = function (data, callbacks) {
                return amplify.request({
                    resourceId: 'createCard',
                    data: data,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },

            update = function (data, callbacks) {
                return amplify.request({
                    resourceId: 'updateCard',
                    data: data,
                    success: callbacks.success,
                    error: callbacks.error
                });
            };
        
        init();

        return {
            get: get,
            create: create,
            update: update
        };
    }
);
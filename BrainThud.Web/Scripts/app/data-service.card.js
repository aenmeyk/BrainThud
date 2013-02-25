define('data-service.card', ['amplify', 'config', 'moment'],
    function (amplify, config, moment) {
        var
            init = function() {
                amplify.request.define('getCards', 'ajax', {
                    url: config.routes.cards,
                    dataType: 'json',
                    type: 'GET'
                }),
                amplify.request.define('getSingle', 'ajax', {
                    url: config.routes.card,
                    dataType: 'json',
                    type: 'GET'
                }),
                amplify.request.define('getForQuiz', 'ajax', {
                    url: config.routes.quizCards,
                    dataType: 'json',
                    type: 'GET'
                }),
                amplify.request.define('createCard', 'ajax', {
                    url: config.routes.cards,
                    dataType: 'json',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    beforeSend: function(xhr) {
                        xhr.setRequestHeader('X-Client-Date', moment().format());
                    }
                }),
                amplify.request.define('updateCard', 'ajax', {
                    url: config.routes.cards,
                    dataType: 'json',
                    type: 'PUT',
                    contentType: 'application/json; charset=utf-8'
                });
                amplify.request.define('deleteCard', 'ajax', {
                    url: config.routes.cards,
                    dataType: 'json',
                    type: 'DELETE',
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

            getSingle = function (options) {
                return amplify.request({
                    resourceId: 'getSingle',
                    data: {
                        userIdPath: options.params.userId,
                        entityIdPath: options.params.entityId
                    },
                    success: options.success,
                    error: options.error
                });
            },

            getForQuiz = function (options) {
                return amplify.request({
                    resourceId: 'getForQuiz',
                    data: {
                        datePath: options.params.datePath,
                        userIdPath: options.params.userId
                    },
                    success: options.success,
                    error: options.error
                });
            },

            create = function (data, cache, callbacks) {
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
            },

            deleteItem = function (options) {
                return amplify.request({
                    resourceId: 'deleteCard',
                    data: options.data,
                    success: options.success,
                    error: options.error
                });
            };
        
        init();

        return {
            get: get,
            getSingle: getSingle,
            getForQuiz: getForQuiz,
            create: create,
            update: update,
            deleteItem: deleteItem
        };
    }
);
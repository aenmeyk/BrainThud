define('data-service.card', ['amplify', 'config', 'moment', 'underscore'],
    function (amplify, config, moment, _) {
        var
            init = function() {
                amplify.request.define('getCards', 'ajax', {
                    url: config.routes.cardDeckCards,
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
                        return true;
                    }
                }),
                amplify.request.define('updateCard', 'ajax', {
                    url: config.routes.cards,
                    dataType: 'json',
                    type: 'PUT',
                    contentType: 'application/json; charset=utf-8'
                });
                amplify.request.define('updateCardBatch', 'ajax', {
                    url: config.routes.cardsBatch,
                    dataType: 'json',
                    type: 'PUT',
                    contentType: 'application/json; charset=utf-8',
                    dataMap: function(data) {
                        return data.body;
                    }
                });
                amplify.request.define('deleteCards', 'ajax', {
                    url: config.routes.cards,
                    dataType: 'json',
                    type: 'DELETE',
                    contentType: 'application/json; charset=utf-8'
                });
            },

            get = function (options) {
                return amplify.request({
                    resourceId: 'getCards',
                    data: {
                        userIdPath: options.params.userId,
                        deckNameSlugPath: options.params.deckNameSlug
                    },
                    success: options.success,
                    error: options.error
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

            create = function (options) {
                return amplify.request({
                    resourceId: 'createCard',
                    data: options.data,
                    success: options.success,
                    error: options.error
                });
            },

            update = function (options) {
                return amplify.request({
                    resourceId: 'updateCard',
                    data: options.data,
                    success: options.success,
                    error: options.error
                });
            },

            updateBatch = function (options) {
                var content = {
                    userIdPath: options.params.userId,
                    body: JSON.stringify(options.data)
                };

                return amplify.request({
                    resourceId: 'updateCardBatch',
                    data: content,
                    success: options.success,
                    error: options.error
                });
            },

            // The $.extend( true, {}, defnSettings.data, data ) call that Amplify.js makes converts
            // an array to an object.  This prevents the serialized value from being received by MVC.
            // For deleteCards, the data is stringified before the Amplify.js call.
           deleteItem = function (options) {
                return amplify.request({
                    resourceId: 'deleteCards',
                    data: JSON.stringify(options.data),
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
            updateBatch: updateBatch,
            deleteItem: deleteItem
        };
    }
);
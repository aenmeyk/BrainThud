define('data-service.card', ['amplify', 'pubs', 'model.mapper'],
    function (amplify, pubs, modelMapper) {
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
                amplify.request.define('deleteCard', 'ajax', {
                    url: '/api/cards/{userId}/{entityId}',
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

            create = function (data, cache, callbacks) {
                return amplify.request({
                    resourceId: 'createCard',
                    data: data,
                    success: callbacks.success,
                    error: callbacks.error
                });
            },

            update = function (data, callbacks) {
                // TODO: Try to only use pub/sub
                var success = function (result) {
                    callbacks.success(result);
                    
                    var card = modelMapper.card.mapResult(result);
                    pubs.card.update(card);
                };

                return amplify.request({
                    resourceId: 'updateCard',
                    data: data,
                    success: success,
                    error: callbacks.error
                });
            },

            deleteItem = function (config) {
                return amplify.request({
                    resourceId: 'deleteCard',
                    data: {
                         entityId: config.params.entityId,
                         userId: config.params.userId
                    },
                    success: config.success,
                    error: config.error
                });
            };
        
        init();

        return {
            get: get,
            create: create,
            update: update,
            deleteItem: deleteItem
        };
    }
);
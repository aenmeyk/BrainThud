define('data-service.quiz-result', ['amplify'],
    function (amplify) {
        var
            init = function () {
                amplify.request.define('getQuizResult', 'ajax', {
                    url: '/api/quizzes/{userId}/{datePath}/results',
                    dataType: 'json',
                    type: 'GET'
                }),

                amplify.request.define('createQuizResult', 'ajax', {
                    url: '/api/quizzes/{userId}/{datePath}/results',
                    dataType: 'json',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("If-None-Match", "*");
                        return true;
                    },
                    decoder: function (data, status, xhr, success, error) {
                        if (status == "success") {
                            success(data);
                        } else {
                            error(xhr);
                        }
                    }
                });

                amplify.request.define('updateQuizResult', 'ajax', {
                    url: '/api/quizzes/{userId}/{datePath}/results',
                    dataType: 'json',
                    type: 'PUT',
                    contentType: 'application/json; charset=utf-8'
                });
            },

            get = function (callbacks) {
                return amplify.request({
                    resourceId: 'getQuizResult',
                    success: callbacks.success,
                    error: callbacks.error
                });
            },

            create = function (data, config) {
                return amplify.request({
                    resourceId: 'createQuizResult',
                    data: {
                        datePath: config.params.datePath,
                        userId: config.params.userId,
                        cardId: data.cardId,
                        isCorrect: data.isCorrect
                    },
                    success: config.success,
                    error: config.error
                });
            },

            update = function(data, callbacks) {
                // TODO: Try to only use pub/sub
                var success = function(result) {
                    callbacks.success(result);

                    var card = modelMapper.card.mapResult(result);
                    pubs.card.update(card);
                };

                return amplify.request({
                    resourceId: 'updateQuizResult',
                    data: data,
                    success: success,
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
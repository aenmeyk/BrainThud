define('data-service.quiz-result', ['amplify', 'model.mapper'],
    function (amplify, modelMapper) {
        var
            currentData,
            cachedResults,
            init = function () {
                amplify.request.define('getQuizResult', 'ajax', {
                    url: '/api/quizzes/{userId}/{datePath}/results',
                    dataType: 'json',
                    type: 'GET',
                    contentType: 'application/json; charset=utf-8'
                });

                amplify.request.define('createQuizResult', 'ajax', {
                    url: '/api/quizzes/{userId}/{datePath}/results/{cardId}',
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
                            // TODO: I think we need a custom header saying why the precondition failed.  If it is because
                            // the resource already exists then we can remove it from the cache before creating it again
                            
                            // If I already have the quiz result client-side, why would I ever do a POST first?
                        } else if (xhr.status == 412) {
                            var location = xhr.getResponseHeader("Location");
                            // TODO: we are not using amplify here.  How do we do that?
                            $.ajax({
                                url: location,
                                type: 'PUT',
                                data: currentData,
                                success: function(result) {
                                    var existingItem = _.find(cachedResults, function(item) {
                                        return item.quizDate() === result.quizDate
                                            && item.cardId() === result.cardId;
                                    });
                                    var index = _.indexOf(cachedResults, existingItem);
                                    cachedResults.splice(index, 1);
                                    modelMapper.quizResult.mapResults([result], cachedResults);
                                }
                            });
                        } else {
                            error();
                        }
                    }
                });

                amplify.request.define('updateQuizResult', 'ajax', {
                    url: '/api/quizzes/{userId}/{datePath}/results/{cardId}',
                    dataType: 'json',
                    type: 'PUT',
                    contentType: 'application/json; charset=utf-8'
                });
            },

            get = function (config) {
                return amplify.request({
                    resourceId: 'getQuizResult',
                    data: {
                        datePath: config.params.datePath,
                        userId: config.params.userId
                    },
                    success: config.success,
                    error: config.error
                });
            },

            create = function (data, cache, config) {
                currentData = data;
                cachedResults = cache;
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
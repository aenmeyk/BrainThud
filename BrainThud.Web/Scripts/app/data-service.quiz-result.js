define('data-service.quiz-result', ['underscore', 'amplify', 'model.mapper', 'config'],
    function (_, amplify, modelMapper, config) {
        var
            currentData,
            cachedResults,
            init = function () {
                amplify.request.define('getQuizResult', 'ajax', {
                    url: config.routes.quizResults,
                    dataType: 'json',
                    type: 'GET',
                    contentType: 'application/json; charset=utf-8'
                });

                amplify.request.define('createQuizResult', 'ajax', {
                    url: config.routes.quizResult,
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
                    url: config.routes.quizResult,
                    dataType: 'json',
                    type: 'PUT',
                    contentType: 'application/json; charset=utf-8'
                });
            },

            get = function (options) {
                return amplify.request({
                    resourceId: 'getQuizResult',
                    data: {
                        datePath: options.params.datePath,
                        userIdPath: options.params.userId
                    },
                    success: options.success,
                    error: options.error
                });
            },

            create = function (data, cache, options) {
                currentData = data;
                cachedResults = cache;
                return amplify.request({
                    resourceId: 'createQuizResult',
                    data: {
                        datePath: options.params.datePath,
                        userIdPath: options.params.userId,
                        cardIdPath: data.cardId,
                        isCorrect: data.isCorrect
                    },
                    success: options.success,
                    error: options.error
                });
            },

            // TODO: Standardize on options/data
            update = function (data, options) {
                var content = {
                    datePath: options.params.datePath,
                    userIdPath: options.params.userId,
                    cardIdPath: data.cardId,
                };

                _.extend(content, data.quizResult);
                
                return amplify.request({
                    resourceId: 'updateQuizResult',
                    data: content,
                    success: options.success,
                    error: options.error
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
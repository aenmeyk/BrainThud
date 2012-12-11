define('data-service.quiz', ['amplify'],
    function (amplify) {
        var
            init = function () {
                amplify.request.define('todaysQuiz', 'ajax', {
                    url: '/api/quizzes/{userId}/{datePath}',
                    dataType: 'json',
                    type: 'GET'
                });
            },
        
            get = function (config) {
                return amplify.request({
                    resourceId: 'todaysQuiz',
                    data: {
                        datePath: config.params.datePath,
                        userId: '19'
                    },
                    success: config.success,
                    error: config.error
                });
            };
        
        init();

        return {
            get: get
        };
    }
);

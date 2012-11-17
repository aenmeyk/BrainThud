define('data-service.quiz', ['amplify'],
    function (amplify) {
        var
            init = function () {
                amplify.request.define('todaysQuiz', 'ajax', {
                    url: '/api/quizzes/{datePath}',
                    dataType: 'json',
                    type: 'GET'
                });
            },
        
            get = function (config) {
                return amplify.request({
                    resourceId: 'todaysQuiz',
                    data: { datePath: config.params.datePath },
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

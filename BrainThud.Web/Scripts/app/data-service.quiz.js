define('data-service.quiz', ['amplify'],
    function (amplify) {
        var
            init = function () {
                var today = new Date();
                var year = today.getFullYear();
                var month = today.getMonth() + 1;
                var day = today.getDate();
                amplify.request.define('todaysQuiz', 'ajax', {
                    url: '/api/quizzes/' + year  + '/' + month + '/' + day,
                    dataType: 'json',
                    type: 'GET'
                });
            },

            get = function (callbacks) {
                return amplify.request({
                    resourceId: 'todaysQuiz',
                    success: callbacks.success,
                    error: callbacks.error
                });
            };
        
        init();

        return {
            get: get
        };
    }
);
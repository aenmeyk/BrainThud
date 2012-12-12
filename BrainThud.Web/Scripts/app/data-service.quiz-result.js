define('data-service.quiz-result', ['amplify'],
    function (amplify) {
        var
            init = function () {
                
                amplify.request.define('createQuizResult', 'ajax', {
                    url: '/api/quizzes/{userId}/{datePath}/results',
                    dataType: 'json',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8'
                });
            },

            save = function (data, config) {
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
            };
        
        init();

        return {
            save: save
        };
    }
);
define('data-service', ['data-service.card', 'data-service.quiz-result', 'data-service.user-configuration'],
    function (card, quizResult, userConfiguration) {
        
        var init = function() {
            amplify.subscribe("request.ajax.preprocess", function(defnSettings, settingValues, ajaxSettings) {
                ajaxSettings.data = JSON.stringify(ajaxSettings.data);
            });
        };

        init();

        return {
            card: card,
            quizResult: quizResult,
            userConfiguration: userConfiguration
        };
    }
);
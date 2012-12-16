define('data-service', ['data-service.card', 'data-service.quiz', 'data-service.quiz-result'],
    function (card, quiz, quizResult) {
        
        var init = function() {
            amplify.subscribe("request.ajax.preprocess", function(defnSettings, settings, ajaxSettings) {
                ajaxSettings.data = JSON.stringify(ajaxSettings.data);
            });
        };

        init();

        return {
            card: card,
            quiz: quiz,
            quizResult: quizResult
        };
    }
);
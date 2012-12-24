define('data-service', ['data-service.card', 'data-service.quiz', 'data-service.quiz-card', 'data-service.quiz-result', 'data-service.config'],
    function (card, quiz, quizCard, quizResult, config) {
        
        var init = function() {
            amplify.subscribe("request.ajax.preprocess", function(defnSettings, settings, ajaxSettings) {
                ajaxSettings.data = JSON.stringify(ajaxSettings.data);
            });
        };

        init();

        return {
            card: card,
            quiz: quiz,
            quizCard: quizCard,
            quizResult: quizResult,
            config: config
        };
    }
);
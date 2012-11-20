define('data-service', ['data-service.card', 'data-service.quiz', 'data-service.quiz-result'],
    function (cards, quiz, quizResult) {
        
        var init = function() {
            amplify.subscribe("request.ajax.preprocess", function(defnSettings, settings, ajaxSettings) {
                ajaxSettings.data = JSON.stringify(ajaxSettings.data);
            });
        };

        init();

        return {
            cards: cards,
            quiz: quiz,
            quizResult: quizResult
        };
    }
);
define('data-service', ['data-service.card', 'data-service.card-deck', 'data-service.quiz-result', 'data-service.user-configuration', 'amplify'],
    function (card, cardDeck, quizResult, userConfiguration, amplify) {
        
        var init = function() {
            amplify.request.decoders._default = function(data, status, xhr, success, error) {
                if (status == "success") {
                    success(data);
                } else {
                    error(xhr);
                }
            };

            amplify.subscribe("request.ajax.preprocess", function (defnSettings, settingValues, ajaxSettings) {
                ajaxSettings.data = JSON.stringify(ajaxSettings.data);
            });
        };

        init();

        return {
            card: card,
            cardDeck: cardDeck,
            quizResult: quizResult,
            userConfiguration: userConfiguration
        };
    }
);
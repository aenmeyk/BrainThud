define('data-service', ['data-service.card', 'data-service.card-deck', 'data-service.quiz-result', 'data-service.user-configuration', 'amplify'],
    function (card, cardDeck, quizResult, userConfiguration, amplify) {
        
        var init = function() {
            amplify.request.decoders._default = function(data, status, xhr, success, error) {
                if (status === "error" || status === "fail") {
                    error(xhr);
                } else {
                    success(data);
                }
            };

            amplify.subscribe("request.ajax.preprocess", function (defnSettings, settingValues, ajaxSettings) {
                // The $.extend( true, {}, defnSettings.data, data ) call that Amplify.js makes converts
                // an array to an object.  This prevents the serialized value from being received by MVC.
                // For deleteCards, the data is stringified before the Amplify.js call.
                if (defnSettings.resourceId !== "deleteCards" && defnSettings.type !== "DELETE") {
                    ajaxSettings.data = JSON.stringify(ajaxSettings.data);
                }
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
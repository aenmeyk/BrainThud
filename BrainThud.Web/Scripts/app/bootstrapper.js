define('bootstrapper', ['jquery', 'binder', 'route-config', 'data-primer', 'gestures', 'markdown'],

function ($, binder, routeConfig, dataPrimer, gestures, markdown) {
    var run = function () {
        initializeEditors();
        $.when(dataPrimer.fetch())
            .done(binder.bind)
            .done(function() {
                if (Modernizr.touch) {
                    gestures.register();
                }
                routeConfig.register();
            });
        },

        initializeEditors = function () {
            var converter = markdown.getSanitizingConverter(),
                questionEditor = new Markdown.Editor(converter, "-question"),
                answerEditor = new Markdown.Editor(converter, "-answer");
            
            questionEditor.run();
            answerEditor.run();
        };

    return {
        run: run
    };
});
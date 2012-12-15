define('bootstrapper', ['jquery', 'binder', 'route-config', 'data-primer', 'gestures'],

function ($, binder, routeConfig, dataPrimer, gestures) {
    var run = function () {
        initializeEditors();
        $.when(dataPrimer.fetch())
            .done(binder.bind)
            .done(function() {
                gestures.register();
                routeConfig.register();
            });
        },

        initializeEditors = function () {
            var
                converter = Markdown.getSanitizingConverter(),
                questionEditor = new Markdown.Editor(converter, "-question"),
                answerEditor = new Markdown.Editor(converter, "-answer");
            
            questionEditor.run();
            answerEditor.run();
        };

    return {
        run: run
    };
});
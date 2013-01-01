define('vm.create-card', ['ko', 'editor', 'dom', 'model', 'amplify', 'config'],
    function (ko, editor, dom, model, amplify, config) {
        var
            card = ko.observable(new model.Card()),

            init = function () {
                amplify.subscribe(config.pubs.createCard, function () {
                    dom.resetNewCard();
                    editor.refreshPreview('create');
                });
            },

            activate = function () { },

            createCard = function () {
                var newCard = {
                    quizDate: new Date(),
                    level: 0
                };
                dom.getCardValues(newCard, 'create');
                amplify.publish(config.pubs.createNewCard, newCard);
            };

        init();

        return {
            activate: activate,
            card: card,
            createCard: createCard
        };
    }
);
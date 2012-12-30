define('vm.quiz-card', ['jquery', 'underscore', 'ko', 'data-context', 'utils', 'router', 'amplify', 'config', 'model'],
    function ($, _, ko, dataContext, utils, router, amplify, config, model) {
        var
            init = function () {
            },

            card = ko.observable(new model.Card()),

            activate = function(routeData) {
                var existingCards = ko.observableArray([]);
                $.when(dataContext.card.getData({ results: existingCards }))
                    .then(function () {
                        var matchingCard = _.find(existingCards(), function(item) {
                            return item.entityId() === routeData.cardId;
                        });
                        card(matchingCard);
                    });
            };
        
        init();

        return {
            activate: activate,
            card: card,
            showNextCard: function () { },
            showPreviousCard: function () { },
            cardCount: function () { },
            flipCard: function () { },
            submitCorrect: function () { },
            submitIncorrect: function () { },
            displayIndex: function () { },
        };
    }
);

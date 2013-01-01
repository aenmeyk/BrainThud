define('vm.cards', ['ko', 'amplify', 'config'],
    function (ko, amplify, config) {
        var
            cards = ko.observableArray([]),

            init = function () {
                amplify.subscribe(config.pubs.cardCacheChanged, function (data) {
                    cards(data);
                });
            },

            editCard = function (card) {
                amplify.publish(config.pubs.showEditCard, card.entityId());
            },

            flipCard = function (card) {
                card.questionSideVisible(!card.questionSideVisible());
            },

            activate = function () { },

            showDeleteDialog = function (card) {
                amplify.publish(config.pubs.showDeleteCard, card);
            };

        init();

        return {
            cards: cards,
            editCard: editCard,
            flipCard: flipCard,
            activate: activate,
            showDeleteDialog: showDeleteDialog
        };
    }
);
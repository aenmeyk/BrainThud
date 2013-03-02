define('model.card-deck', ['ko'],
    function (ko) {
        var CardDeck = function() {
            var self = this;
            self.partitionKey = ko.observable();
            self.rowKey = ko.observable();
            self.timestamp = ko.observable();
            self.createdTimestamp = ko.observable();
            self.deckName = ko.observable();
            self.deckNameSlug = ko.observable();
            self.cardIds = ko.observable();
            self.cardCount = ko.observable();
        };

        return CardDeck;
    }
);
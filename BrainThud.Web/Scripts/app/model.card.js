define('model.card', ['ko'],
    function(ko) {
        var Card = function() {
            var self = this;
            self.partitionKey = ko.observable();
            self.rowKey = ko.observable();
            self.question = ko.observable();
            self.answer = ko.observable();
            self.deckName = ko.observable();
            self.tags = ko.observable();
            self.userId = ko.observable();
        };

        return Card;
    }
);
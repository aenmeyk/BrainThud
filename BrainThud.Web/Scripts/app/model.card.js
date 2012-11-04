define('model.card', ['ko'],
    function(ko) {
        var Card = function() {
            var self = this;
            self.partitionKey = ko.observable();
            self.rowKey = ko.observable();
            self.question = ko.observable();
            self.answer = ko.observable();
        };

        return Card;
    }
);
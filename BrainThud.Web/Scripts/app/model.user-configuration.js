define('model.user-configuration', ['ko'],
    function (ko) {
        var UserConfiguration = function() {
            var self = this;
            self.partitionKey = ko.observable();
            self.rowKey = ko.observable();
            self.timestamp = ko.observable();
            self.createdTimestamp = ko.observable();
            self.userId = ko.observable();
            self.quizInterval0 = ko.observable();
            self.quizInterval1 = ko.observable();
            self.quizInterval2 = ko.observable();
            self.quizInterval3 = ko.observable();
            self.quizInterval4 = ko.observable();
            self.quizInterval5 = ko.observable();
        };

        return UserConfiguration;
    }
);
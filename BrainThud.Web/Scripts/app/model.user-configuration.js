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
            self.quizInterval0DayLabel = ko.computed(function() {
                return (self.quizInterval0() === 1) ? 'day' : 'days';
            });
            self.quizInterval1DayLabel = ko.computed(function() {
                return (self.quizInterval1() === 1) ? 'day' : 'days';
            });
            self.quizInterval2DayLabel = ko.computed(function() {
                return (self.quizInterval2() === 1) ? 'day' : 'days';
            });
            self.quizInterval3DayLabel = ko.computed(function() {
                return (self.quizInterval3() === 1) ? 'day' : 'days';
            });
            self.quizInterval4DayLabel = ko.computed(function() {
                return (self.quizInterval4() === 1) ? 'day' : 'days';
            });
            self.quizInterval5DayLabel = ko.computed(function() {
                return (self.quizInterval5() === 1) ? 'day' : 'days';
            });
            self.dirtyFlag = new ko.DirtyFlag([
                self.quizInterval0,
                self.quizInterval1,
                self.quizInterval2,
                self.quizInterval3,
                self.quizInterval4,
                self.quizInterval5
            ]);
        };

        return UserConfiguration;
    }
);
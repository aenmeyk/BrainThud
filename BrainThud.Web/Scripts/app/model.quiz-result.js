define('model.quiz-result', ['ko'],
    function (ko) {
        var QuizResult = function() {
            var self = this;
            self.partitionKey = ko.observable();
            self.rowKey = ko.observable();
            self.timestamp = ko.observable();
            self.createdTimestamp = ko.observable();
            self.quizDate = ko.observable();
            self.quizYear = ko.observable();
            self.quizMonth = ko.observable();
            self.quizDay = ko.observable();
            self.cardId = ko.observable();
            self.isCorrect = ko.observable();
            self.userId = ko.observable();
            self.entityId = ko.observable();
            self.dirtyFlag = new ko.DirtyFlag([
                self.isCorrect
            ]);
        };

        return QuizResult;
    }
);
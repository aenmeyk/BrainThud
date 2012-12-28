define('model.quiz-result', ['ko'],
    function (ko) {
        var QuizResult = function() {
            var self = this;
            self.partitionKey = ko.observable();
            self.rowKey = ko.observable();
            self.quizDate = ko.observable();
            self.cardId = ko.observable();
            self.isCorrect = ko.observable();
            self.userId = ko.observable();
            self.entityId = ko.observable();
        };

        return QuizResult;
    }
);
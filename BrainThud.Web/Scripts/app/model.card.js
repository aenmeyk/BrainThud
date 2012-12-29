define('model.card', ['ko', 'editor'],
    function (ko, editor) {
        var Card = function() {
            var self = this;
            self.partitionKey = ko.observable();
            self.rowKey = ko.observable();
            self.deckName = ko.observable();
            self.tags = ko.observable();
            self.question = ko.observable();
            self.answer = ko.observable();
            self.quizDate = ko.observable();
            self.level = ko.observable();
            self.userId = ko.observable();
            self.entityId = ko.observable();
            self.questionSideVisible = ko.observable(true);
            self.questionHtml = ko.computed(function () {
                return self.question() ? editor.makeHtml(self.question()) : '';
            });
            self.answerHtml = ko.computed(function () {
                return self.answer() ? editor.makeHtml(self.answer()) : '';
            });
            self.currentSideText = ko.computed(function () {
                return self.questionSideVisible() ? self.questionHtml() : self.answerHtml();
            });
        };

        return Card;
    }
);
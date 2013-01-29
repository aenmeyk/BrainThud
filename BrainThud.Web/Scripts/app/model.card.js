define('model.card', ['ko', 'editor', 'moment'],
    function (ko, editor, moment) {
        var Card = function() {
            var self = this;
            self.partitionKey = ko.observable();
            self.rowKey = ko.observable();
            self.timestamp = ko.observable();
            self.createdTimestamp = ko.observable();
            self.deckName = ko.observable();
            self.deckNameSlug = ko.observable();
            self.tags = ko.observable();
            self.question = ko.observable();
            self.answer = ko.observable();
            self.quizDate = ko.observable();
            self.level = ko.observable();
            self.userId = ko.observable();
            self.entityId = ko.observable();
            self.isCorrect = ko.observable();
            self.completedQuizYear = ko.observable();
            self.completedQuizMonth = ko.observable();
            self.completedQuizDay = ko.observable();
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
            self.libraryCardBorder = ko.computed(function () {
                if (!self.questionSideVisible()) return 'answer';
                
                // If the completedQuizYear has been set, it means the user has answered this card
                if (self.completedQuizYear() > 0) {
                    return self.isCorrect() ? 'correct' : 'incorrect';
                }

                return 'question';
            });
            self.localizedQuizDate = ko.computed(function () {
                return moment(self.quizDate()).format('L');
            });
            self.dirtyFlag = new ko.DirtyFlag([
                self.deckName,
                self.tags,
                self.question,
                self.answer
            ]);
        };

        return Card;
    }
);
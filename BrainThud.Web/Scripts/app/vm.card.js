define('vm.card', ['jquery', 'ko', 'data-context', 'presenter', 'toastr', 'markdown'],
    function ($, ko, dataContext, presenter, toastr, markdown) {
        var
            questionEditor,
            answerEditor,
            question = ko.observable(''),
            answer = ko.observable(''),
            deckName = ko.observable(''),
            tags = ko.observable(''),

            saveCard = function () {
                $.when(dataContext.card.saveData({
                    data: {
                        question: question(),
                        answer: answer(),
                        deckName: deckName(),
                        tags: tags(),
                        quizDate: new Date(),
                        level: 0
                    }}))
                    .then(function() {
                        toastr.success('Success!');
                        createNewCard();
                    });
            },

            activate = function () {
                var converter = markdown.getSanitizingConverter();

                questionEditor = new Markdown.Editor(converter, "-question");
                answerEditor = new Markdown.Editor(converter, "-answer");

                questionEditor.run();
                answerEditor.run();
            },

            createNewCard = function () {
                question('');
                answer('');
                questionEditor.refreshPreview();
                answerEditor.refreshPreview();
            };

        return {
            activate: activate,
            question: question,
            answer: answer,
            deckName: deckName,
            tags: tags,
            saveCard: saveCard
        };
    }
);
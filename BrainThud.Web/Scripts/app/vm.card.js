define('vm.card', ['jquery', 'ko', 'data-context', 'presenter', 'toastr', 'markdown', 'dom'],
    function ($, ko, dataContext, presenter, toastr, markdown, dom) {
        var
            init = function () {
                if (dom.isCreateCardRendered()) {
                    var converter = markdown.getSanitizingConverter();

                    questionEditor = new Markdown.Editor(converter, "-question");
                    answerEditor = new Markdown.Editor(converter, "-answer");

                    questionEditor.run();
                    answerEditor.run();
                }
            },
            
            questionEditor,
            answerEditor,
            question = ko.observable(''),
            answer = ko.observable(''),
            deckName = ko.observable(''),
            tags = ko.observable(''),

            saveCard = function () {
                var cardData = {
                    quizDate: new Date(),
                    level: 0
                };
                dom.getNewCardValues(cardData);
                $.when(dataContext.card.saveData({
                    data: cardData
                }))
                    .then(function() {
                        toastr.success('Success!');
                        createNewCard();
                    });
            },

            activate = function () {
                // do nothing
            },

            createNewCard = function () {
                dom.resetNewCard();
                questionEditor.refreshPreview();
                answerEditor.refreshPreview();
            };

        init();

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
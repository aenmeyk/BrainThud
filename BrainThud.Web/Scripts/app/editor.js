define('editor', ['markdown'],
    
function (markdown) {
    var
        markDownConverter,
        questionEditorCreate,
        questionEditorEdit,
        answerEditorCreate,
        answerEditorEdit,

        init = function () {
            markDownConverter = markdown.getSanitizingConverter();

            questionEditorCreate = new Markdown.Editor(markDownConverter, "-question-create");
            questionEditorEdit = new Markdown.Editor(markDownConverter, "-question-edit");
            answerEditorCreate = new Markdown.Editor(markDownConverter, "-answer-create");
            answerEditorEdit = new Markdown.Editor(markDownConverter, "-answer-edit");

            questionEditorCreate.run();
            questionEditorEdit.run();
            answerEditorCreate.run();
            answerEditorEdit.run();
        },

        refreshPreview = function (editor) {
            if (editor === 'create') {
                questionEditorCreate.refreshPreview();
                answerEditorCreate.refreshPreview();
            } else if (editor === 'edit') {
                questionEditorEdit.refreshPreview();
                answerEditorEdit.refreshPreview();
            };
        },

        makeHtml = function (text) {
            return markDownConverter.makeHtml(text);
        };

    init();

    return {
        refreshPreview: refreshPreview,
        makeHtml: makeHtml
    };
});
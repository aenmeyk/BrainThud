var converter = Markdown.getSanitizingConverter();
$('.markdown').each(function() {
    var elem = $(this);
    elem.html(converter.makeHtml(elem.html()));
});

var flipCard = function(entityId) {
    $('#' + entityId + ' > .question').toggleClass('hidden');
    $('#' + entityId + ' > .answer').toggleClass('hidden');
};


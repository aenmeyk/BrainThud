var converter = Markdown.getSanitizingConverter();
$('.markdown').each(function() {
    var elem = $(this);
    var decoded = $("<div/>").html(elem.html()).text();
    elem.html(converter.makeHtml(decoded));
});

var flipCard = function(entityId) {
    $('#' + entityId + ' > .question').toggleClass('hidden');
    $('#' + entityId + ' > .answer').toggleClass('hidden');
};


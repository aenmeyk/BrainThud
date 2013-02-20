var converter = Markdown.getSanitizingConverter();
$('.markdown').each(function() {
    var elem = $(this);
    elem.html(converter.makeHtml(elem.html()));
})
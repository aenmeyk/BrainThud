define('vm.card', ['jquery', 'ko', 'data-context', 'presenter', 'toastr', 'dom', 'editor'],
    function ($, ko, dataContext, presenter, toastr, dom, editor) {
        var
            cards = ko.observableArray(),
            dataOptions = function () {
                return {
                    results: cards
                };
            },
            partitionKey = ko.observable(''),
            rowKey = ko.observable(''),
            deckName = ko.observable(''),
            tags = ko.observable(''),
            question = ko.observable(''),
            answer = ko.observable(''),
            quizDate = ko.observable(''),
            level = ko.observable(''),
            entityId = ko.observable(''),
            userId = ko.observable(''),

            updateCard = function () {
                var cardData = {
                    partitionKey: partitionKey(),
                    rowKey: rowKey(),
                    quizDate: quizDate(),
                    level: level(),
                    entityId: entityId(),
                    userId: userId()
                };
                dom.getCardValues(cardData, 'edit');
                $.when(dataContext.card.updateData({
                    data: cardData
                }))
                    .then(function () {
                        toastr.success('Success!');
                    });
            },

            activate = function (routeData) {
                $.when(dataContext.card.getData(dataOptions()))
                    .then(function () {
                        for (var i = 0; i < cards().length; i++) {
                            if (cards()[i].cardId() === parseInt(routeData.cardId)) {
                                // TODO: Move this to the mapper
                                var card = cards()[i];
                                partitionKey(card.partitionKey());
                                rowKey(card.rowKey());
                                deckName(card.deckName());
                                tags(card.tags());
                                question(card.question());
                                answer(card.answer());
                                quizDate(card.quizDate());
                                level(card.level());
                                entityId(card.cardId());
                                userId(card.userId());
                            }
                        };
                    })
                    .then(function () {
                        editor.refreshPreview('edit');
                    });
            };

        return {
            activate: activate,
            deckName: deckName,
            tags: tags,
            question: question,
            answer: answer,
            updateCard: updateCard
        };
    }
);
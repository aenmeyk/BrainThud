﻿define('vm.card', ['jquery', 'ko', 'data-context', 'presenter', 'toastr'],
    function ($, ko, dataContext, presenter, toastr) {
        var question = ko.observable(''),
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
                // do nothing
            },

            createNewCard = function () {
                question('');
                answer('');
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
﻿define('config', ['toastr'],
    function (toastr) {
        var
            hashes = {
                cardEdit: '#/cards/:cardId/edit',
                createCard: '#/create-card',
                cards: '#/cards',
                quiz: '#/quizzes/:userId/:year/:month/:day',
                quizCard: '#/quizzes/:userId/:year/:month/:day/:cardId'
            },

            viewIds = {
                card: '#card-view',
                createCard: '#create-card-view',
                cards: '#cards-view',
                quiz: '#quiz-view',
                quizCard: '#quiz-card-view',
                nav: '#nav-view'
            },

            pubs = {
                addQuizResult: 'add-quiz-result'
            };

        toastr.options.timeOut = 1500;

        return {
            hashes: hashes,
            viewIds: viewIds,
            pubs: pubs
        };
    }
);
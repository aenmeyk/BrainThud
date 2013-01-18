﻿define('dom', ['jquery', 'utils'],
    function ($, utils) {
        var
            getCardValues = function (card, editorName) {
                card.deckName = $('#new-card-deckname-' + editorName).val();
                card.tags = $('#new-card-tags-' + editorName).val();
                card.question = $('#wmd-input-question-' + editorName).val();
                card.answer = $('#wmd-input-answer-' + editorName).val();
            },

            resetNewCard = function () {
                $('#wmd-input-question-create').val('');
                $('#wmd-input-answer-create').val('');
            },

            isCreateCardRendered = function () {
                return $('#wmd-input-question-create').length != 0;
            };

        var $cardDeckChevron = $('#card-deck-chevron');
        var $cardDeckContainer = $('.card-deck-container');

        $('.card-deck-name').toggle(function () {
            $cardDeckContainer.collapse('toggle');
            setTimeout(function () {
                $cardDeckChevron.addClass("icon-chevron-up");
                $cardDeckChevron.removeClass("icon-chevron-down");
            }, 100);
        }, function () {
            $cardDeckContainer.collapse('toggle');
            setTimeout(function () {
                $cardDeckChevron.addClass("icon-chevron-down");
                $cardDeckChevron.removeClass("icon-chevron-up");
            }, 100);
        });

        return {
            getCardValues: getCardValues,
            resetNewCard: resetNewCard,
            isCreateCardRendered: isCreateCardRendered
        };
    }
);
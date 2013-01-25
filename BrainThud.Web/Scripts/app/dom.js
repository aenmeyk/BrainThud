define('dom', ['jquery'],
    function ($) {
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
            },
            
            showLoading = function() {
                $('#loading').css({
                    height: "100px"
                });
            },
            
            hideLoading = function () {
                $('#loading').animate({
                    height: "0px"
                }, 400);
            },
            
            $cardDeckChevron = $('#card-deck-chevron'),
            $cardDeckContainer = $('.card-deck-container'),
            $cardDeckName = $('.card-deck-name');
        $("#slider").slider();
        $cardDeckName.click(function () {
            $cardDeckContainer.collapse('toggle');
        });
        
        $cardDeckName.toggle(function () {
            setTimeout(function () {
                $cardDeckChevron.addClass("icon-chevron-up");
                $cardDeckChevron.removeClass("icon-chevron-down");
            }, 100);
        }, function () {
            setTimeout(function () {
                $cardDeckChevron.addClass("icon-chevron-down");
                $cardDeckChevron.removeClass("icon-chevron-up");
            }, 100);
        });

        return {
            getCardValues: getCardValues,
            resetNewCard: resetNewCard,
            isCreateCardRendered: isCreateCardRendered,
            showLoading: showLoading,
            hideLoading: hideLoading
        };
    }
);
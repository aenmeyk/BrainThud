﻿define('dom', ['jquery', 'ko'],
    function ($, ko) {
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
//        $("#level0Slider").slider({
//            range: "min",
//            value: 30,
//            min: 0,
//            max: 365,
//            slide: function (event, ui) {
//                $("#level0Interval").val(ui.value);
//            }
//        });
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

        ko.bindingHandlers.slider = {
            init: function (element, valueAccessor, allBindingsAccessor) {
                var options = allBindingsAccessor().sliderOptions || {};
                $(element).slider(options);
                ko.utils.registerEventHandler(element, "slidechange", function (event, ui) {
                    var observable = valueAccessor();
                    observable(ui.value);
                });
                ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                    $(element).slider("destroy");
                });
                ko.utils.registerEventHandler(element, "slide", function (event, ui) {
                    var observable = valueAccessor();
                    observable(ui.value);
                });
            },
            update: function (element, valueAccessor) {
                var value = ko.utils.unwrapObservable(valueAccessor());
                if (isNaN(value)) value = 0;
                $(element).slider("value", value);

            }
        };

        return {
            getCardValues: getCardValues,
            resetNewCard: resetNewCard,
            isCreateCardRendered: isCreateCardRendered,
            showLoading: showLoading,
            hideLoading: hideLoading
        };
    }
);

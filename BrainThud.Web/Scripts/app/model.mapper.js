﻿define('model.mapper', ['model'],
    function(model) {
        var
            quiz = {
                mapResults: function (dto, results) {
                    var cards = [];

                    for (var i = 0; i < dto.cards.length; i++) {
                        cards.push(getCardFromDto(dto.cards[i]));
                    }

                    results.push({
                        cards: cards,
                        resultsUri: dto.resultsUri,
                        userId: dto.userId
                    });
                }
            },
            
            card = {
                mapResults: function (dto, results) {
                    for (var i = 0; i < dto.length; i++) {
                        results.push(getCardFromDto(dto[i]));
                    }
                }
            },

            getCardFromDto = function(dto) {
                var singleCard = new model.Card();
                singleCard.partitionKey(dto.partitionKey)
                    .rowKey(dto.rowKey)
                    .question(dto.question)
                    .answer(dto.answer)
                    .deckName(dto.deckName)
                    .tags(dto.tags)
                    .userId(dto.userId)
                    .cardId(dto.entityId);
                
                return singleCard;
            };

        return {
            card: card,
            quiz: quiz
        };
    }
);
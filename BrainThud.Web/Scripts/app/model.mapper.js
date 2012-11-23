define('model.mapper', ['model'],
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
                        requestsUri: dto.resultsUri
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
                    .answer(dto.deckName)
                    .answer(dto.tags);
                
                return singleCard;
            };

        return {
            card: card,
            quiz: quiz
        };
    }
);
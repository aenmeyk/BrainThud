define('model.mapper', ['model', 'editor'],
    function (model, editor) {
        var
            quizCard = {
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
            
            cardHtml = {
                mapResults: function (dto, results) {
                    for (var i = 0; i < dto.length; i++) {
                        results.push(getCardFromDtoHtml(dto[i]));
                    }
                }
            },
            
            config = {
                mapResults: function (dto, results) {
                    results.push(dto);
                }
            },

            getCardFromDto = function(dto) {
                var singleCard = new model.Card();
                singleCard.partitionKey(dto.partitionKey)
                    .rowKey(dto.rowKey)
                    .deckName(dto.deckName)
                    .tags(dto.tags)
                    .question(dto.question)
                    .answer(dto.answer)
                    .quizDate(dto.quizDate)
                    .level(dto.level)
                    .userId(dto.userId)
                    .cardId(dto.entityId);
                
                return singleCard;
            },

            getCardFromDtoHtml = function(dto) {
                var singleCard = getCardFromDto(dto);
                singleCard.question(editor.makeHtml(dto.question))
                          .answer(editor.makeHtml(dto.answer));
                return singleCard;
            };

        return {
            card: card,
            cardHtml: cardHtml,
            quizCard: quizCard,
            config: config
        };
    }
);
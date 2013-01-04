define('model.mapper', ['model', 'editor', 'utils'],
    function (model, editor, utils) {
        var quiz = {
            mapResults: function (dto, results) {
                    var quizResults = [];

                    for (var i = 0; i < dto.quizResults.length; i++) {
                        
                        // TODO: Why is quizResult being mapped from getCardFromDto?
                        quizResults.push(getCardFromDto(dto.quizResults[i]));
                    }

                    results.push({
                        cardIds: dto.cardIds,
                        resultsUri: dto.resultsUri,
                        userId: dto.userId,
                        quizDate: dto.quizDate,
                        quizResults: dto.quizResults
                    });
                }
            },
            card = {
                mapResults: function(dto, results) {
                    for (var i = 0; i < dto.length; i++) {
                        if (!utils.entityExists(results, dto)) {
                            results.push(getCardFromDto(dto[i]));
                        }
                    }
                },
                mapResult: function(dto) {
                    return getCardFromDto(dto);
                }
            },

            quizResult = {
                mapResults: function(dto, results) {
                    for (var i = 0; i < dto.length; i++) {
                        if (!utils.entityExists(results, dto)) {
                            var singleQuizResult = new model.QuizResult();
                            singleQuizResult.partitionKey(dto[i].partitionKey)
                                .rowKey(dto[i].rowKey)
                                .quizDate(dto[i].quizDate)
                                .cardId(dto[i].cardId)
                                .isCorrect(dto[i].isCorrect)
                                .userId(dto[i].userId)
                                .entityId(dto[i].entityId);

                            results.push(singleQuizResult);
                        }
                    }
                }
            },

            config = {
                mapResults: function(dto, results) {
                    if (!utils.entityExists(results, dto)) {
                        results.push(dto);
                    }
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
                    .entityId(dto.entityId);

                return singleCard;
            };

        return {
            card: card,
            quiz: quiz,
            quizResult: quizResult,
            config: config
        };
    }
);
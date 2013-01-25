define('model.mapper', ['model', 'editor', 'utils'],
    function (model, editor, utils) {
        var 
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
                            results.push(getQuizResultFromDto(dto[i]));
                        }
                    }
                },
                mapResult: function (dto) {
                    return getQuizResultFromDto(dto);
                }
            },

            userConfiguration = {
                mapResults: function (dto, results) {
                    if (!utils.entityExists(results, dto)) {
                        results.push(getUserConfigurationFromDto(dto));
                    }
                }
            },
            
            getUserConfigurationFromDto = function (dto) {
                var singleUserConfiguration = new model.UserConfiguration();
                singleUserConfiguration.partitionKey(dto.partitionKey)
                    .rowKey(dto.rowKey)
                    .timestamp(dto.timestamp)
                    .createdTimestamp(dto.createdTimestamp)
                    .userId(dto.userId)
                    .quizInterval0(dto.quizInterval0)
                    .quizInterval1(dto.quizInterval1)
                    .quizInterval2(dto.quizInterval2)
                    .quizInterval3(dto.quizInterval3)
                    .quizInterval4(dto.quizInterval4)
                    .quizInterval5(dto.quizInterval5);

                return singleUserConfiguration;
            },
            
            getCardFromDto = function(dto) {
                var singleCard = new model.Card();
                singleCard.partitionKey(dto.partitionKey)
                    .rowKey(dto.rowKey)
                    .timestamp(dto.timestamp)
                    .createdTimestamp(dto.createdTimestamp)
                    .deckName(dto.deckName)
                    .deckNameSlug(dto.deckNameSlug)
                    .tags(dto.tags)
                    .question(dto.question)
                    .answer(dto.answer)
                    .quizDate(dto.quizDate)
                    .level(dto.level)
                    .userId(dto.userId)
                    .entityId(dto.entityId);

                return singleCard;
            },
            
            getQuizResultFromDto = function(dto) {
                var singleQuizResult = new model.QuizResult();
                singleQuizResult.partitionKey(dto.partitionKey)
                    .rowKey(dto.rowKey)
                    .timestamp(dto.timestamp)
                    .createdTimestamp(dto.createdTimestamp)
                    .quizDate(dto.quizDate)
                    .cardId(dto.cardId)
                    .isCorrect(dto.isCorrect)
                    .userId(dto.userId)
                    .entityId(dto.entityId);

                return singleQuizResult;
            };

        return {
            card: card,
            quizResult: quizResult,
            userConfiguration: userConfiguration
        };
    }
);
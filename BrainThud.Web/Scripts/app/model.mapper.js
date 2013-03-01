﻿define('model.mapper', ['model', 'utils'],
    function (model, utils) {
        var 
            card = {
                mapResults: function(dto, results) {
                    for (var i = 0; i < dto.length; i++) {
                        // TODO: If the entity exists, remove it then add it again
                        if (!utils.entityExists(results, dto)) {
                            results.push(getCardFromDto(dto[i]));
                        }
                    }
                },
                mapResult: function(dto) {
                    return getCardFromDto(dto);
                }
            },

            cardDeck = {
                mapResults: function(dto, results) {
                    for (var i = 0; i < dto.length; i++) {
                        if (!utils.entityExists(results, dto)) {
                            results.push(getCardDeckFromDto(dto[i]));
                        }
                    }
                },
                mapResult: function(dto) {
                    return getCardDeckFromDto(dto);
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
                },
                mapResult: function (dto) {
                    return getUserConfigurationFromDto(dto);
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

                singleUserConfiguration.dirtyFlag().reset();
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
                    .entityId(dto.entityId)
                    .isCorrect(dto.isCorrect)
                    .completedQuizYear(dto.completedQuizYear)
                    .completedQuizMonth(dto.completedQuizMonth)
                    .completedQuizDay(dto.completedQuizDay);

                singleCard.dirtyFlag().reset();
                return singleCard;
            },
            
            getCardDeckFromDto = function (dto) {
                var singleCardDeck = new model.CardDeck();
                singleCardDeck.partitionKey(dto.partitionKey)
                    .rowKey(dto.rowKey)
                    .timestamp(dto.timestamp)
                    .createdTimestamp(dto.createdTimestamp)
                    .deckName(dto.deckName)
                    .deckNameSlug(dto.deckNameSlug)
                    .cardIds(dto.cardIds);

                return singleCardDeck;
            },
            
            getQuizResultFromDto = function(dto) {
                var singleQuizResult = new model.QuizResult();
                singleQuizResult.partitionKey(dto.partitionKey)
                    .rowKey(dto.rowKey)
                    .timestamp(dto.timestamp)
                    .createdTimestamp(dto.createdTimestamp)
                    .quizDate(dto.quizDate)
                    .quizYear(dto.quizYear)
                    .quizMonth(dto.quizMonth)
                    .quizDay(dto.quizDay)
                    .cardId(dto.cardId)
                    .isCorrect(dto.isCorrect)
                    .userId(dto.userId)
                    .entityId(dto.entityId);

                singleQuizResult.dirtyFlag().reset();
                return singleQuizResult;
            };

        return {
            card: card,
            cardDeck: cardDeck,
            quizResult: quizResult,
            userConfiguration: userConfiguration
        };
    }
);
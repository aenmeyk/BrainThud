define('data-context', ['jquery', 'data-service', 'utils', 'model', 'model.mapper'],
    function ($, dataService, utils, model, modelMapper) {
        var EntitySet = function(config) {
            var cachedResults = [],
                getData = function(options) {
                    var def = new $.Deferred();

                    var results = options && options.results;
                    if (!cachedResults || !utils.hasProperties(cachedResults)) {
                        config.get({
                            params: options.params,
                            success: function (dto) {
                                cachedResults = [];
                                
                                config.mapper.mapResults(dto, cachedResults);

                                if (results) {
                                    results(cachedResults);
                                }

                                def.resolve(results);
                            },
                            error: function(response) {
                                if (def.reject) def.reject();
                                alert(response.statusText);
                            }
                        });
                    } else {
                        results(cachedResults);
                         def.resolve(results);
                    }

                    return def.promise();
                };

            var saveData = function(options) {
                return $.Deferred(function(def) {
                    config.save(options.data, {
                        params: options.params,
                        success: function(result) {
                            def.resolve();
                        },
                        error: function(response) {
                            if (def.reject) def.reject();
                            alert(response.statusText);
                        }
                    });
                });
            };

            return {
                getData: getData,
                saveData: saveData
            };
        };

        var cards = new EntitySet({
                get: dataService.cards.get,
                mapper: modelMapper.card
            }),
            card = new EntitySet({
                save: dataService.cards.save
            }),
            quiz = new EntitySet({
                get: dataService.quiz.get,
                mapper: modelMapper.quiz
            }),
            quizResult = new EntitySet({
                save: dataService.quizResult.save
            });

        return {
            cards: cards,
            card: card,
            quiz: quiz,
            quizResult: quizResult
        };
    }
);
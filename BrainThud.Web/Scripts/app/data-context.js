define('data-context', ['jquery', 'data-service', 'utils', 'model', 'model.mapper'],
    function ($, dataService, utils, model, modelMapper) {
        var
            EntitySet = function (config) {
                var cachedResults = [],
                    getData = function (options) {
                        var def = new $.Deferred();

                        var results = options && options.results;
                        if (!cachedResults || !utils.hasProperties(cachedResults)) {
                            config.get({
                                success: function (dto) {
                                    cachedResults = [];

                                    config.mapper.mapResults(dto, cachedResults);

                                    if (results) {
                                        results(cachedResults);
                                    }

                                    def.resolve(results);
                                },
                                error: function () {
                                    if (def.reject) def.reject();
                                }
                            });
                        } else {
                            results(cachedResults);
                            def.resolve(results);
                        }

                        return def.promise();
                    },

                    createData = function (options) {
                        return $.Deferred(function (def) {
                            config.create(options.data, {
                                success: function (result) {
                                    def.resolve();
                                },
                                error: function (response) {
                                    if (def.reject) def.reject();
                                }
                            });
                        });
                    },

                    updateData = function (options) {
                        return $.Deferred(function (def) {
                            config.update(options.data, {
                                success: function (result) {
                                    def.resolve();
                                },
                                error: function (response) {
                                    if (def.reject) def.reject();
                                }
                            });
                        });
                    };

                return {
                    getData: getData,
                    createData: createData,
                    updateData: updateData
                };
            },

            cards = new EntitySet({
                get: dataService.card.get,
                mapper: modelMapper.cardHtml
            }),
            card = new EntitySet({
                get: dataService.card.get,
                create: dataService.card.create,
                update: dataService.card.update,
                mapper: modelMapper.card
            }),
            quiz = new EntitySet({
                get: dataService.quiz.get,
                mapper: modelMapper.quiz
            }),
            quizResult = new EntitySet({
                create: dataService.quizResult.create
            });

        return {
            cards: cards,
            card: card,
            quiz: quiz,
            quizResult: quizResult
        };
    }
);
define('data-context', ['jquery', 'data-service', 'presenter', 'utils', 'model', 'model.mapper'],
    function ($, dataService, presenter, utils, model, modelMapper) {
        var EntitySet = function(config) {
            var items = [],
                getData = function(options) {
                    var def = new $.Deferred();

                    var results = options && options.results;
                    if (!items || !utils.hasProperties(items)) {
                        config.get({
                            success: function(dtoList) {
                                for (var i = 0; i < dtoList.length; i++) {
                                    items.push(config.mapper.fromDto(dtoList[i]));
                                }

                                if (results) {
                                    results(items);
                                }

                                def.resolve(results);
                            },
                            error: function(response) {
                                if (def.reject) def.reject();
                                alert(response.statusText);
                            }
                        });
                    } else {
                        results(items);
                        def.resolve(results);
                    }

                    return def.promise();
                };

            var saveData = function(options) {
                return $.Deferred(function(def) {
                    config.save(options.data, {
                        success: function(result) {
                            presenter.showSuccess();
                            def.resolve();
                            options.createNewCard();
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

        cards = new EntitySet({
            get: dataService.cards.get,
            mapper: modelMapper.card
        });
        
        card = new EntitySet({
             save: dataService.cards.save
        });
        
        quiz = new EntitySet({
             get: dataService.quiz.get,
             mapper: modelMapper.card
        });

        return {
            cards: cards,
            card: card,
            quiz: quiz
        };
    }
);
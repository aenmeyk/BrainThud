define('data-context', ['jquery', 'data-service', 'presenter', 'utils', 'model', 'model.mapper'],
    function ($, dataService, presenter, utils, model, modelMapper) {
        var
            EntitySet = function (getFunction, saveFunction) {
                var
                    items = [],

                    getData = function (options) {
                        var def = new $.Deferred();

                        var results = options && options.results;
                        if (!items || !utils.hasProperties(items)) {
                            getFunction({
                                success: function (dtoList) {
                                    for (var i = 0; i < dtoList.length; i++) {
                                        items.push(modelMapper.card.fromDto(dtoList[i]));
                                    }

                                    if (results) {
                                        results(items);
                                    }
                                    def.resolve(results);
                                },
                                error: function (response) {
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

                var saveData = function (options) {
                    return $.Deferred(function (def) {
                        saveFunction(options.data, {
                            success: function (result) {
                                presenter.showSuccess();
                                def.resolve();
                                options.createNewCard();
                            },
                            error: function (response) {
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
            },

            cards = new EntitySet(dataService.cards.get, function () { });
            card = new EntitySet(function () { }, dataService.cards.save);
            quiz = new EntitySet(dataService.quiz.get, function () { });

        return {
            cards: cards,
            card: card,
            quiz: quiz
        };
    }
);
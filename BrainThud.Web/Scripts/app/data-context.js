define('data-context', ['jquery', 'data-service', 'presenter', 'utils', 'model', 'model.mapper'],
    function ($, dataService, presenter, utils, model, modelMapper) {
        var
            entitySet = function (getFunction, saveFunction) {
                var
                    items = [],

                    getData = function (options) {
                        console.log('Start getData');
                        var def = new $.Deferred();

                        console.log('Start Deferred');
                        var results = options && options.results;
                        if (!items || !utils.hasProperties(items)) {
                            console.log('getFunction');
                            getFunction({
                                success: function (dtoList) {
                                    console.log('success');
                                    for (var i = 0; i < dtoList.length; i++) {
                                        items.push(modelMapper.card.fromDto(dtoList[i]));
                                    }

                                    if (results) {
                                        results(items);
                                        console.log('resolve ' + results().length);
                                    }
                                    def.resolve(results);
                                },
                                error: function (response) {
                                    console.log('error');
                                    if (def.reject) def.reject();
                                    alert(response.statusText);
                                }
                            });
                        } else {
                            console.log('Else');
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
                                console.log(result);
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

            cards = new entitySet(dataService.cards.get, function () { });
            card = new entitySet(function () { }, dataService.cards.save);
            quiz = new entitySet(dataService.quiz.get, function () { });

        return {
            cards: cards,
            card: card,
            quiz: quiz
        };
    }
);
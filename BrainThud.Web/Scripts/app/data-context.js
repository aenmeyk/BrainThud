define('data-context', ['jquery', 'data-service', 'presenter', 'utils', 'model', 'model.mapper'],
    function ($, dataService, presenter, utils, model, modelMapper) {
        var
            entitySet = function (getFunction, saveFunction) {
                var
                    items = [],

                    getData = function (options) {
                        return $.Deferred(function (def) {
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
                        }).promise();
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

        return {
            cards: cards,
            card: card
        };
    }
);
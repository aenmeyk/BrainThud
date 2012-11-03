define('data-context', ['jquery', 'data-service', 'presenter', 'utils'],
    function ($, dataService, presenter, utils) {
        var
            entitySet = function (getFunction, saveFunction) {
                var
                    items = {},

                    getData = function (options) {
                        return $.Deferred(function (def) {
                            var results = options && options.results;
                            if (!items || !utils.hasProperties(items)) {
                                getFunction({
                                    success: function (dtoList) {
                                        items = dtoList;
                                        //                                options.results(dtoList);
                                        
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
                        saveFunction({
                            data: options.data,
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

            cards = new entitySet(dataService.getCards, function () { });
        card = new entitySet(function () { }, dataService.saveCard);

        return {
            cards: cards,
            card: card
        };
    }
);
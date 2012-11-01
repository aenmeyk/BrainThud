define('data-context', ['jquery', 'data-service', 'presenter'],
    function ($, dataService, presenter) {
        var
            entitySet = function (getFunction, saveFunction) {
                
                var getData = function (options) {
                    return $.Deferred(function (def) {
                        var results = options && options.results;
                        getFunction({
                            success: function (dtoList) {
                                options.results(dtoList);
                                results = dtoList;
                                def.resolve(results);
                            },
                            error: function (response) {
                                if (def.reject) def.reject();
                                alert(response.statusText);
                            }
                        });
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
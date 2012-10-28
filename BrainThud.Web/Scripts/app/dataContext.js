define('dataContext', ['jquery', 'dataService'],
    function ($, dataService) {
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

                var saveData = function () {
                    return $.Deferred(function (def) {
                        saveFunction({
                            success: function (data) {
                                console.log(data);
                                def.resolve();
                            }
                        });
                    });
                };

                return {
                    getData: getData,
                    saveData: saveData
                };
            },

            questions = new entitySet(dataService.getCards, dataService.saveCard);

        return {
            questions: questions
        };
    }
);
define('dataContext', ['jquery', 'dataService'],
    function ($, dataService) {
        var
            entitySet = function (getFunction) {
                var getData = function (options) {
                    var items = getFunction();
                    options.results(items);
//                    return $.Deferred(function (def) {
//                        var results = options && options.results;
//                        getFunction({
//                            success: function(dtoList) {
//                                results = dtoList;
//                                def.resolve(results);
//                            },
//                            error: function(response) {
//                                
//                            }
//                        });
//                    }).promise();
                };

                return {
                    getData: getData
                };
            },
            
            questions = new entitySet(dataService.getNuggets);
        
        return {
            questions: questions
        };
    }
);
define('vm.nugget', ['jquery', 'ko', 'dataContext'],
    function ($, ko, dataContext) {
        var questions = ko.observableArray(),
            dataOptions = function() {
                return {
                    results: questions
                };
            };
        
        
        dataContext.questions.getData(dataOptions());
        
        return {
            questions: questions
        };
    }
);
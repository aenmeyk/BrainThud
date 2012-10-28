define('vm.card', ['jquery', 'ko', 'dataContext'],
    function ($, ko, dataContext) {
        var questions = ko.observableArray(),
            dataOptions = function() {
                return {
                    results: questions
                };
            };
        
        dataContext.questions.getData(dataOptions());
        dataContext.questions.saveData();
        
        return {
            questions: questions
        };
    }
);
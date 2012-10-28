define('vm.card', ['jquery', 'ko', 'dataContext'],
    function ($, ko, dataContext) {
        var questions = ko.observableArray(),
            dataOptions = function() {
                return {
                    results: questions
                };
            },
            saveCard = function () {
                dataContext.questions.saveData();
            };
        
        dataContext.questions.getData(dataOptions());
        
        
        return {
            questions: questions,
            saveCard: saveCard
        };
    }
);
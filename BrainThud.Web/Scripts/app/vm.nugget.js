define('vm.nugget', ['jquery', 'ko', 'dataContext'],
    function ($, ko, dataContext) {
        
        var questions = ko.observable(dataContext.questions);
        
        return {
            questions: questions
        };
    }
);
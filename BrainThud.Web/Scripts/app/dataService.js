define('dataService', ['jquery', 'model'],
    function ($, model) {

        var root = '/api/',
            getNuggets = function(callbacks) {
                var url = root + 'nuggets';
                $.getJSON(url)
                .done(function(result, status) {
                    callbacks.success(result);
                })
                .fail(function (result, status) {
                    callbacks.error(result);
                });
            };

//        var questions = function() {
//            return [
//                'How long is a piece of string?',
//                'When does 1 + 1 != 2?',
//                'What is black and white and read all over?'
//            ];
//        };
//
//        var nuggets = function() {
//            var n1 = new model.nugget();
//            n1.question('First Question');
//            
//            var n2 = new model.nugget();
//            n2.question('Second Question');
//            
//            var n3 = new model.nugget();
//            n3.question('Third Question');
//
//            return [n1, n2, n3];
//        };

        return {
            getNuggets: getNuggets
//            getNuggets: questions
//            getNuggets: nuggets
        };
    }
);
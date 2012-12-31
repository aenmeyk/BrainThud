define('data-subs', ['amplify', 'config'],
    function (amplify, config) {
        var quiz = [function(getCachedResults) {
            amplify.subscribe(config.pubs.createQuizResult, function(data) {
                var quizResults = getCachedResults()[0].quizResults;
                for (var i = 0; i < quizResults.length; i++) {
                    if (quizResults[i].cardId === data.cardId) {
                        quizResults.splice(i, 1);
                        break;
                    }
                }

                quizResults.push(data);
            });
        }];

        return {
            quiz: quiz
        };
    });
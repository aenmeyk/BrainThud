define('data-context', ['data-service', 'model.mapper', 'data-context-helper'],
    function (dataService, modelMapper, dataContextHelper) {
        var
            cards = new dataContextHelper.EntitySet({
                get: dataService.card.get,
                mapper: modelMapper.cardHtml
            }),
            card = new dataContextHelper.EntitySet({
                get: dataService.card.get,
                create: dataService.card.create,
                update: dataService.card.update,
                mapper: modelMapper.card
            }),
            quiz = new dataContextHelper.EntitySet({
                get: dataService.quiz.get,
                mapper: modelMapper.quiz
            }),
            quizResult = new dataContextHelper.EntitySet({
                create: dataService.quizResult.create
            });

        return {
            cards: cards,
            card: card,
            quiz: quiz,
            quizResult: quizResult
        };
    }
);
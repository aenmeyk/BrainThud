define('data-context', ['data-service', 'model.mapper', 'data-context-helper'],
    function (dataService, modelMapper, dataContextHelper) {
        var
            card = new dataContextHelper.EntitySet({
                get: dataService.card.get,
                create: dataService.card.create,
                update: dataService.card.update,
                deleteItem: dataService.card.deleteItem,
                mapper: modelMapper.card
            }),
            quiz = new dataContextHelper.EntitySet({
                get: dataService.quiz.get,
                mapper: modelMapper.quiz
            }),
            quizCard = new dataContextHelper.EntitySet({
                get: dataService.quizCard.get,
                mapper: modelMapper.quizCard
            }),
            config = new dataContextHelper.EntitySet({
                get: dataService.config.get,
                mapper: modelMapper.config
            }),
            quizResult = new dataContextHelper.EntitySet({
                create: dataService.quizResult.create,
                mapper: modelMapper.quizResult
            });

        return {
            card: card,
            quiz: quiz,
            quizCard: quizCard,
            config: config,
            quizResult: quizResult
        };
    }
);
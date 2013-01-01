define('data-context', ['data-service', 'model.mapper', 'data-context-helper', 'data-subs'],
    function (dataService, modelMapper, dataContextHelper, dataSubs) {
        var
            userConfig = new dataContextHelper.EntitySet({
                get: dataService.config.get,
                mapper: modelMapper.config
            }),
            
            card = new dataContextHelper.EntitySet({
                get: dataService.card.get,
                create: dataService.card.create,
                update: dataService.card.update,
                deleteItem: dataService.card.deleteItem,
                mapper: modelMapper.card
            }),
            
            quiz = new dataContextHelper.EntitySet({
                get: dataService.quiz.get,
                mapper: modelMapper.quiz,
                subs: dataSubs.quiz
            }),
            
            quizResult = new dataContextHelper.EntitySet({
                create: dataService.quizResult.create,
                mapper: modelMapper.quizResult
            });

        return {
            card: card,
            quiz: quiz,
            config: userConfig,
            quizResult: quizResult
        };
    }
);
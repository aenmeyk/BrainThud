define('data-context', ['data-service', 'model.mapper', 'data-context-helper', 'data-subs', 'config'],
    function (dataService, modelMapper, dataContextHelper, dataSubs, config) {
        var
            userConfiguration = new dataContextHelper.EntitySet({
                get: dataService.userConfiguration.get,
                update: dataService.userConfiguration.update,
                mapper: modelMapper.userConfiguration,
                showSuccessToastr: true
            }),
            
            card = new dataContextHelper.EntitySet({
                get: dataService.card.get,
                create: dataService.card.create,
                update: dataService.card.update,
                deleteItem: dataService.card.deleteItem,
                mapper: modelMapper.card,
                cacheChangedPub: config.pubs.cardCacheChanged,
                showSuccessToastr: true
            }),
            
            quizCard = new dataContextHelper.EntitySet({
                get: dataService.card.getForQuiz,
                deleteItem: dataService.card.deleteItem,
                mapper: modelMapper.card,
                cacheChangedPub: config.pubs.quizCardCacheChanged
            }),
            
            quizResult = new dataContextHelper.EntitySet({
                get: dataService.quizResult.get,
                create: dataService.quizResult.create,
                update: dataService.quizResult.update,
                mapper: modelMapper.quizResult,
                cacheChangedPub: config.pubs.quizResultCacheChanged
            });

        return {
            userConfiguration: userConfiguration,
            card: card,
            quizCard: quizCard,
            quizResult: quizResult
        };
    }
);
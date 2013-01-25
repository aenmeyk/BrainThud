///// <reference path="../../Scripts/jasmine/jasmine.js" />
///// <reference path="jasmine-test-setup.js" />
///// <reference path="../../Scripts/app/data-context.js" />
//
//describe("Given data-context module", function () {
//    var dataContext, dataService, modelMapper, dataContextHelper;
//
//    beforeEach(function () {
//        dataContext = modules['data-context'];
//        dataContextHelper = jasmine.createSpyObj('dataContextHelper', ['EntitySet']);
//        dataContextHelper.EntitySet.andCallFake(function (config) {
//            return config;
//        });
//        modelMapper = jasmine.createSpyObj('modelMapper', ['card', 'cardHtml', 'quiz']);
//
//        dataService = {
//            card: jasmine.createSpyObj('dataService.card', ['get', 'create', 'update']),
//            quiz: jasmine.createSpyObj('dataService.quiz', ['get']),
//            config: jasmine.createSpyObj('dataService.config', ['get']),
//            quizResult: jasmine.createSpyObj('dataService.quizResult', ['create'])
//        };
//    });
//
//    describe("when card is called", function () {
//        var card;
//
//        beforeEach(function () {
//            card = dataContext(dataService, modelMapper, dataContextHelper).card;
//        });
//
//        it('then the dataContextHelper.EntitySet should be instantiated with the correct configuration', function () {
//            expect(card).toEqual({
//                get: dataService.card.get,
//                create: dataService.card.create,
//                update: dataService.card.update,
//                mapper: modelMapper.card
//            });
//        });
//    });
//
//    describe("when quiz is called", function () {
//        var quiz;
//
//        beforeEach(function () {
//            quiz = dataContext(dataService, modelMapper, dataContextHelper).quiz;
//        });
//
//        it('then the dataContextHelper.EntitySet should be instantiated with the correct configuration', function () {
//            expect(quiz).toEqual({
//                get: dataService.quiz.get,
//                mapper: modelMapper.quiz
//            });
//        });
//    });
//
//    describe("when quizResult is called", function () {
//        var quizResult;
//
//        beforeEach(function () {
//            quizResult = dataContext(dataService, modelMapper, dataContextHelper).quizResult;
//        });
//
//        it('then the dataContextHelper.EntitySet should be instantiated with the correct configuration', function () {
//            expect(quizResult).toEqual({
//                create: dataService.quizResult.create
//            });
//        });
//    });
//
//});
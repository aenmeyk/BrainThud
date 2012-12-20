/// <reference path="../../Scripts/jasmine/jasmine.js" />
/// <reference path="jasmine-test-setup.js" />
/// <reference path="../../Scripts/jquery-1.8.2.js" />
/// <reference path="../../Scripts/app/data-context.js" />

describe("A data-context module, when cards is called", function () {
    var dataService, utils, modelMapper, card;

    beforeEach(function () {

        dataService = {
            card: jasmine.createSpyObj('dataService.card', ['get', 'create', 'update']),
            quiz: jasmine.createSpyObj('dataService.quiz', ['get']),
            quizResult: jasmine.createSpyObj('dataService.quizResult', ['create'])
        };

        utils = jasmine.createSpyObj('utils', ['hasProperties']);
        modelMapper = jasmine.createSpyObj('modelMapper', ['card', 'cardHtml', 'quiz']);

        card = modules['data-context'](jQuery, dataService, utils, modelMapper).cards;
    });

    it('the card.get should be equal dataService.card.get', function () {
        expect(card.get).toEqual(dataService.card.get);
    });
});
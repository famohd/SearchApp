describe('search service', function () {

    var $httpBackend;
    var PeopleService;


    beforeEach(angular.mock.module('PeopleSearchApp'));
    beforeEach(module(function($urlRouterProvider) {
        $urlRouterProvider.deferIntercept();
    }));    
    beforeEach(angular.mock.inject(function(_PeopleService_, _$httpBackend_) {
        $httpBackend = _$httpBackend_;
        PeopleService = _PeopleService_;

    }));


    // List people test
    it('Get should return 200 on success', function(){

      $httpBackend
        .when('GET', '/search')
        .respond(200, [{ "id": 1, "FirstName": "Jeff", "LastName": "Kemper" }])

      var results = PeopleService.Get();
      $httpBackend.flush();

      expect(results).not.toBe(undefined);

    });

    // search by name  test
    it('ByName should return 200 on success', function(){

      $httpBackend
        .when('GET', '/search/name/a')
         .respond(200, [{ id: 1, FirstName: 'Jeff', LastName: 'Kemper' }])

      var results = PeopleService.ByName('a');
      $httpBackend.flush();

      expect(results).not.toBe(undefined);
    });
        

    // save new person data  test
    it('Save should return 200 on success', function(){

      $httpBackend
        .when('POST', '/search/create')
         .respond(200);

      var newPerson = {id: 1, FirstName: 'Jeff', LastName: 'Kemper'};
      var results = PeopleService.Save(newPerson);
      $httpBackend.flush();

      expect(results).not.toBe(undefined);
    });


    })
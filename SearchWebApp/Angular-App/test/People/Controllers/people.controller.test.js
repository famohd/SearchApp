describe('search controller', function () {

    var $q;
    var $scope;
    var deferred;

    beforeEach(angular.mock.module('PeopleSearchApp'));
    beforeEach(module(function($urlRouterProvider) {
        $urlRouterProvider.deferIntercept();
    }));

    beforeEach(angular.mock.inject(function($controller, _$rootScope_, _$q_, PeopleService) {
        $q = _$q_;
        $scope = _$rootScope_.$new();

        deferred = _$q_.defer();

        spyOn(PeopleService, 'Get').and.returnValue(deferred.promise);
        spyOn(PeopleService, 'ByName').and.returnValue(deferred.promise);
        spyOn(PeopleService, 'Save').and.returnValue(deferred.promise);

        $controller('PeopleController', {
            $scope: $scope,
            peopleService: PeopleService
        });
    }));

    // List people tests
    it('should resolve list all people', function(){
        $scope.listPeople();
        deferred.resolve({data: '[{ "id": 1, "FirstName": "Jeff", "LastName": "Kemper" }]' });

        $scope.$apply();
        expect($scope.people).not.toBe(undefined);
        expect($scope.error).toBe(undefined);
    });

    it('should reject promise', function () {
      deferred.reject('There has been an error!');
      $scope.listPeople();
      
      $scope.$apply();
      expect($scope.people).toBe(undefined);
      expect($scope.error).toBe('There has been an error!');
    });
        

    // search by name tests
    it('should search by name', function(){
        $scope.searchExpr = 'j';
        $scope.searchByName();
        deferred.resolve({data: '[{ "id": 1, "FirstName": "Jeff", "LastName": "Kemper" }]' });

        $scope.$apply();
        expect($scope.people).not.toBe(undefined);
        expect($scope.error).toBe(undefined);
    });

    it('should reject promise', function () {
      $scope.searchExpr = 'j';
      deferred.reject('There has been an error!');
      $scope.searchByName();
      
      $scope.$apply();
      expect($scope.people).toBe(undefined);
      expect($scope.error).toBe('There has been an error!');
    });
        

    // add person tests
    it('should add new person', function(){
        $scope.newPerson = '{ "id": 1, "FirstName": "Jeff", "LastName": "Kemper" }';
        $scope.addPerson();

        $scope.$apply();
        expect($scope.error).toBe(undefined);
    });

    it('should reject promise', function () {
      deferred.reject('There has been an error!');
      $scope.newPerson = '{ "id": 1, "FirstName": "Jeff", "LastName": "Kemper" }';
      $scope.addPerson();
      
      $scope.$apply();
      expect($scope.people).toBe(undefined);
      expect($scope.error).toBe('There has been an error!');
    });

    })
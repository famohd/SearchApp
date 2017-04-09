(function () {
    'use strict';

    function peopleController($scope, $location, PeopleService) {
        $scope.title = "Search";
        $scope.promise = null;

        $scope.listPeople = function () {

            $scope.promise = PeopleService.Get()
                .then(function (results) {
                    console.log("success")
                    $scope.people = JSON.parse(results.data)
                }).catch(function (error) {
                    $scope.error = error;
                    $location.path('error');
                });

            return $scope.promise;
        };

        $scope.addPerson = function () {

            if ($scope.newPerson == undefined) {
                return;
            }
            $scope.promise = PeopleService.Save($scope.newPerson).then(
                function (results) {
                    $location.path('people');
                }).catch(function (error) {
                    $scope.error = error;
                    $location.path('error');
                });

            return $scope.promise;
        };

        $scope.searchByName = function () {

            if ($scope.searchExpr == undefined) {
                return;
            }

            $scope.promise = PeopleService.ByName($scope.searchExpr)
                .then(function (results) {
                    console.log("success")
                    $scope.people = JSON.parse(results.data)
                }).catch(function (error) {
                    $scope.error = error;
                    $location.path('error');
                });

            return $scope.promise;
        };

    }

    angular.module('PeopleSearchApp')
      .controller('PeopleController', ['$scope', '$location', 'PeopleService', peopleController]);
}())
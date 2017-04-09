(function () {
    'use strict';

    function homeController($scope) {
        $scope.title = 'You are Home';
    }

    angular.module('PeopleSearchApp')
      .controller('HomeController', ['$scope', homeController]);
}())
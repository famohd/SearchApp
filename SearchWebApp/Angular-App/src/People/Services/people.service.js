(function () {
    'use strict';

    function peopleService($http) {

        var get = function () {

            return $http.get('/search');
        },
            save = function (person) {
                return $http.post('/search/create', { personData: JSON.stringify(person) });
            },
            byName = function (expr) {
                return $http.get('/search/name/' + expr);
            };

        return {
            Get: get,
            Save: save,
            ByName: byName
        };
    }

    angular.module('PeopleSearchApp')
      .factory('PeopleService', ['$http', peopleService])
}())
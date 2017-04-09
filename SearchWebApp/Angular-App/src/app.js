(function () {
    'use strict';

    function config($stateProvider, $urlRouterProvider, $locationProvider, $provide) {
        $stateProvider
          .state('home', {
              url: '/',
              templateUrl: '/Angular-App/src/home/views/home.view.html'
          })
          .state('people', {
              url: '/people',
              templateUrl: '/Angular-App/src/people/views/people.view.html'
          })
          .state('add', {
              url: '/add',
              templateUrl: '/Angular-App/src/people/views/add.people.view.html'
          })
          .state('error', {
            url: '/error',
            templateUrl: '/Angular-App/src/people/views/error.people.view.html'
          });
        $urlRouterProvider.otherwise('/');

        $provide.decorator('$exceptionHandler', function ($delegate) {

            return function (exception, cause) {
                exception.message = "An Error occurred in the app. \n Details: " + exception.message;
                $delegate(exception, cause);
                alert(exception.message);
            };
        });
    }

    angular.module('PeopleSearchApp', ['ui.router', 'cgBusy'])
      .config(['$stateProvider', '$urlRouterProvider', '$locationProvider', '$provide', config]);
}())
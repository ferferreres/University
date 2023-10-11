'use strict';

var app = angular.module("universityApp", ["ngRoute", "moduleController", 'moduleService'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider
            .when("/students", {
                templateUrl: "/render/GetHtml?view=students",
                controller: 'studentsController'
            })
            .when("/courses", {
                templateUrl: "/render/GetHtml?view=courses",
                controller: "coursesController"
            })
            .otherwise({
                redirectTo: "/"
            });

        $locationProvider.html5Mode(true);
    })
    .controller('mainController', function ($scope) {
        $scope.message = 'hello world';

    });

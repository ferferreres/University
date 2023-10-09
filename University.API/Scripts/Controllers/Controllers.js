'use strict';

var courseCtrl = angular.module("moduleController", [])
    .controller("coursesController", ['$scope', 'CoursesService','EnrollmentService', function ($scope, CoursesService, EnrollmentService) {

    var handleSuccess = function (response) {
        $scope.courses = response.data;
    }

    CoursesService.getCourses().then(handleSuccess);
    $scope.newCourse = {};

    $scope.addCourse = function () {
        CoursesService.postCourse($scope.newCourse)
            .then(function () {
                CoursesService.getCourses().then(handleSuccess);
            })
        $scope.newCourse = {};
    };

    $scope.removeCourse = function (course) {
        CoursesService.deleteCourse(course)
            .then(function () {
                CoursesService.getCourses().then(handleSuccess);
            })
        };
        $scope.infoCourse = function (courseId) {
            EnrollmentService.getCourseInfoById(courseId).then(function (response) {
                $scope.courseInfo = response.data;
            })
        };
    }])
    .controller("studentsController", ['$scope', 'StudentsService', function ($scope, StudentService) {

        var handleSuccess = function (response) {
            $scope.students = response.data;
        }

        StudentService.getStudents().then(handleSuccess);
        $scope.newStudent = {};

        $scope.showStudentById = function (studentID) {
            StudentService.getByIdStudent(studentID).then(function (response) {
                $scope.studentInfo = response.data;
            });
        }

        $scope.addStudent = function () {
            StudentService.postStudent($scope.newStudent)
                .then(function () {
                    StudentService.getStudents().then(handleSuccess);
                })
            $scope.newStudent = {};
        };

        $scope.removeStudent = function (student) {
            StudentService.deleteStudent(student)
                .then(function () {
                    StudentService.getStudents().then(handleSuccess);
                })
        };

    }]);
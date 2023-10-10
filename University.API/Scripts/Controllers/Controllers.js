﻿'use strict';

var courseCtrl = angular.module("moduleController", [])
    .controller("coursesController", ['$scope', 'CoursesService', function ($scope, CoursesService) {

        var handleSuccess = function (response) {
            $scope.courses = response.data;
        };

        CoursesService.getCourses().then(handleSuccess);
        $scope.newCourse = {};
        $scope.getInfo = function (id) {
            CourseService.getInfo(id).then(function(response) {
                $scope.StudentsInfo = response.data;
            })
        };
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

        $studentInfo = {};
        StudentService.getStudents().then(handleSuccess);
        $scope.newStudent = {};

        $scope.showStudentById = function (studentID) {
            StudentService.getByIdStudent(studentID).then(function (response) {
                $scope.studentInfo.student = response.data;
            });
            StudentService.getInfoStudent(studentID).then(function (response) {
                $scope.studentInfo.enrollments = response.data
            }
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
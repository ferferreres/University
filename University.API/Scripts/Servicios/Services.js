'use strict';

angular.module('moduleService', [])

    .factory('CoursesService', ['$http', function ($http) {

        var courseService = {};

        courseService.key = '/api/Courses';

        var getCourses = function () {
            return $http.get(courseService.key);
        };

        var postCourse = function (newCourse) {
            return $http.post(courseService.key, newCourse);
        };

        var deleteCourse = function (idCourse) {
            return $http.delete(courseService.key + '/' + idCourse);
        }

        return {
            getCourses: getCourses,
            postCourse: postCourse,
            deleteCourse: deleteCourse
        };
    }])

    .factory('StudentsService', ['$http', function ($http) {

        var studentService = {};

        studentService.key = '/api/Students';

        var getStudent = function () {
            return $http.get(studentService.key);
        };

        var postStudent = function (newStudent) {
            return $http.post(studentService.key, newStudent);
        };

        var deleteStudent = function (idStudent) {
            return $http.delete(studentService.key + '/' + idStudent);
        }

        return {
            getStudent: getStudent,
            postStudent: postStudent,
            deleteStudent: deleteStudent
        };
    }]);
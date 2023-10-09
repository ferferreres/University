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
        };

        return {
            getCourses: getCourses,
            postCourse: postCourse,
            deleteCourse: deleteCourse
        };
    }])

    .factory('StudentsService', ['$http', function ($http) {

        var studentService = {};

        studentService.key = '/api/Students';

        var getStudents = function () {
            return $http.get(studentService.key);
        };

        var getByIdStudent = function (studentId) {
            return $http.get(studentService.key + '/' + studentId);
        };

        var postStudent = function (newStudent) {
            return $http.post(studentService.key, newStudent);
        };

        var deleteStudent = function (idStudent) {
            return $http.delete(studentService.key + '/' + idStudent);
        };

        return {
            getStudents: getStudents,
            getByIdStudent: getByIdStudent,
            postStudent: postStudent,
            deleteStudent: deleteStudent
        };
    }])

    .factory('EnrollmentService', ['$http', function ($http) {
        var enrollmentService = {};

        enrollmentService.key = '/api/Enrollments';

        var getCourseInfoById = function (courseId) {
            return $http.get(enrollmentService.key + '/' + courseId + '/Students');
        };

        return {
            getCourseInfoById: getCourseInfoById
        }
    }]);
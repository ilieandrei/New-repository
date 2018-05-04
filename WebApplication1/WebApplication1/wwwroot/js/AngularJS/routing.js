var app = angular.module("myApp", ['ngRoute', 'angularjs-md5']);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "/Account/Index",
            controller: "userController"
        })
        .when("/studentTimetable", {
            templateUrl: "/Account/StudentTimetable",
            controller: "studentController"
        })
        .when("/teacherTimetable", {
            templateUrl: "/Account/TeacherTimetable",
            controller: "teacherController"
        })
        .when("/studentProfile", {
            templateUrl: "/Account/StudentProfile",
            controller: "studentController"
        })
        .when("/studentProfileSettings", {
            templateUrl: "/Account/StudentProfileSettings",
            controller: "studentController"
        })
        .when("/teacherProfile", {
            templateUrl: "/Account/TeacherProfile",
            controller: "teacherController"
        })
        .when("/teacherProfileSettings", {
            templateUrl: "/Account/TeacherProfileSettings",
            controller: "teacherController"
        })
        .when("/teacherFullTimetable", {
            templateUrl: "/Account/TeacherFullTimetable",
            controller: "teacherController"
        })
        .when("/studentFullTimetable", {
            templateUrl: "/Account/StudentFullTimetable",
            controller: "studentController"
        });
});
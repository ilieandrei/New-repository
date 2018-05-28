var app = angular.module("myApp", ['ngRoute', 'angularjs-md5', 'ngRateIt']);
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
        })
        .when("/teacherCourses", {
            templateUrl: "/Account/TeacherCourses",
            controller: "teacherController"
        })
        .when("/studentAnswer", {
            templateUrl: "/Account/StudentAnswer",
            controller: "studentController"
        })
        .when("/studentCourses", {
            templateUrl: "/Account/StudentCourses",
            controller: "studentController"
        })
        .when("/teacherAnswers", {
            templateUrl: "/Account/TeacherAnswers",
            controller: "teacherController"
        })
        .when("/studentStatus", {
            templateUrl: "/Account/StudentStatus",
            controller: "studentController"
        })
        .when("/teacherStatus", {
            templateUrl: "/Account/TeacherStatus",
            controller: "teacherController"
        });
});
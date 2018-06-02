var app = angular.module("myApp", ['ngRoute', 'angularjs-md5', 'ngRateIt']);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "/Account/Index",
            controller: "userController"
        })
        .when("/studentTimetable", {
            templateUrl: "/Student/StudentTimetable",
            controller: "studentController"
        })
        .when("/teacherTimetable", {
            templateUrl: "/Teacher/TeacherTimetable",
            controller: "teacherController"
        })
        .when("/studentProfile", {
            templateUrl: "/Student/StudentProfile",
            controller: "studentController"
        })
        .when("/studentProfileSettings", {
            templateUrl: "/Student/StudentProfileSettings",
            controller: "studentController"
        })
        .when("/teacherProfile", {
            templateUrl: "/Teacher/TeacherProfile",
            controller: "teacherController"
        })
        .when("/teacherProfileSettings", {
            templateUrl: "/Teacher/TeacherProfileSettings",
            controller: "teacherController"
        })
        .when("/teacherFullTimetable", {
            templateUrl: "/Teacher/TeacherFullTimetable",
            controller: "teacherController"
        })
        .when("/studentFullTimetable", {
            templateUrl: "/Student/StudentFullTimetable",
            controller: "studentController"
        })
        .when("/teacherCourses", {
            templateUrl: "/Teacher/TeacherCourses",
            controller: "teacherController"
        })
        .when("/studentAnswer", {
            templateUrl: "/Student/StudentAnswer",
            controller: "studentController"
        })
        .when("/studentCourses", {
            templateUrl: "/Student/StudentCourses",
            controller: "studentController"
        })
        .when("/teacherAnswers", {
            templateUrl: "/Teacher/TeacherAnswers",
            controller: "teacherController"
        })
        .when("/studentStatus", {
            templateUrl: "/Student/StudentStatus",
            controller: "studentController"
        })
        .when("/teacherStatus", {
            templateUrl: "/Teacher/TeacherStatus",
            controller: "teacherController"
        })
        .when("/adminTimetable", {
            templateUrl: "/Administrator/AdminTimetable",
            controller: "adminController"
        })
        .when("/adminUsers", {
            templateUrl: "/Administrator/AdminUsers",
            controller: "adminController"
        })
        .when("/studentsStatus", {
            templateUrl: "/Teacher/StudentsStatus",
            controller: "teacherController"
        });
});
var app = angular.module("myApp");

app.controller("userController", ['$scope', '$http', '$location', 'myService', userController]);
function userController($scope, $http, $location, myService) {

    $scope.userRoleValue = function (username) {
        if ($scope.usrRole === undefined) {
            myService.getUserRole(username).then(function (response) {
                $scope.usrRole = response.data;
                $scope.showBarItem = true;
                myService.getCurrentTab("").then(function (response1) {
                    if (response1.data != "No content") {
                        document.getElementById(response1.data).classList.add("active");
                        document.getElementById(response1.data).classList.add("text-primary");
                    }
                }, function (response1) {
                });
            }, function (response) {
            });
        }
    };


    $scope.teacherProfileTab = function () {
        myService.setCurrentTab("teacherProfileTabId").then(function (response) {
        }, function (response) {
        });
    };
    $scope.teacherTimetableTab = function () {
        myService.setCurrentTab("teacherTimetableTabId").then(function (response) {
        }, function (response) {
        });
    };
    $scope.studentProfileTab = function () {
        myService.setCurrentTab("studentProfileTabId").then(function (response) {
        }, function (response) {
        });
    };
    $scope.studentTimetableTab = function () {
        myService.setCurrentTab("studentTimetableTabId").then(function (response) {
        }, function (response) {
        });
    };
}
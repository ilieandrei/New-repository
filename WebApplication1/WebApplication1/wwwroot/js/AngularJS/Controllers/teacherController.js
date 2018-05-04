var app = angular.module("myApp");

app.controller("teacherController", ['$scope', '$http', '$location', 'myService', teacherController]);
function teacherController($scope, $http, $location, myService) {

    $scope.teacherProfileValue = function (username) {
        $scope.teacherTitles = [
            "",
            "Conf. dr.",
            "Colab.",
            "Asist. dr.",
            "Lect. dr.",
            "Lect.",
            "drd.",
            "Colab. dr.",
            "Prof. dr.",
            "Asist. drd.",
            "Cerc. dr.",
            "Prof.",
            "Asist."
        ]
        myService.getTeacherProfile(username).then(function (response) {
            $scope.teacherDetails = {
                teacherTitle: response.data.function,
                teacherFullName: response.data.fullName,
                teacherMail: response.data.email
            };
        }, function (response) {
        });
    };

    $scope.editTeacherProfilePage = function () {
        $location.path('teacherProfileSettings');
    };

    $scope.editTeacherProfile = function (username, teacherDetails) {
        var data = {
            Email: $scope.teacherDetails.teacherMail,
            FullName: $scope.teacherDetails.teacherFullName,
            Function: $scope.teacherDetails.teacherTitle,
            Username: username
        };
        myService.putTeacherProfile(data).then(function (response) {
            if (response === 200)
                $location.path('teacherProfile');
        }, function (response) {
            $scope.teacherUpdateMessage = "A apărut o eroare şi schimbările nu s-au salvat";
        });
    };

    $scope.backToTeacherProfile = function () {
        $location.path('teacherProfile');
    };
    
    $scope.teacherTimetableValue = function (username) {
        myService.getTeacherTimetable(username).then(function (response) {
            $scope.teacherTimetable = response.data.timetables;
            $scope.teacherTimetable[0].day = "Vineri";
            $scope.teacherDailyTimetable = [];
            var days = ["Duminica", "Luni", "Marti", "Miercuri", "Joi", "Vineri", "Sambata"];
            for (var i = 0; i < $scope.teacherTimetable.length; i++) {
                if ($scope.teacherTimetable[i].day === days[new Date().getDay()]) {
                    $scope.teacherDailyTimetable.push($scope.teacherTimetable[i]);
                }
            }
        }, function (response) {
        });
    }

    $scope.teacherCompleteTimetablePath = function () {
        $location.path('teacherFullTimetable');
    }

    $scope.backToTeacherDailyTimetable = function () {
        $location.path('teacherTimetable');
    }

}
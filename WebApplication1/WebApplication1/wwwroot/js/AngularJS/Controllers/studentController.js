var app = angular.module("myApp");

app.controller("studentController", ['$scope', '$http', '$location', 'myService', studentController]);
function studentController($scope, $http, $location, myService) {

    $scope.studentProfileValue = function (username) {
        $scope.studyYears = ["1", "2", "3"];
        $scope.studyHalfyears = ["A", "B", "E"];
        myService.getStudentProfile(username).then(function (response) {
            $scope.studentDetails = {
                studentFullName: response.data.fullName,
                studyYear: response.data.year,
                studentHalfyear: response.data.group[0],
                studentGroup: response.data.group[1],
                studentMail: response.data.email
            };
        }, function (response) {
        });
    };

    $scope.editStudentProfilePage = function () {
        $location.path('studentProfileSettings');
    };

    $scope.editStudentProfile = function (username, studentDetails) {
        var data = {
            Email: studentDetails.studentMail,
            FullName: studentDetails.studentFullName,
            Year: studentDetails.studyYear,
            Group: studentDetails.studentHalfyear + studentDetails.studentGroup,
            Username: username
        };
        myService.putStudentProfile(data).then(function (response) {
            if (response === 200)
                $location.path('studentProfile');
        }, function (response) {
            $scope.studentUpdateMessage = "A apărut o eroare şi schimbările nu s-au salvat";
        });
        /*var response = myService.putStudentProfile(data);
        if (response.$$state.value === 200) {
            $location.path('studentProfile');
        } else {
            $scope.studentUpdateMessage = "A apărut o eroare şi schimbările nu s-au salvat";
        }*/
    };

    $scope.backToStudentProfile = function () {
        $location.path('studentProfile');
    };

    $scope.studentTimetableValue = function (username) {
        myService.getStudentTimetable(username).then(function (response) {
            $scope.studentTimetable = response.data.timetables;
            $scope.studentTimetable[0].day = "Vineri";
            $scope.studentTimetable[1].day = "Vineri";
            $scope.studentTimetable[2].day = "Vineri";
            $scope.studentTimetable[3].day = "Vineri";
            $scope.studentTimetable[4].day = "Vineri";
            for (var i = 0; i < $scope.studentTimetable.length; i++) {
                $scope.studentTimetable[i].teacher = $scope.studentTimetable[i].teacher.split(';');//replace(/;/g, " | ");
                $scope.studentTimetable[i].teacher.pop();
            }
            $scope.studentDailyTimetable = [];
            var days = ["Duminica", "Luni", "Marti", "Miercuri", "Joi", "Vineri", "Sambata"];
            for (var i = 0; i < $scope.studentTimetable.length; i++) {
                if ($scope.studentTimetable[i].day === days[new Date().getDay()]) {
                    $scope.studentDailyTimetable.push($scope.studentTimetable[i]);
                }
            }
            $scope.dailyLen = $scope.studentDailyTimetable.length;
        }, function (response) {
        });
    }

    $scope.studentCompleteTimetablePath = function () {
        $location.path('studentFullTimetable');
    }

    $scope.backToStudentDailyTimetable = function () {
        $location.path('studentTimetable');
    }

}
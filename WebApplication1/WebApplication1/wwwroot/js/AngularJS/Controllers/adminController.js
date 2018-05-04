var app = angular.module("myApp");

app.controller("adminController", ['$scope', '$http', '$location', 'myService', adminController]);
function adminController($scope, $http, $location, myService) {

    $scope.getTimetableValue = function () {
        myService.getFullTimetale().then(function (response) {
            $scope.timetableMessage = response.data;
        }, function (response) {
            $scope.timetableMessage = response.data;
        });
    };

}
var app = angular.module("myApp", ['ngRoute']);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "Route/Home",
            controller: "userController"
        })
        .when("/register", {
            templateUrl: "Route/Register",
            controller: "userController"
        })
        .when("/green", {
            templateUrl: "green.htm"
        })
        .when("/blue", {
            templateUrl: "blue.htm"
        });
});
app.controller("userController", ['$scope', '$http', userController]);
function userController($scope, $http) {
    $scope.loginUser = function (userCredentials) {
        $http.get('/Authentication/LoginUser?username=' + userCredentials.username + '&password=' + userCredentials.password)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data == 404)
                $scope.successLoginMessage = "Utilizatorul \"" +  userCredentials.username + "\" nu există!";
            else if (response.data == 401)
                $scope.successLoginMessage = "Parolă incorectă!";
            else {
                $scope.successLoginMessage = userCredentials.username + " logged successfully!";
            }
        };
        function errorCallback(response) {
            alert("[Server error] An error occured when trying to check the credentials");
        };
    };
    addUser = function (userCredentials) {
        $http.post('Authentication/RegisterUser?username=' + userCredentials.username + '&password=' + userCredentials.password)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data == 201) {
                $scope.regMessage = "Register successfull!";
            }
            else {
                $scope.regMessage = "A apărut o eroare. Cod status: " + response.data;
            }
        };
        function errorCallback(response) {
            alert("[Server error] An error occured when trying to post the credentials");
        };
    }
    $scope.registerUser = function (userCredentials) {
        $http.get('Authentication/UserExists?username=' + userCredentials.username)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data == true)
                $scope.regMessage = "\"" + userCredentials.username + "\" există deja!";
            else {
                addUser(userCredentials);
            }
        };
        function errorCallback(response) {
            alert("[Server error] An error occured when trying to check the credentials");
        };
    };
};
var app = angular.module("myApp", ['ngRoute', 'angularjs-md5']);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "Account/StudentProfile",
            controller: "userController"
        })
        .when("/studentTimetable", {
            templateUrl: "Account/StudentTimetable",
            controller: "userController"
        })
        .when("/studentProfile", {
            templateUrl: "Account/StudentProfile",
            controller: "userController"
        })
        .when("/studentProfileSettings", {
            templateUrl: "Account/StudentProfileSettings",
            controller: "userController"
        })
        .when("/teacherProfile", {
            templateUrl: "Account/TeacherProfile",
            controller: "userController"
        })
        .when("/teacherProfileSettings", {
            templateUrl: "Account/TeacherProfileSettings",
            controller: "userController"
        });
});
app.directive("htmlPage", function () {
    return {
        templateUrl: "Account/StudentLayout"
    };
});
app.controller("userController", ['$scope', '$http', '$md5', '$location', userController]);
function userController($scope, $http, $md5, $location) {
    var currentUsername;
    $scope.loginUser = function (userCredentials) {
        userCredentials.password = $md5.md5(userCredentials.password);
        $http.get('/Authentication/LoginUser?username=' + userCredentials.username + '&password=' + userCredentials.password)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data === 404) {
                $scope.successLoginMessage = "Utilizatorul \"" + userCredentials.username + "\" nu există!";
                userCredentials.username = '';
                userCredentials.password = '';
            }
            else if (response.data === 401) {
                userCredentials.password = '';
                $scope.successLoginMessage = "Parolă incorectă!";
            }
            else {
            $scope.successLoginMessage = userCredentials.username + " logged successfully!";
                currentUsername = userCredentials.username;
                $http.get('/Route/HomeSession?username=' + userCredentials.username + '&password=' + userCredentials.password);
                
            }
        }
        function errorCallback(response) {
            alert("[Server error] An error occured when trying to check the credentials");
        }
    };
    addUser = function (userCredentials) {
        var userPassword = userCredentials.password;
        userPassword = $md5.md5(userPassword);
        $http.post('Authentication/RegisterUser?username=' + userCredentials.username + '&password=' + userPassword)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data === 201) {
                currentUsername = userCredentials.username;
                userCredentials.username = '';
                userCredentials.password = '';
                $scope.regMessage = "Register successfull!";
                $location.path('registerForm'); 
            }
            else {
                $scope.regMessage = "A apărut o eroare. Cod status: " + response.data;
            }
        }
        function errorCallback(response) {
            alert("[Server error] An error occured when trying to post the credentials");
        }
    };
    $scope.registerUser = function (userCredentials) {
        $http.get('Authentication/UserExists?username=' + userCredentials.username)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data === true)
                $scope.regMessage = "\"" + userCredentials.username + "\" există deja!";
            else {
                addUser(userCredentials);
            }
        }
        function errorCallback(response) {
            alert("[Server error] An error occured when trying to check the credentials");
        }
    };
    $scope.registerStudentProfile = function (student) {
        $scope.userRegSuccess = student.fullname + '\n' + student.email + '\n' + student.year +
            '\n' + student.group + '\n<' + currentUsername + '>';
        $http.post('Student/RegisterStudent?fullname=' + student.fullname + '&email=' + student.email + '&year=' + student.year +
            '&group=' + student.group + '&username=' + currentUsername).then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data === 201) {
                student.fullname = '';
                student.email = '';
                student.year = '';
                student.group = '';
                $scope.userRegSuccess = "Student profile added! -> " + currentUsername;
            } else {
                $scope.userRegSuccess = "Cannot add student profile...";
            }
        }
        function errorCallback(response) {
            alert("[Server error] An error occured when trying to add student profile");
        }
    };

    $scope.userMenu = function () {
        $scope.mess = "CLICK";
    };

    $scope.userRoleValue = function (username) {
        if ($scope.usrRole == null) {
            $http.get('User/GetUserRole?username=' + username)
                .then(successCallback, errorCallback);
            function successCallback(response) {
                if (response.data !== null)
                    $scope.usrRole = response.data;
            }
            function errorCallback(response) {
            }
        }
    };

    $scope.getStudentProfile = function (username) {
        $http.get('Student/GetStudentProfile?username=' + username)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data !== null) {
                $scope.studentFullName = response.data.fullName;
                $scope.studyYear = response.data.year;
                $scope.studentGroup = response.data.group;
                $scope.studentMail = response.data.email;
            }
        }
        function errorCallback(response) {
        }
    };

    $scope.editStudentProfilePage = function () {
        $location.path('studentProfileSettings');
    };

    $scope.editStudentProfile = function (username) {
        $http.post('Student/EditStudentProfile?username=' + username + '&studentFullName=' + $scope.studentFullName +
            '&studyYear=' + $scope.studyYear + '&studentGroup=' + $scope.studentGroup + '&studentMail=' + $scope.studentMail)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data === 200) {
                $location.path('studentProfile');
            } else {
                $scope.studentUpdateMessage = "A apărut o eroare şi schimbările nu s-au salvat";
            }
        }
        function errorCallback(response) {
        }
    };

    $scope.backToStudentProfile = function () {
        $location.path('studentProfile');
    };

    $scope.getTeacherProfile = function (username) {
        $http.get('Teacher/GetTeacherProfile?username=' + username)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data !== null) {
                $scope.teacherTitle = response.data.function;
                $scope.teacherFullName = response.data.fullName;
                $scope.teacherMail = response.data.email;
            }
        }
        function errorCallback(response) {
        }
    };

    $scope.editTeacherProfilePage = function () {
        $location.path('teacherProfileSettings');
    };

    $scope.editTeacherProfile = function (username) {
        $http.post('Teacher/EditTeacherProfile?username=' + username + '&teacherFullName=' + $scope.teacherFullName +
            '&teacherTitle=' + $scope.teacherTitle + '&teacherMail=' + $scope.teacherMail)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            if (response.data === 200) {
                $location.path('teacherProfile');
            } else {
                $scope.teacherUpdateMessage = "A apărut o eroare şi schimbările nu s-au salvat";
            }
        }
        function errorCallback(response) {
        }
    };

    $scope.backToTeacherProfile = function () {
        $location.path('teacherProfile');
    };
}
var app = angular.module("myApp", ['ngRoute', 'angularjs-md5']);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "/Account/StudentProfile",
            controller: "routeController"
        })
        .when("/studentTimetable", {
            templateUrl: "/Account/StudentTimetable",
            controller: "routeController"
        })
        .when("/teacherTimetable", {
            templateUrl: "/Account/TeacherTimetable",
            controller: "routeController"
        })
        .when("/studentProfile", {
            templateUrl: "/Account/StudentProfile",
            controller: "routeController"
        })
        .when("/studentProfileSettings", {
            templateUrl: "/Account/StudentProfileSettings",
            controller: "routeController"
        })
        .when("/teacherProfile", {
            templateUrl: "/Account/TeacherProfile",
            controller: "routeController"
        })
        .when("/teacherProfileSettings", {
            templateUrl: "/Account/TeacherProfileSettings",
            controller: "routeController"
        });
});
app.controller("routeController", ['$scope', '$http', '$md5', '$location', '$timeout', routeController]);
function routeController($scope, $http, $md5, $location, $timeout) {
    //$scope.clickRes = "abcd";
    //var currentUsername;
    //$scope.loginUser = function (userCredentials) {
    //    userCredentials.password = $md5.md5(userCredentials.password);
    //    $http.get('/Authentication/LoginUser?username=' + userCredentials.username + '&password=' + userCredentials.password)
    //        .then(successCallback, errorCallback);
    //    function successCallback(response) {
    //        if (response.data === 404) {
    //            $scope.successLoginMessage = "Utilizatorul \"" + userCredentials.username + "\" nu există!";
    //            userCredentials.username = '';
    //            userCredentials.password = '';
    //        }
    //        else if (response.data === 401) {
    //            userCredentials.password = '';
    //            $scope.successLoginMessage = "Parolă incorectă!";
    //        }
    //        else {
    //        $scope.successLoginMessage = userCredentials.username + " logged successfully!";
    //            currentUsername = userCredentials.username;
    //            $http.get('/Route/HomeSession?username=' + userCredentials.username + '&password=' + userCredentials.password);

    //        }
    //    }
    //    function errorCallback(response) {
    //        alert("[Server error] An error occured when trying to check the credentials");
    //    }
    //};
    //addUser = function (userCredentials) {
    //    var userPassword = userCredentials.password;
    //    userPassword = $md5.md5(userPassword);
    //    $http.post('Authentication/RegisterUser?username=' + userCredentials.username + '&password=' + userPassword)
    //        .then(successCallback, errorCallback);
    //    function successCallback(response) {
    //        if (response.data === 201) {
    //            currentUsername = userCredentials.username;
    //            userCredentials.username = '';
    //            userCredentials.password = '';
    //            $scope.regMessage = "Register successfull!";
    //            $location.path('registerForm'); 
    //        }
    //        else {
    //            $scope.regMessage = "A apărut o eroare. Cod status: " + response.data;
    //        }
    //    }
    //    function errorCallback(response) {
    //        alert("[Server error] An error occured when trying to post the credentials");
    //    }
    //};
    //$scope.registerUser = function (userCredentials) {
    //    $http.get('Authentication/UserExists?username=' + userCredentials.username)
    //        .then(successCallback, errorCallback);
    //    function successCallback(response) {
    //        if (response.data === true)
    //            $scope.regMessage = "\"" + userCredentials.username + "\" există deja!";
    //        else {
    //            addUser(userCredentials);
    //        }
    //    }
    //    function errorCallback(response) {
    //        alert("[Server error] An error occured when trying to check the credentials");
    //    }
    //};
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

    $scope.userRoleValue = function (username) {
        if ($scope.usrRole === undefined) {
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

    $scope.getTimetable = function () {
        $http.get('/Admin/GetTimetable').then(successCallback, errorCallback);
        function successCallback(response) {
            $scope.timetableMessage = response.data;
        }
        function errorCallback(response) {
            $scope.timetableMessage = response.data;
        }
    };

    $scope.getStudentTimetable = function (username) {
        $http.get('Student/GetStudentTimetable?username=' + username).then(successCallback, errorCallback);
        function successCallback(response) {
            $scope.studentTimetable = response.data;
            //$scope.studentTimetable[0].day = "Duminica";
            $scope.studentDailyTimetable = [];
            var days = ["Duminica", "Luni", "Marti", "Miercuri", "Joi", "Vineri", "Sambata"];
            for (var i = 0; i < $scope.studentTimetable.length; i++) {
                if ($scope.studentTimetable[i].day === days[new Date().getDay()]) {
                    $scope.studentDailyTimetable.push($scope.studentTimetable[i]);
                }
            }
        }
        function errorCallback(response) {
        }
    }

    $scope.getTeacherTimetable = function (username) {
        $http.get('Teacher/GetTeacherTimetable?username=' + username).then(successCallback, errorCallback);
        function successCallback(response) {
            $scope.teacherTimetable = response.data;
            //$scope.teacherTimetable[0].day = "Duminica";
            $scope.teacherDailyTimetable = [];
            var days = ["Duminica", "Luni", "Marti", "Miercuri", "Joi", "Vineri", "Sambata"];
            for (var i = 0; i < $scope.teacherTimetable.length; i++) {
                if ($scope.teacherTimetable[i].day === days[new Date().getDay()]) {
                    $scope.teacherDailyTimetable.push($scope.teacherTimetable[i]);
                }
            }
        }
        function errorCallback(response) {
        }
    }

    $scope.postChatMessage = function (username) {
        $http.post('Chat/PostMessage?username=' + username + '&timetableId=' + $scope.timetableId + '&message=' + $scope.chatMessage)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            $scope.chatPostResponse = response.data;
        }
        function errorCallback(response) {
            $scope.chatPostResponse = response.data;
        }
    }

    //use time1 for requests every X times
    $scope.time1 = 0;
    $scope.chatMessages = [];
    //timer1 is called every X times
    var timer1 = function () {
        //dateToShow is current date, updated every X times
        var dateToShow = new Date();
        //student daily timetable - setup startDate and endDate according to from-to from current course
        if ($scope.studentDailyTimetable)
            for (var i = 0; i < $scope.studentDailyTimetable.length; i++) {
                //start setup student session original code
                /*if (parseInt($scope.studentDailyTimetable[i].from) < new Date().getHours()
                    && parseInt($scope.studentDailyTimetable[i].to) > new Date().getHours()) {
                $scope.startDate = new Date(new Date().getFullYear(),
                    new Date().getMonth(),
                    new Date().getDate(),
                    parseInt($scope.studentDailyTimetable[i].from));
                $scope.endDate = new Date(new Date().getFullYear(),
                    new Date().getMonth(),
                    new Date().getDate(),
                    parseInt($scope.studentDailyTimetable[i].to));
                $scope.currentCourseName = $scope.studentDailyTimetable[i].name;
                }*/
                //end original code
                //start setup student session temp code
                $scope.startDate = new Date(new Date().getFullYear(),
                    new Date().getMonth(),
                    new Date().getDate(),
                    new Date().getHours()/*parseInt($scope.studentDailyTimetable[i].from)*/);
                $scope.endDate = new Date(new Date().getFullYear(),
                    new Date().getMonth(),
                    new Date().getDate(),
                    new Date().getHours() + 1/*parseInt($scope.studentDailyTimetable[i].to)*/);
                $scope.currentCourseName = $scope.studentDailyTimetable[i].name;
                //end temp code
                $scope.timetableId = $scope.studentDailyTimetable[i].id;
            }
        //teacher daily timetable - setup startDate and endDate according to from-to from current course
        if ($scope.teacherDailyTimetable)
            for (var i = 0; i < $scope.teacherDailyTimetable.length; i++) {
                //start setup teacher session original code
                /*if (parseInt($scope.teacherDailyTimetable[i].from) < new Date().getHours()
                    && parseInt($scope.teacherDailyTimetable[i].to) > new Date().getHours()) {
                $scope.startDate = new Date(new Date().getFullYear(),
                    new Date().getMonth(),
                    new Date().getDate(),
                    parseInt($scope.teacherDailyTimetable[i].from));
                $scope.endDate = new Date(new Date().getFullYear(),
                    new Date().getMonth(),
                    new Date().getDate(),
                    parseInt($scope.teacherDailyTimetable[i].to));
                $scope.currentCourseName = $scope.teacherDailyTimetable[i].name;
                }*/
                //end original code
                //start setup teacher session temp code
                $scope.startDate = new Date(new Date().getFullYear(),
                    new Date().getMonth(),
                    new Date().getDate(),
                    new Date().getHours()/*parseInt($scope.teacherDailyTimetable[i].from)*/);
                $scope.endDate = new Date(new Date().getFullYear(),
                    new Date().getMonth(),
                    new Date().getDate(),
                    new Date().getHours() + 1/*parseInt($scope.teacherDailyTimetable[i].to)*/);
                $scope.currentCourseName = $scope.teacherDailyTimetable[i].name;
                //end temp code
                $scope.timetableId = $scope.teacherDailyTimetable[i].id;
            }
        //open session and show chat during the course
        if (dateToShow > $scope.startDate && dateToShow < $scope.endDate) {
            $scope.sessionIsOpen = true;
            $scope.showChat = true;
        }
        else {
            $scope.sessionIsOpen = false;
            $scope.showChat = false;
        }
        //increment time1 every 5 sec (function timer1 is called every 5 sec)
        $scope.time1 += 5000;
        //take time of the last posted message (if is not exists, take current time)
        var lastMessageTime = ($scope.chatMessages.length === 0)
            ? (new Date().toISOString())
            : (new Date($scope.chatMessages[$scope.chatMessages.length - 1].postTime).toISOString());
        //check if is not exists any messages in current chat
        var isEmpty = ($scope.chatMessages.length === 0) ? true : false;
        //get messages from last visible in chat to last posted in database (for current course)
        $http.get('Chat/GetMessages?isEmpty=' + isEmpty + '&lastMessageTime=' + lastMessageTime + '&timetableId=' + $scope.timetableId)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            for (var i = 0; i < response.data.length; i++)
                $scope.chatMessages.push(response.data[i]);
        }
        function errorCallback(response) {

        }
        $timeout(timer1, 5000);
    }
    //infinite calling of timer1 function
    $timeout(timer1, 100);

    /*var init = function () {
        var lastMessageTime = ($scope.chatMessages.length == 0)
            ? (new Date())
            : $scope.chatMessages[$scope.chatMessages.length - 1].PostTime;
        $http.get('Chat/GetMessages?lastMessageTime=' + lastMessageTime + '&timetableId=08d59d56ff3d')
            .then(successCallback, errorCallback);
        $scope.chatMessages
    }*/
}
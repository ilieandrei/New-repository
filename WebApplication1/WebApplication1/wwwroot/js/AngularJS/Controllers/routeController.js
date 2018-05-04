var app = angular.module("myApp");

app.controller("routeController", ['$scope', '$http', '$md5', '$location', '$timeout', 'myService', routeController]);
function routeController($scope, $http, $md5, $location, $timeout, myService) {
    
    $scope.postChatMessage = function (username) {
        var data = {
            TimetableId: $scope.timetableId,
            Message: $scope.chatMessage,
            Username: username
        };
        var response = myService.postMessage(data);
        if (response.$$state.value === 200) {
            $scope.chatPostResponse = response.$$state.value;
        } else {
            $scope.chatPostResponse = response;
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
        var data = {
            IsEmpty: isEmpty,
            LastMessageTime: lastMessageTime,
            TimetableId: $scope.timetableId
        };
        var response = myService.getMessages(data);
        var responseData = response.$$state.value;
        if (responseData !== undefined) {
            for (var i = 0; i < responseData.chatModels.length; i++)
                $scope.chatMessages.push(responseData.chatModels[i]);
        }
        /*$http.get('Chat/GetMessages?isEmpty=' + isEmpty + '&lastMessageTime=' + lastMessageTime + '&timetableId=' + $scope.timetableId)
            .then(successCallback, errorCallback);
        function successCallback(response) {
            for (var i = 0; i < response.data.chatModels.length; i++)
                $scope.chatMessages.push(response.data.chatModels[i]);
        }
        function errorCallback(response) {

        }*/
        $timeout(timer1, 5000);
    };
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
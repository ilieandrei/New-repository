var app = angular.module("myApp");

app.service('myService', function ($http, $q) {
    var service = this;
    this.putStudentProfile = function (studentModel) {
        //var deferred = $q.defer();
        return $.ajax({
            type: "POST",
            data: studentModel,
            url: "Student/EditStudentProfile",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        });/*.then(function (response) {
            deferred.resolve(response);
        }, function (response) {
            deferred.resolve(response);
        });
        return deferred.promise;*/
        /*$http.post('Student/EditStudentProfile', studentModel, {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
                'X-Requested-With' :'XMLHttpRequest'
            }
        });*/
    };

    this.postStudentProfile = function (studentModel) {
        var deferred = $q.defer();
        $.ajax({
            type: "POST",
            data: studentModel,
            url: "Student/RegisterStudent",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        }).then(function (response) {
            deferred.resolve(response);
        }, function (response) {
            deferred.resolve(response);
        });
        return deferred.promise;
    };

    this.putTeacherProfile = function (teacherModel) {
        return $.ajax({
            type: "POST",
            data: teacherModel,
            url: "Teacher/EditTeacherProfile",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        });
    };

    this.postTeacherProfile = function (teacherModel) {
        var deferred = $q.defer();
        $.ajax({
            type: "POST",
            data: teacherModel,
            url: "Teacher/RegisterTeacher",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        }).then(function (response) {
            deferred.resolve(response);
        }, function (response) {
            deferred.resolve(response);
        });
        return deferred.promise;
    };

    this.postMessage = function (messageModel) {
        var deferred = $q.defer();
        $.ajax({
            type: "POST",
            data: messageModel,
            url: "Chat/PostMessage",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        }).then(function (response) {
            deferred.resolve(response);
        }, function (response) {
            deferred.resolve(response);
        });
        return deferred.promise;
    };

    this.getMessages = function (queryModel) {
        var deferred = $q.defer();
        $.ajax({
            type: "GET",
            data: queryModel,
            url: "Chat/GetMessages",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        }).then(function (response) {
            deferred.resolve(response);
        }, function (response) {
            deferred.resolve(response);
        });
        return deferred.promise;
    };

    this.getUserRole = function (username) {
        return $http.get('User/GetUserRole?username=' + username);
    };

    this.getStudentProfile = function (username) {
        return $http.get('Student/GetStudentProfile?username=' + username);
    };

    this.getTeacherProfile = function (username) {
        return $http.get('Teacher/GetTeacherProfile?username=' + username);
    };

    this.getFullTimetale = function () {
        return $http.get('/Admin/GetTimetable');
    };

    this.getStudentTimetable = function (username) {
        return $http.get('Student/GetStudentTimetable?username=' + username);
    };

    this.getTeacherTimetable = function (username) {
        return $http.get('Teacher/GetTeacherTimetable?username=' + username);
    };

    this.setCurrentTab = function (currentTab) {
        return $http.post('User/SetCurrentTab?currentTab=' + currentTab);
    };

    this.getCurrentTab = function (param) {
        return $http.get('User/GetCurrentTab');
    };
});
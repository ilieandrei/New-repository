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

    this.setCurrentTimetableId = function (currentTimetableId) {
        return $http.post('QuestionAnswer/SetCurrentTimetableId?currentTimetableId=' + currentTimetableId);
    };

    this.getCurrentTimetableId = function (param) {
        return $http.get('QuestionAnswer/GetCurrentTimetableId');
    };

    this.getTeacherCourses = function (username, timetableId) {
        return $http.get('QuestionAnswer/GetTeacherCourses?username=' + username + '&timetableId=' + timetableId);
    };

    this.addTeacherCourse = function (courseModel) {
        return $.ajax({
            type: "POST",
            data: courseModel,
            url: "QuestionAnswer/AddTeacherCourse",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        });
    };

    this.setCurrentCourseId = function (currentCourseId) {
        return $http.post('QuestionAnswer/SetCurrentCourseId?currentCourseId=' + currentCourseId);
    };

    this.getCurrentCourseId = function (param) {
        return $http.get('QuestionAnswer/GetCurrentCourseId');
    };

    this.addCourseQuestion = function (questionModel) {
        return $.ajax({
            type: "POST",
            data: questionModel,
            url: "QuestionAnswer/AddQuestion",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        });
    };

    this.getCourseQuestions = function (courseId) {
        return $http.get('QuestionAnswer/GetQuestions?courseId=' + courseId);
    };

    this.editTeacherCourse = function (courseId, courseName) {
        return $http.post('QuestionAnswer/EditCourse?courseId=' + courseId + '&courseName=' + courseName);
    };

    this.deleteTeacherCourse = function (courseId) {
        return $http.post('QuestionAnswer/DeleteCourse?courseId=' + courseId);
    };

    this.editCourseQuestion = function (questionModel) {
        return $.ajax({
            type: "POST",
            data: questionModel,
            url: "QuestionAnswer/EditQuestion",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        });
    };

    this.deleteCourseQuestion = function (questionId) {
        return $http.post('QuestionAnswer/DeleteQuestion?questionId=' + questionId);
    };

    this.getStudentCourses = function (timetableId, teacherName) {
        return $http.get('QuestionAnswer/GetStudentCourses?timetableId=' + timetableId + '&teacherName=' + teacherName);
    };

    this.setCurrentStudentTimetable = function (currentTimetableId, currentTeacherName) {
        return $http.post('QuestionAnswer/SetCurrentStudentTimetable?currentTimetableId=' + currentTimetableId + '&currentTeacherName=' + currentTeacherName);
    };

    this.getCurrentStudentTimetable = function (param) {
        return $http.get('QuestionAnswer/GetCurrentStudentTimetable');
    };

    this.getStudentQuestion = function (courseId) {
        return $http.get('QuestionAnswer/GetQuestion?courseId=' + courseId);
    };

    this.launchQuestion = function (questionId) {
        return $http.post('QuestionAnswer/LaunchQuestion?questionId=' + questionId);
    };

    this.stopTime = function (questionId) {
        return $http.post('QuestionAnswer/StopTime?questionId=' + questionId);
    };

    this.addCourseAnswer = function (answerModel) {
        return $.ajax({
            type: "POST",
            data: answerModel,
            url: "QuestionAnswer/AddAnswer",
            contentType: 'application/x-www-form-urlencoded',
            dataType: 'json'
        });
    };

    this.getTeacherAnswers = function (questionId) {
        return $http.get('QuestionAnswer/GetAnswers?questionId=' + questionId);
    };

    this.rateAnswer = function (answerId, answerRate) {
        return $http.post('QuestionAnswer/RateAnswer?answerId=' + answerId + '&answerRate=' + answerRate);
    };

    this.deleteAnswer = function (answerId) {
        return $http.post('QuestionAnswer/DeleteAnswer?answerId=' + answerId);
    };

    this.getStudentStatus = function (username, courseId) {
        return $http.get('Student/GetStudentStatus?username=' + username + '&courseId=' + courseId);
    };

    this.getStatusTimetable = function (username) {
        return $http.get('Student/GetStatusTimetable?username=' + username);
    };

    this.getStatusCourse = function (timetableId) {
        return $http.get('Student/GetStatusCourse?timetableId=' + timetableId);
    };

    this.getTeacherStatus = function (username, courseId) {
        return $http.get('Teacher/GetTeacherStatus?username=' + username + '&courseId=' + courseId);
    };

    this.getTeacherStatusTimetable = function (username) {
        return $http.get('Teacher/GetStatusTimetable?username=' + username);
    };

    this.getTeacherStatusCourse = function (timetableId) {
        return $http.get('Teacher/GetStatusCourse?timetableId=' + timetableId);
    };
});
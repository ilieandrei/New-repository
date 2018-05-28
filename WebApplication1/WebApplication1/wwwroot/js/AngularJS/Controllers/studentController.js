var app = angular.module("myApp");

app.controller("studentController", ['$scope', '$http', '$location', '$timeout', 'myService', studentController]);
function studentController($scope, $http, $location, $timeout, myService) {

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
            $scope.studentTimetable[0].day = "Luni";
            $scope.studentTimetable[1].day = "Luni";
            $scope.studentTimetable[2].day = "Luni";
            $scope.studentTimetable[3].day = "Luni";
            $scope.studentTimetable[4].day = "Luni";
            for (let i = 0; i < $scope.studentTimetable.length; i++) {
                $scope.studentTimetable[i].teacher = $scope.studentTimetable[i].teacher.split(';');//replace(/;/g, " | ");
                $scope.studentTimetable[i].teacher.pop();
            }
            $scope.studentDailyTimetable = [];
            var days = ["Duminica", "Luni", "Marti", "Miercuri", "Joi", "Vineri", "Sambata"];
            for (let i = 0; i < $scope.studentTimetable.length; i++) {
                if ($scope.studentTimetable[i].day === days[new Date().getDay()]) {
                    $scope.studentDailyTimetable.push($scope.studentTimetable[i]);
                }
            }
            $scope.dailyLen = $scope.studentDailyTimetable.length;
        }, function (response) {
        });
    };

    $scope.studentCompleteTimetablePath = function () {
        $location.path('studentFullTimetable');
    };

    $scope.backToStudentDailyTimetable = function () {
        $location.path('studentTimetable');
    };

    $scope.navigateToCourses = function (timetableId, teacherName) {
        myService.setCurrentStudentTimetable(timetableId, teacherName).then(function (response) { }, function (response) { });
        $location.path('studentCourses');
    };

    $scope.studentCourseDetails = function () {
        myService.getCurrentStudentTimetable("").then(function (response) {
            console.log(response.data);
            if (response.data[0] !== "No content" && response.data[1] !== "No content") {
                myService.getStudentCourses(response.data[0], response.data[1]).then(function (response1) {
                    if (response1.data === "") {
                        $scope.existsCourses = false;
                        $scope.noCourses = "Nu există cursuri";
                    }
                    else {
                        $scope.existsCourses = true;
                        $scope.studentCourses = response1.data;
                    }
                }, function (response1) {
                    $scope.existsCourses = false;
                    $scope.noCourses = "Nu există cursuri";
                });
            }
        }, function (response) { });
    };

    $scope.navigateToAnswerPage = function (course) {
        myService.setCurrentCourseId(course.id).then(function (response) { }, function (response) { });
        $location.path('studentAnswer');
    };

    $scope.loadAnswerData = function () {
        $scope.noAnswers = "Nu există întrebări pentru acest curs";
        myService.getCurrentCourseId("").then(function (response) {
            myService.getStudentQuestion(response.data).then(function (response1) {
                console.log(response1);
                if (response1.data === "") {
                    $scope.questionIsLaunched = false;
                } else {
                    $scope.courseQuestion = response1.data;
                    $scope.difTime = new Date().getTime() - new Date($scope.courseQuestion.launchTime).getTime();
                    $scope.time = $scope.courseQuestion.answerTime * 1000 - $scope.difTime;
                    var timer = function () {
                        if ($scope.time > 0) {
                            $scope.questionIsLaunched = true;
                            $scope.time -= 1000;
                            console.log("time elapsed: ", $scope.time);
                            $timeout(timer, 1000);
                        }
                        else if ($scope.time <= 0) {
                            console.log("finished");
                            $scope.questionIsLaunched = false;
                        }
                    }
                    $timeout(timer, 1000);
                }
            }, function (response1) {
            });
        }, function (response) { });
    };

    $scope.backToStudentCourses = function () {
        $location.path('studentCourses');
    };

    $scope.sendAnswer = function (studentAnswer, username) {
        var data = {
            QuestionId: $scope.courseQuestion.id,
            StudentUsername: username,
            AnswerName: studentAnswer
        };
        myService.addCourseAnswer(data).then(function (response) {
            if (response === 200) {
                $location.path('studentCourses');
            }
        }, function (response) { });
    };

    $scope.studentStatusDetails = function (username) {
        myService.getStatusTimetable(username).then(function (response) {
            $scope.studentStatusTimetable = response.data;
        }, function (response) { });
    };

    $scope.showTimetableCourses = function (timetableId) {
        myService.getStatusCourse(timetableId).then(function (response) {
            if (response.data.length === 0) {
                $scope.existsStatusCourses = false;
                $scope.noStatusCourses = "Nu există cursuri";
            }
            else {
                $scope.existsStatusCourses = true;
                $scope.studentStatusCourses = response.data;
            }
        }, function (response) {
            $scope.existsStatusCourses = false;
            $scope.noStatusCourses = "Nu există cursuri";
        });
    };

    $scope.showCourseStatus = function (courseId, username) {
        myService.getStudentStatus(username, courseId).then(function (response) {
            if (response.data.length === 0) {
                $scope.existsStatus = false;
                $scope.noStatus = "Nu există întrebări";
            }
            else {
                $scope.existsStatus = true;
                $scope.studentStatus = response.data;
            }
        }, function (response) {
            $scope.existsStatus = false;
            $scope.noStatus = "Nu există întrebări";
        });
    };
}
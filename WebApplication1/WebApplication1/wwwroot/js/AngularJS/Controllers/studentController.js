var app = angular.module("myApp");

app.controller("studentController", ['$scope', '$http', '$location', '$timeout', 'myService', studentController]);
function studentController($scope, $http, $location, $timeout, myService) {

    $scope.studentProfileValue = function (username) {
        $scope.studyYears = ["1", "2", "3"];
        $scope.studyHalfyears = ["A", "B"];
        myService.getStudentProfile(username).then(function (response) {
            $scope.studentDetails = {
                studentFullName: response.data.fullName,
                studyYear: response.data.year,
                studentHalfyear: response.data.group[0],
                studentGroup: response.data.group[1]
            };
        }, function (response) {
        });
    };

    $scope.editStudentProfilePage = function () {
        $location.path('studentProfileSettings');
    };

    $scope.editStudentProfile = function (username, studentDetails) {
        var data = {
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
            /*$scope.studentTimetable[0].day = "Duminica";
            $scope.studentTimetable[1].day = "Duminica";
            $scope.studentTimetable[2].day = "Duminica";
            $scope.studentTimetable[3].day = "Duminica";
            $scope.studentTimetable[4].day = "Duminica";*/
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
        //mesajul de eroare
        $scope.noAnswers = "Nu există întrebări pentru acest curs";
        //obtine ID-ul cursului curent
        myService.getCurrentCourseId("").then(function (response) {
            //obtine intrebarea lansata in cursul curent
            myService.getStudentQuestion(response.data).then(function (response1) {
                console.log(response1);
                //afiseaza mesajul de eroare
                if (response1.data === "") {
                    $scope.questionIsLaunched = false;
                } else {
                    $scope.courseQuestion = response1.data;
                    //calculeaza diferenta de timp scurs de la lansarea intrebarii pana in prezent
                    $scope.difTime = new Date().getTime() - new Date($scope.courseQuestion.launchTime).getTime();
                    $scope.time = $scope.courseQuestion.answerTime * 1000 * 60 - $scope.difTime;
                    //porneste cronometrul
                    var timer = function () {
                        //scade timpul secunda cu secunda
                        if ($scope.time > 0) {
                            $scope.questionIsLaunched = true;
                            $scope.time -= 1000;
                            console.log("time elapsed: ", $scope.time);
                            $timeout(timer, 1000);
                        }
                        //s-a scurs timpul
                        else if ($scope.time <= 0) {
                            console.log("finished");
                            $scope.questionIsLaunched = false;
                        }
                    }
                    //apeleaza functia timer() pana expira timpul
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

    $scope.timetableChanged = function (selectedTimetable, username) {
        myService.getStatusCourse(selectedTimetable.id, username).then(function (response) {
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

    $scope.courseChanged = function (selectedCourse, username) {
        myService.getStudentStatus(username, selectedCourse.id).then(function (response) {
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

    $scope.studentStatusDetails = function (username) {
        myService.getStatusTimetable(username).then(function (response) {
            $scope.studentStatusTimetable = response.data;
        }, function (response) { });
    };

    $scope.showTimetableCourses = function (timetableId, username) {
        myService.getStatusCourse(timetableId, username).then(function (response) {
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
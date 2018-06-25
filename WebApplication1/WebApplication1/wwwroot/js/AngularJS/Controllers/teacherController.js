var app = angular.module("myApp");

app.controller("teacherController", ['$scope', '$http', '$location', '$timeout', 'myService', teacherController]);
function teacherController($scope, $http, $location, $timeout, myService) {

    $scope.teacherProfileValue = function (username) {
        $scope.teacherTitles = [
            "",
            "Conf. dr.",
            "Colab.",
            "Asist. dr.",
            "Lect. dr.",
            "Lect.",
            "drd.",
            "Colab. dr.",
            "Prof. dr.",
            "Asist. drd.",
            "Cerc. dr.",
            "Prof.",
            "Asist."
        ]
        myService.getTeacherProfile(username).then(function (response) {
            $scope.teacherDetails = {
                teacherTitle: response.data.function,
                teacherFullName: response.data.fullName
            };
        }, function (response) {
        });
    };

    $scope.editTeacherProfilePage = function () {
        $location.path('teacherProfileSettings');
    };

    $scope.editTeacherProfile = function (username, teacherDetails) {
        var data = {
            FullName: $scope.teacherDetails.teacherFullName,
            Function: $scope.teacherDetails.teacherTitle,
            Username: username
        };
        myService.putTeacherProfile(data).then(function (response) {
            if (response === 200)
                $location.path('teacherProfile');
        }, function (response) {
            $scope.teacherUpdateMessage = "A apărut o eroare şi schimbările nu s-au salvat";
        });
    };

    $scope.backToTeacherProfile = function () {
        $location.path('teacherProfile');
    };

    $scope.teacherTimetableValue = function (username) {
        myService.getTeacherTimetable(username).then(function (response) {
            $scope.teacherTimetable = response.data.timetables;
            $scope.teacherDailyTimetable = [];
            var days = ["Duminica", "Luni", "Marti", "Miercuri", "Joi", "Vineri", "Sambata"];
            /*$scope.teacherTimetable[0].day = "Luni";
            $scope.teacherTimetable[1].day = "Luni";*/
            for (var i = 0; i < $scope.teacherTimetable.length; i++) {
                if ($scope.teacherTimetable[i].day === days[new Date().getDay()]) {
                    $scope.teacherDailyTimetable.push($scope.teacherTimetable[i]);
                }
            }
        }, function (response) {
        });
    };

    $scope.teacherCompleteTimetablePath = function () {
        $location.path('teacherFullTimetable');
    };

    $scope.backToTeacherDailyTimetable = function () {
        $location.path('teacherTimetable');
    };

    $scope.navigateToCourses = function (timetableId) {
        myService.setCurrentTimetableId(timetableId).then(function (response) { }, function (response) { });
        $location.path('teacherCourses');
    };
    var currentTimeTableId;
    $scope.teacherCourseDetails = function (username) {
        myService.getCurrentTimetableId("").then(function (response) {
            if (response.data !== "No content") {
                currentTimeTableId = response.data;
                myService.getTeacherCourses(username, response.data).then(function (response1) {
                    $scope.questionIsLaunched = false;
                    $scope.teacherCourses = response1.data;
                }, function (response1) {
                    $scope.teacherCourses = response1;
                });
            }
        }, function (response) { });
    };

    $scope.addTeacherCourse = function (username, teacherCourse) {
        var data = {
            Username: username,
            TimetableId: currentTimeTableId,
            Title: teacherCourse.title
        };
        myService.addTeacherCourse(data).then(function (response) {
            if (response === 200)
                myService.getTeacherCourses(username, currentTimeTableId).then(function (response1) {
                    $scope.teacherCourses = response1.data;
                }, function (response1) {
                    $scope.teacherCourses = response1;
                });
        }, function (response) {
            $scope.teacherCourses = response;
        });
    };

    var currentCourseId;
    $scope.navigateToQuestionModal = function (courseId) {
        currentCourseId = courseId;
    }

    $scope.initQuestionModal = function (courseQuestion) {
        console.log(courseQuestion);
    };

    $scope.addCourseQuestion = function (courseQuestion, username) {
        if (courseQuestion !== undefined) {
            var data = {
                Id: 0,
                CourseId: currentCourseId,
                QuestionName: courseQuestion.questionName,
                AnswerTime: courseQuestion.answerTime
            };
            myService.addCourseQuestion(data).then(function (response) {
                var myVar = $scope.teacherCourseDetails(username);
            }, function (response) {
            });
        }
    };

    $scope.showCourseQuestions = function (courseId) {
        console.log(courseId);
        myService.getCourseQuestions(courseId).then(function (response) {
            if (response.data.length === 0) {
                $scope.showQuestionTable = false;
                $scope.noQuestionsMessage = "Nu există întrebări"
            }
            else {
                $scope.showQuestionTable = true;
                $scope.noQuestionsMessage = "";
                $scope.courseQuestions = response.data;
            }
        }, function (response) { });
    };

    $scope.populateCourseField = function (teacherCourse) {
        $scope.thisTeacherCourse = teacherCourse;
    };

    $scope.editTeacherCourse = function (teacherCourse) {
        myService.editTeacherCourse(teacherCourse.id, teacherCourse.title).then(function (response) {
            console.log("Course saved");
        }, function (response) {
            console.log("Course NOT saved");
        });
    };

    $scope.navigateToDeleteCourseModal = function (course) {
        $scope.thisTeacherCourse = course;
    };

    $scope.deleteTeacherCourse = function (teacherCourse, username) {
        myService.deleteTeacherCourse(teacherCourse.id).then(function (response) {
            var myVar = $scope.teacherCourseDetails(username);
        }, function (response) { });
    };

    $scope.navigateToEditQuestionModal = function (courseQuestion) {
        $scope.thisCourseQuestion = courseQuestion;
    };

    $scope.editCourseQuestion = function (courseQuestion) {
        var data = {
            Id: courseQuestion.id,
            QuestionName: courseQuestion.questionName,
            AnswerTime: courseQuestion.answerTime,
            CourseId: courseQuestion.courseId
        };
        myService.editCourseQuestion(data).then(function (response) {
            var myVar = $scope.showCourseQuestions(courseQuestion.courseId);
        }, function (response) { });
    };

    $scope.navigateToDeleteQuestionModal = function (courseQuestion) {
        $scope.thisCourseQuestion = courseQuestion;
    };

    $scope.deleteCourseQuestion = function (courseQuestion) {
        myService.deleteCourseQuestion(courseQuestion.id).then(function (response) {
            var myVar = $scope.showCourseQuestions(courseQuestion.courseId);
        }, function (response) { });
    };

    $scope.launchQuestion = function (question, username) {
        myService.launchQuestion(question.id).then(function (response) {
            console.log("question is launched!");
            $scope.time = question.answerTime * 1000 * 60;
            var timer = function () {
                if ($scope.time > 0) {
                    $scope.questionIsLaunched = true;
                    $scope.time -= 1000;
                    console.log("time elapsed: ", $scope.time);
                    $timeout(timer, 1000);
                }
                else if ($scope.time === 0) {
                    console.log("finished");
                    myService.stopTime(question.id).then(function (response1) {
                        myService.getCourseQuestions(question.courseId).then(function (response2) {
                            if (response2.data.length === 0) {
                                $scope.showQuestionTable = false;
                                $scope.noQuestionsMessage = "Nu există întrebări"
                            }
                            else {
                                $scope.showQuestionTable = true;
                                $scope.noQuestionsMessage = "";
                                $scope.courseQuestions = response2.data;
                            }
                        }, function (response) { });
                        console.log(response1);
                    }, function (response1) {
                        console.log(response1)
                    });
                    $scope.questionIsLaunched = false;
                }
            }
            $timeout(timer, 1000);
        }, function (response) { });
    };

    $scope.navigateToAnswersPage = function (course) {
        myService.setCurrentCourseId(course.id).then(function (response) { }, function (response) { });
        $location.path('teacherAnswers');
    };

    $scope.teacherAnswersDetails = function () {
        myService.getCurrentCourseId("").then(function (response) {
            if (response.data !== "No content") {
                myService.getTeacherAnswers(response.data).then(function (response1) {
                    $scope.teacherAnswers = response1.data;
                }, function (response1) {
                });
            }
        }, function (response) { });
    };

    $scope.rateAnswer = function (answer, rating) {
        myService.rateAnswer(answer.id, rating).then(function (response) { }, function (response) { });
    };

    $scope.resetRate = function (answer) {
        myService.rateAnswer(answer.id, 0).then(function (response) { }, function (response) { });
    };

    $scope.navigateToDeleteAnswerModal = function (answer) {
        $scope.thisAnswer = answer;
    };

    $scope.deleteAnswer = function (answer) {
        myService.deleteAnswer(answer.id).then(function (response) {
            myService.getCurrentCourseId("").then(function (response1) {
                if (response1.data !== "No content") {
                    myService.getTeacherAnswers(response1.data).then(function (response2) {
                        $scope.teacherAnswers = response2.data;
                    }, function (response2) {
                    });
                }
            }, function (response1) { });
        }, function (response) { });
    };

    $scope.backToTeacherCourses = function () {
        $location.path('teacherCourses');
    };

    $scope.teacherStatusDetails = function (username) {
        myService.getTeacherStatusTimetable(username).then(function (response) {
            $scope.teacherStatusTimetable = response.data;
        }, function (response) { });
    };

    $scope.timetableChanged = function (selectedTimetable) {
        myService.getTeacherStatusCourse(selectedTimetable.id).then(function (response) {
            if (response.data.length === 0) {
                $scope.existsStatusCourses = false;
                $scope.noStatusCourses = "Nu există cursuri";
            }
            else {
                $scope.existsStatusCourses = true;
                $scope.teacherStatusCourses = response.data;
            }
        }, function (response) {
            $scope.existsStatusCourses = false;
            $scope.noStatusCourses = "Nu există cursuri";
        });
    };

    $scope.courseChanged = function (selectedCourse, username) {
        myService.getTeacherStatus(username, selectedCourse.id).then(function (response) {
            if (response.data.length === 0) {
                $scope.existsStatus = false;
                $scope.noStatus = "Nu există întrebări";
            }
            else {
                $scope.existsStatus = true;
                $scope.teacherStatus = response.data;
            }
        }, function (response) {
            $scope.existsStatus = false;
            $scope.noStatus = "Nu există întrebări";
        });
    };

    $scope.showTimetableCourses = function (timetableId, index) {
        $scope.tIndex = index;
        console.log($scope.tIndex);
        myService.getTeacherStatusCourse(timetableId).then(function (response) {
            if (response.data.length === 0) {
                $scope.existsStatusCourses = false;
                $scope.noStatusCourses = "Nu există cursuri";
            }
            else {
                $scope.existsStatusCourses = true;
                $scope.teacherStatusCourses = response.data;
            }
        }, function (response) {
            $scope.existsStatusCourses = false;
            $scope.noStatusCourses = "Nu există cursuri";
        });
    };

    $scope.showCourseStatus = function (courseId, username, index) {
        myService.getTeacherStatus(username, courseId).then(function (response) {
            if (response.data.length === 0) {
                $scope.existsStatus = false;
                $scope.noStatus = "Nu există întrebări";
            }
            else {
                $scope.existsStatus = true;
                $scope.teacherStatus = response.data;
            }
        }, function (response) {
            $scope.existsStatus = false;
            $scope.noStatus = "Nu există întrebări";
        });
    };

    $scope.navigateToStudentsStatus = function () {
        $location.path('studentsStatus');
    };

    $scope.navigateToTeacherStatus = function () {
        $location.path('teacherStatus');
    };

    $scope.showTimetableStudents = function (timetableId) {
        $scope.noStudentsStatus="Această materie nu conține întrebări"
        myService.getStudentsStatus(timetableId).then(function (response) {
            if (response.data.length !== 0) {
                $scope.existsStudentStatusCourses = true;
                $scope.studentsStatus = response.data;
            }
            else
                $scope.existsStudentStatusCourses = false;
        }, function (response) {
            $scope.existsStudentStatusCourses = false;
        });
    };
}
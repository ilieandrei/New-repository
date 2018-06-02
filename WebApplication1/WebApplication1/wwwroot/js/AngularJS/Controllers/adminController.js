var app = angular.module("myApp");

app.controller("adminController", ['$scope', '$http', '$location', '$timeout', 'myService', adminController]);
function adminController($scope, $http, $location, $timeout, myService) {

    $scope.getTimetableValue = function () {
        myService.getFullTimetale().then(function (response) {
            $scope.timetable = response.data;
        }, function (response) {
        });
    };

    $scope.pleaseWait = "";
    $scope.refreshTimetable = function () {
        $scope.pleaseWait = "Așteaptă...";
        myService.refreshFullTimetale().then(function (response) {
            $scope.timetable = response.data;
            $scope.pleaseWait = "";
        }, function (response) {
        });
    };

    $scope.isInUpdate = false;
    $scope.uptadeWarningMessage = "Procesul de actualizare a început. Vă rugăm să NU navigați pe alte pagini din aplicație pentru a nu întrerupe procesul."
    $scope.deletingAnswersMessage = "Se șterg răspunsurile...";
    $scope.answersDeletedMessage = "";
    $scope.deletingQuestionsMessage = "";
    $scope.questionsDeletedMessage = "";
    $scope.deletingCoursesMessage = "";
    $scope.coursesDeletedMessage = "";
    $scope.deletingTimetablesMessage = "";
    $scope.timetablesDeletedMessage = "";
    $scope.updatingTimetableMessage = "";
    $scope.timetableUpdatedMessage = "";
    $scope.uptadeTimetable = function () {
        var r = confirm("ATENȚIE! Vă recomandăm să actualizați orarul cât se poate de rar,"
            + " deoarece presupune ștergerea acestuia și, implicit, ștergerea cursurilor, întrebărilor și răspunsurilor asociate!"
            + " Dacă nu există schimbări majore (ex. schimbarea anului sau semestrului), folosiți funcția de reîmprospătare.\n"
            + "Confirmați dacă doriți o actualizare riguroasă a orarului");
        if (r === true) {
            $scope.isInUpdate = true;
            myService.deleteAnswers().then(function (response1) {
                $scope.answersDeletedMessage = "Răspunsurile au fost șterse";
                $scope.deletingQuestionsMessage = "Se șterg întrebările...";
                myService.deleteQuestions().then(function (response2) {
                    $scope.questionsDeletedMessage = "Întrebările au fost șterse";
                    $scope.deletingCoursesMessage = "Se șterg cursurile...";
                    myService.deleteCourses().then(function (response3) {
                        $scope.coursesDeletedMessage = "Cursurile au fost șterse";
                        $scope.deletingTimetablesMessage = "Se șterge orarul...";
                        myService.deleteTimetables().then(function (response4) {
                            $scope.timetablesDeletedMessage = "Orarul a fost șters";
                            $scope.updatingTimetableMessage = "Se actualizează orarul...";
                            myService.updateTimetable().then(function (response5) {
                                $scope.time = 10000;
                                var timer = function () {
                                    if ($scope.time > 0) {
                                        $scope.time -= 1000;
                                        $timeout(timer, 1000);
                                        $scope.timetableUpdatedMessage = "Orarul a fost actualizat. Veți fi redirecționat pe pagina orarului în " + $scope.time / 1000 + " secunde";
                                    }
                                    else {
                                        $scope.timetable = response5.data;
                                        $scope.isInUpdate = false;
                                    }
                                }
                                $timeout(timer, 1000);
                            })
                        }, function (response) { });
                    }, function (response) { });
                }, function (response) { });
            }, function (response) { });
        }
    };

    $scope.getUsersValue = function () {
        myService.getUsers().then(function (response) {
            $scope.users = response.data;
        }, function (response) {
        });
    };

    $scope.blockUser = function (username) {
        myService.blockUser(username).then(function (response) {
            myService.getUsers().then(function (response1) {
                $scope.users = response1.data;
            }, function (response1) {
            });
        }, function (response) {
        });
    };

    $scope.unlockUser = function (username) {
        myService.unlockUser(username).then(function (response) {
            myService.getUsers().then(function (response1) {
                $scope.users = response1.data;
            }, function (response1) {
            });
        }, function (response) {
        });
    };

}
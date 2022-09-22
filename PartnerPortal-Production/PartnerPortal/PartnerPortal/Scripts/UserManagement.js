
app.controller("CMSUserManagementController", function ($controller, $scope, $http, $rootScope,
            notificationFactory, validationFactory, dbUtilFactory, $window, $modal, $log) {

    $scope.User = {};
    $scope.Users = [];
    
    $scope.inIt = function () {
        var url = rootUrl + 'api/ppcmsapi/GetUsers';
        dbUtilFactory.get(url, "", function (d) {
            $scope.Users = d;

        }, '', 'Error getting data ');
    }

    $scope.inIt();

    $scope.GetBlankUser = function () {
        $scope.User = {};
        $scope.Id = 0;
        $scope.User.FirstName = "";
        $scope.User.LastName = "";
        $scope.User.Email = "";
        $scope.User.Phone = "";
        $scope.User.LogInName = "";
        $scope.User.LogInPassword = "";
        $scope.User.UserLevel = 0;
        $scope.User.Additional = "";
    };

    $scope.AddUser = function () {
        $scope.GetBlankUser();
        $("#displayTopicList").slideUp("slow", function () {
            $("#UpdateTopic").slideDown("slow", function () {
                // Animation complete.
            });
        });
    };

    $scope.EditUser = function (user) {
        $scope.User = user;
        $("#displayTopicList").slideUp("slow", function () {
            $("#UpdateTopic").slideDown("slow", function () {
                // Animation complete.
            });
        });
    };

    $scope.DeleteUser = function (user) {
        if (!$scope.EvaluateDelete(user)) {
            swal("This is the last administrator. You cant delete it, as there should be at least one Administrator.");
            return;
        }
        var text = "You won't be able to revert this!";
        swal({
            title: "Are you sure?",
            text: text,
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete the user.",
            closeOnConfirm: true
        },
        function (isConfirm) {
            if (isConfirm) {
                dbUtilFactory.postp(rootUrl + 'api/ppcmsapi/DeleteUser/', user, function () {
                    $scope.inIt();
                    swal(
                        'Deleted!',
                        'The User has been deleted.',
                        'success'
                    );
                }, '', 'Error getting data ');
            }
        });
    };

    $scope.CancelUpdate = function () {
        $("#UpdateTopic").slideUp("slow", function () {
            $("#displayTopicList").slideDown("slow", function () {
                // Animation complete.
            });
        });
    };

    $scope.EvaluateDelete = function (user) {
        if (user.UserLevel == 1) {
            var c = 0;
            for(var i =0;i < $scope.Users.length;i++)
            {
                if ($scope.Users[i].UserLevel == 1) {
                    c = c + 1;
                }
            }
            if (c > 1) {
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }
    };


    $scope.SaveUser = function () {
        if ($scope.User.LoginId == "") {
            swal("Log in name is required");
            return;
        }
        if ($scope.User.LoginPassword == "") {
            swal("Log in password is required");
            return;
        }
        if ($scope.User.Email != "") {
            if (!$scope.validateEmail($scope.User.Email)) {
                swal("Invalid email address");
                return;
            }
        }
        dbUtilFactory.postp(rootUrl + 'api/ppcmsapi/SaveUser/', $scope.User, function (d) {
            $scope.inIt();
            $scope.User = {};
            $scope.CancelUpdate();
        }, '', 'Error getting data ');
       
    };

    $scope.validateEmail =function(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }

    $scope.$watch('UserTopics ', function (nowSelected) {
        if (!nowSelected) {
            return;
        }
        angular.forEach(nowSelected, function (val) {
            if (val.Id != undefined) {
                $scope.UserTopics.push(val.Id.toString());
            }
        });
    });
});

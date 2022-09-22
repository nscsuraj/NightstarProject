
app.controller("CMSMetaTagModalManagementController", function ($controller, $scope, $http, $rootScope,
            notificationFactory, validationFactory, dbUtilFactory, $window, $modalInstance, $modal, $log) {

    $scope.MetaTag = {};
    $scope.MetaTag.Page = {};
    $scope.MetaTags = [];
    $scope.Pages = [];
    $scope.MetaTag.PageId = $("#hdnPageId").val();

    $scope.inIt = function () {
        var url = rootUrl + 'api/pppageinfo/GetPages';
        dbUtilFactory.get(url, "", function (d) {
            $scope.Pages = d;

        }, '', 'Error getting data ');

        var url = rootUrl + 'api/ppcmsapi/GetMetaTagsByPageId/' + $("#hdnPageId").val();
        dbUtilFactory.get(url, "", function (d) {
            $scope.MetaTags = d;

        }, '', 'Error getting data ');
    }

    $scope.inIt();

    $scope.GetBlankMetaTag = function () {
        $scope.MetaTag = {};
        $scope.MetaTag.Id = "0";
        $scope.MetaTag.TagType = "1";
        $scope.MetaTag.TagKey = "";
        $scope.MetaTag.TagValue = "";
        $scope.MetaTag.PageId = $("#hdnPageId").val();
    };


    $scope.AddMetaTag = function () {
        $scope.GetBlankMetaTag();
        $("#displayTopicList").slideUp("slow", function () {
            $("#UpdateTopic").slideDown("slow", function () {
                // Animation complete.
            });
        });
    };

    $scope.EditMetaTag = function (tag) {
        $scope.MetaTag = tag;
        $("#displayTopicList").slideUp("slow", function () {
            $("#UpdateTopic").slideDown("slow", function () {
                // Animation complete.
            });
        });
    };

    $scope.DeleteMetaTag = function (tag) {
        var text = "You won't be able to revert this!";
        swal({
            title: "Are you sure?",
            text: text,
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete the meta tag.",
            closeOnConfirm: true
        },
        function (isConfirm) {
            if (isConfirm) {
                dbUtilFactory.postp(rootUrl + 'api/ppcmsapi/DeleteMetaTag/', tag, function () {
                    $scope.inIt();
                    swal(
                        'Deleted!',
                        'The meta tag has been deleted.',
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
    $scope.CloseModal = function () {
        $modalInstance.dismiss('cancel');
    };

    $scope.SaveMetaTag = function () {
        //$scope.MetaTag.PageId == $("#hdnPageId").val();
        if ($scope.MetaTag.TagType == -1) {
            swal("Type of Tag is required");
            return;
        }
        //$scope.MetaTag.TagType = parseInt($scope.MetaTag.TagType);
        dbUtilFactory.postp(rootUrl + 'api/ppcmsapi/SaveMetaTag/', $scope.MetaTag, function (d) {
            $scope.inIt();
            $scope.MetaTag = {};
            $scope.CancelUpdate();
        }, '', 'Error getting data ');
       
    };

});

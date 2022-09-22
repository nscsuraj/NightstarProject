
app.controller("CMSMetaTagManagementController", function ($controller, $scope, $http, $rootScope,
            notificationFactory, validationFactory, dbUtilFactory, $window, $modal, $log) {

    $scope.MetaTag = {};
    $scope.MetaTag.Page = {};
    $scope.MetaTags = [];
    $scope.FilteredMetaTags = [];
    $scope.Pages = [];
    $scope.PreviousTagRowPageId = -1;
    $scope.RowClickedTagId = -1;
    $scope.filterName = "";
    //$scope.StopRowHiding = false;

    $scope.inIt = function () {
        var url = rootUrl + 'api/pppageinfo/GetPages';
        dbUtilFactory.get(url, "", function (d) {
            $scope.Pages = d;

        }, '', 'Error getting data ');

        var url = rootUrl + 'api/ppcmsapi/GetMetaTags';
        dbUtilFactory.get(url, "", function (d) {
            $scope.MetaTags = d;

        }, '', 'Error getting data ');
    }

    $scope.getPageList = function(callback) {
        callback($scope.Pages);
    }

    $scope.SetRowClickedTagId = function (tag) {
        var result = $.grep($scope.MetaTags, function (e) { return e.PageId == tag.PageId });
        $(result).each(function (key, value) {
            value.Expanded = !value.Expanded;
        });
    }

    $scope.processFilteredList = function () {
        var len = $scope.FilteredMetaTags.length;
        var prevPageId = -1;
        for (var i = 0; i < len; i++) {
            $scope.FilteredMetaTags[i].HideThisRow = false;
            if (prevPageId == $scope.FilteredMetaTags[i].PageId) {
                $scope.FilteredMetaTags[i].HideThisRow = true;
            }
            prevPageId = $scope.FilteredMetaTags[i].PageId;
        }
    }
    //$scope.$watch('filterName', function () {
    //    alert($scope.FilteredMetaTags.length);
    //});
    //$scope.IsDisplayRow = function (tag) {
    //    return tag.ID == $scope.RowClickedTagId;
    //}

    $scope.inIt();

    $scope.GetBlankMetaTag = function () {
        $scope.MetaTag = {};
        $scope.MetaTag.Id = "0";
        $scope.MetaTag.TagType = "1";
        $scope.MetaTag.TagKey = "";
        $scope.MetaTag.TagValue = "";
        $scope.MetaTag.Page = {};
        $scope.MetaTag.PageId = -1;
    };

    $scope.pageSelectionChanged = function (value) {
        $scope.MetaTag.PageId = value.Id;
    }

    //$scope.ShowPageNameRow = function (tag) {
    //    //if (tag.PageId == $scope.PreviousTagRowPageId) {
    //    //    return true;
    //    //} else {
    //    //    $scope.PreviousTagRowPageId = tag.PageId;
    //    //    return false;
    //    //}
    //    if ($scope.StopRowHiding) {
    //        return false;
    //    }
    //    return tag.PageTitle == "";
    //}

    $scope.GetPage = function (pageId) {
        var result = $.grep($scope.Pages, function (e) { return e.Id == pageId; })[0];

        return result != undefined ? result : {};
    }
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
        $scope.MetaTag.Page = $scope.GetPage(tag.PageId);
//        $scope.MetaTag.TagType = tag.TagType.toString();
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


    $scope.SaveMetaTag = function () {
        if ($scope.MetaTag.PageId == -1) {
            swal("Page is required");
            return;
        }
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

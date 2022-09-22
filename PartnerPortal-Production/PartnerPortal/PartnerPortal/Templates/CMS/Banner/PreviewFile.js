app.controller("PreviewFile", function ($controller, $scope, $http, $rootScope, SweetAlert,
    notificationFactory, $location,
    validationFactory, filterFilter, helperFactory, dbUtilFactory, $window, $modal, $modalInstance, $log, Advertisement) {

    $scope.FilePath = Advertisement.FilePath;
    $scope.FileType = Advertisement.FileType;

    $scope.cancel = function () { $modalInstance.dismiss('cancel'); }

    $scope.close = function () {
        $modalInstance.close();
    }
});
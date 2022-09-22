app.controller("SelectedParamModalCtrl", function($controller, $scope, $http, $rootScope, SweetAlert,
    notificationFactory, $location,
    validationFactory, filterFilter, helperFactory, dbUtilFactory, $window, $modal, $modalInstance, $log, List, Header) {

    $scope.Header = Header;
    $scope.List = List;

    $scope.cancel = function() { $modalInstance.dismiss('cancel'); }

    $scope.close = function() {
        $modalInstance.close();
    }
});
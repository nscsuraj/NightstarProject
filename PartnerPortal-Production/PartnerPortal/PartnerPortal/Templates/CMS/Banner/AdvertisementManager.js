"use strict";
(function () {
    app.factory("entityService",
           ["akFileUploaderService", function (akFileUploaderService) {
               var saveTutorial = function (tutorial,url) {
                   return akFileUploaderService.saveModel(tutorial, url);
               };
               return {
                   saveTutorial: saveTutorial
               };
           }]);
})();

app.controller("AddAdvertisementController", function($controller, $scope, $http, $rootScope, SweetAlert,
    notificationFactory, $location,
    validationFactory, filterFilter, helperFactory, dbUtilFactory, $window, $modal, $modalInstance, $log, entityService, Advertisement) {

    $scope.IsAdd = true;
    $scope.IsUpdate = false;
    $scope.loader = false;
    $scope.Advertisement = {};
    $scope.Advertisement.FileType = 'Image';
    $scope.Advertisement.LinkTarget = "_blank";

    $scope.Advertisement.LocationNames = "";
    $scope.Advertisement.Locations = [];
    $scope.Advertisement.LocationIds = [];

    $scope.Submit = function() {
        entityService.saveTutorial($scope.Advertisement, "/api/advertisementmanager/Add")
            .then(function (data) {
                Advertisement.Id = data;
                Advertisement.AdTitle = $scope.Advertisement.AdTitle;
                Advertisement.Description = $scope.Advertisement.Description;
                Advertisement.FileType = $scope.Advertisement.FileType;
                Advertisement.Status = true;
                Advertisement.Location = $scope.Advertisement.LocationNames;
                SweetAlert.swal("Advertisement added", "", "success");
                $modalInstance.close(Advertisement);
                
        });

    };

    $scope.clear = function() {
        $scope.Advertisement = {};
        $scope.Advertisement.FileType = 'Image';
        $scope.Advertisement.LinkTarget = "_self";

        $scope.Advertisement.LocationNames = "";
        $scope.Advertisement.Locations = [];
        $scope.Advertisement.LocationIds = [];

        $scope.Advertisement.AdTitle = "";
        $scope.Advertisement.Description = "";
        $scope.Advertisement.Width = "";
        $scope.Advertisement.Height = "";
        angular.element("input[type='file']").val(null);
        document.getElementById("vdP").src = "";
        document.getElementById("imgP").src = "";
 };

    $scope.DisableAddAdvertisement = function () {
        if (validationFactory.isEmpty($scope.Advertisement.LocationNames)) {
            return true;
        } else if (validationFactory.isEmpty($scope.Advertisement.AdTitle)) {
            return true;
        } else if ($scope.Advertisement.attachment == undefined || $scope.Advertisement.attachment.size === 0 || $scope.Advertisement.attachment.size > 10485760) {
            return true;
        } else if ($scope.Advertisement.attachment != undefined && !$scope.validateFileExtension()) {
            return true;
        } else {
            return false;
        }
    };

    $scope.validateFileExtension = function () {
        if ($scope.Advertisement.attachment != undefined) {
            var fileName = $scope.Advertisement.attachment.name;
            var fileExtension = "";
            if (fileName.lastIndexOf(".") > 0) {
                fileExtension = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);
            }
            return fileExtension.toLowerCase().match(/(?:gif|jpeg|jpg|png|mp4)$/);
        } else {
            return false;
        }
    };

    $scope.AdvLocationSelect = function() {
        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: rootUrl + 'Templates/CMS/Banner/AdvertisementLocation.html',
            controller: 'AdvertisementLocationController',
            width:'lg',
            resolve: { Advertisement: function () { return $scope.Advertisement; } }
        });

        modalInstance.result.then(function (advertisement) {
            $scope.Advertisement = advertisement;

        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.ShowSelectedParameters = function(Parameter, Header) {
        var List = [];
        for (var i = 0; i < Parameter.length; i++)
            List.push(Parameter[i]);
        var modalInstance = $modal.open({
            templateUrl: rootUrl + 'Templates/CMS/SelectedParamModal.html',
            controller: 'SelectedParamModalCtrl',
            resolve: { List: function() { return List; }, Header: function() { return Header; } }
        });
    };

    $scope.Validate = function(type) {
        switch(type) {
            case 'AdvLocation':
                return validationFactory.isEmpty($scope.Advertisement.LocationNames);
            case 'AdvTitle':
                return validationFactory.isEmpty($scope.Advertisement.AdTitle);
            case 'AdvFile':
                return ($scope.Advertisement.Attachment == undefined || $scope.Advertisement.Attachment.size == 0) || ($scope.Advertisement.Attachment.size > 10485760);
                default :
                    return false;
        }
    };

    $scope.cancel = function () { $modalInstance.dismiss('cancel'); }
});

app.controller("UpdateAdvertisementController", function ($controller, $scope, $http, $rootScope, SweetAlert,
    notificationFactory, $location,
    validationFactory, filterFilter, helperFactory, dbUtilFactory, $window, $modal, $modalInstance, $log, entityService, Advertisement) {

    $scope.loader = false;
    $scope.Advertisement = {};

    dbUtilFactory.get('/api/advertisementmanager/GetById/', Advertisement.Id, function (d) {
        Advertisement = d.Advertisement;
        Advertisement.LocationNames = d.LocationNames;
        Advertisement.Locations = d.Locations;
        Advertisement.LocationIds = d.LocationIds;
        $scope.Advertisement.attachment = d.attachment;

        $scope.Advertisement = Advertisement;
    }, '', 'Error getting data ');

    $scope.Submit = function() {
        entityService.saveTutorial($scope.Advertisement, "/api/advertisementmanager/Update")
            .then(function (data) {
                SweetAlert.swal("Advertisement updated", "", "success");
                Advertisement.Id = data;
                Advertisement.AdTitle = $scope.Advertisement.AdTitle;
                Advertisement.Description = $scope.Advertisement.Description;
                Advertisement.FileType = $scope.Advertisement.FileType;
                Advertisement.Status = $scope.Advertisement.IsActive;
                Advertisement.Location = $scope.Advertisement.LocationNames;
                $modalInstance.close(Advertisement);
            });
    };

    $scope.clear = function () {
        $scope.Advertisement = {};
        $scope.Advertisement.FileType = 'Image';

        $scope.Advertisement.LocationNames = "";
        $scope.Advertisement.Locations = [];
        $scope.Advertisement.LocationIds = [];

        $scope.Advertisement.AdTitle = "";
        $scope.Advertisement.Description = "";
        $scope.Advertisement.Width = "";
        $scope.Advertisement.Height = "";
        angular.element("input[type='file']").val(null);
        document.getElementById("vdP").src = "";
        document.getElementById("imgP").src = "";
    };

    $scope.DisableUpdateAdvertisement = function () {
        if (validationFactory.isEmpty($scope.Advertisement.LocationNames)) {
            return true;
        } else if (validationFactory.isEmpty($scope.Advertisement.AdTitle)) {
            return true;
        } else if ($scope.Advertisement.attachment != undefined && !$scope.validateFileExtension()) {
            return true;
        } else {
            return false;
        }
    };

    $scope.validateFileExtension = function () {
        if ($scope.Advertisement.attachment != undefined) {
            var fileName = $scope.Advertisement.attachment.name;
            var fileExtension = "";
            if (fileName.lastIndexOf(".") > 0) {
                fileExtension = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);
            }
            return fileExtension.toLowerCase().match(/(?:gif|jpeg|jpg|png|mp4)$/);
        } else {
            return false;
        }
    };

    $scope.AdvLocationSelect = function () {
        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: rootUrl + 'Templates/CMS/Banner/AdvertisementLocation.html',
            controller: 'AdvertisementLocationController',
            width: 'lg',
            resolve: { Advertisement: function () { return $scope.Advertisement; } }
        });

        modalInstance.result.then(function (advertisement) {
            $scope.Advertisement = advertisement;

        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.ShowSelectedParameters = function (Parameter, Header) {
        var List = [];
        for (var i = 0; i < Parameter.length; i++)
            List.push(Parameter[i]);
        var modalInstance = $modal.open({
            templateUrl: rootUrl + 'Templates/CMS/SelectedParamModal.html',
            controller: 'SelectedParamModalCtrl',
            resolve: { List: function () { return List; }, Header: function () { return Header; } }
        });
    };

    $scope.Validate = function (type) {
        switch (type) {
            case 'AdvLocation':
                return validationFactory.isEmpty($scope.Advertisement.LocationNames);
            case 'AdvTitle':
                return validationFactory.isEmpty($scope.Advertisement.AdTitle);
            case 'AdvFile':
                return ($scope.Advertisement.Attachment == undefined || $scope.Advertisement.Attachment.size == 0) || ($scope.Advertisement.Attachment.size > 10485760);
            default:
                return false;
        }
    };

    $scope.cancel = function () { $modalInstance.dismiss('cancel'); }
});

app.controller("AdvertisementLocationController", function ($controller, $scope, $http, $rootScope, SweetAlert,
    notificationFactory, $location,
    validationFactory, filterFilter, helperFactory, dbUtilFactory, $window, $modal,$modalInstance, $log, Advertisement) {

    $scope.loader = true;
    $scope.LocationList = [];

    dbUtilFactory.get('/api/advertisementmanager/GetAdvertisementLocations/', "", function (d) {
        $scope.LocationList = d;
        
        for (var i = 0; i < $scope.LocationList.length; i++) {
            var item = $scope.LocationList[i];
            item.isSelected = false;
            for (var j = 0; j < Advertisement.LocationIds.length; j++)
                if (item.Id == Advertisement.LocationIds[j]) item.isSelected = true;
        }

        $scope.loader = false;

    }, '', 'Error getting data ');

    $scope.Submit = function () {
        Advertisement.Locations = [];
        Advertisement.LocationNames = "";
        Advertisement.LocationIds = [];
        for (var i = 0; i < $scope.LocationList.length; i++) {
            var item = $scope.LocationList[i];
            if (item.isSelected) {
                Advertisement.Locations.push(item.PositionName);// += item.Name + ", ";
                Advertisement.LocationNames += item.PositionName + ",";
                Advertisement.LocationIds.push(item.Id);
            }
        }
        var contlength = Advertisement.LocationNames.length;
        Advertisement.LocationNames = Advertisement.LocationNames.substring(0, contlength - 1);
        
        $modalInstance.close(Advertisement);
    };

    $scope.cancel = function () { $modalInstance.dismiss('cancel'); }
});
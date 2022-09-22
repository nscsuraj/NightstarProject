app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            controller: 'CrudListController',
            templateUrl: '/Public/app/partials/listview.html'
        })
        .when('/record', {
            controller: 'CrudDetailController',
            templateUrl: '/Public/app/partials/recordview.html'
        })
        .when('/record/:Id', {
            controller: 'CrudDetailController',
            templateUrl: '/Public/app/partials/recordview.html'
        })
        .otherwise({
            redirectTo: '/'
        });
}]);

app.factory('defaultFactory', [ '$http' , '$rootScope' , function ($http, $rootScope) {
    var factory = {};
    factory.activeRecord = {};
    factory.settings = { Columns: {}, CollapseDetail: false };

    factory.getObjects = function () {
        return $http({
            method: 'GET',
            url: apiURL + '/Get'
        });
    };

    factory.getSettings = function () {
        return $http({
            method: 'GET',
            url: apiURL + '/GetSettings'
        });
    };

    factory.updateObject = function (object) {
        return $http({
            method: 'PUT',
            url: apiURL + '/Put',
            data: object
        });
    };

    factory.createObject = function (object) {
        return $http({
            method: 'POST',
            url: apiURL + '/Post',
            data: object
        });
    };

    factory.getObject = function (object) {
        return $http({
            method: 'POST',
            url: apiURL + '/Get',
            data: object
        });
    };

    factory.deleteObject = function (object) {
        return $http({
            method: 'DELETE',
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            },
            url: apiURL + '/Delete',
            data: object
        });
    };

    factory.setActiveRecord = function (object) {
        $rootScope.$emit("UPDATE_ACTIVE_RECORD", object);
        factory.activeRecord = object;
    };

    return factory;
}]);

app.controller('CrudDetailController', [
    '$scope',
    'defaultFactory',
    'notificationFactory',
    '$location',
    'validationFactory',
    function ($scope, defaultFactory, notificationFactory, $location, validationFactory) {

        // Get Default Value
        $scope.settings = defaultFactory.settings;
        $scope.activeRecord = defaultFactory.activeRecord;
        $scope.prefix = $scope.settings.Prefix;

        // Check if it not initilized
        if (!(defaultFactory.settings.Columns instanceof Array)) {
            defaultFactory.getSettings().success(function (data, status, headers) {
                $scope.settings = data;
                $scope.prefix = data.Prefix;
                defaultFactory.settings = data;

                if (angularDebug) {
                    console.log(data);
                }
            });
        }

        $scope.filterLookUpValues = function (data, column) {
            return data;
        };

        $scope.getLookUpValues = function (column) {
            return column.LookUpValues;
        };


        if ($scope.settings.CollapseDetail && defaultFactory.activeRecord.Id != undefined) {
            $scope.isCollapsed = true;
        } else {
            $scope.isCollapsed = false;
        }

        // Hide button and prevent collapse when adding new record
        if ($scope.activeRecord.Id === undefined) {
            $scope.settings.CollapseDetail = false;
        }

        $scope.isCollapsed = $scope.settings.CollapseDetail;

        $scope.showWeeks = true;

        $scope.saveObject = function (object) {
            // Validate
            var validated = validationFactory.validateFields($scope.settings.Columns, $scope.settings.Prefix, '');

            if (validated) {
                if (object.isUpdating) {
                    defaultFactory.updateObject(object).success(function () {
                        notificationFactory.success('Record saved successfully');
                    })
                    .error(function () {
                        notificationFactory.error('Unable to save the record');
                    });
                } else {

                    defaultFactory.createObject(object).success(function () {
                        notificationFactory.success('Record saved successfully');

                    })
                    .error(function () {
                        notificationFactory.error('Unable to save the record');
                    });
                }

                $location.path('#/');

                defaultFactory.setActiveRecord({});
            }

        };

        $scope.cancel = function () {
            $location.path('#/');

            defaultFactory.setActiveRecord({});
        };
    }]);


app.controller('CrudListController', [
    '$scope',
    'defaultFactory',
    'notificationFactory',
    '$location',
    'validationFactory',
    'filterFilter',
    'helperFactory',
function ($scope, defaultFactory, notificationFactory, $location, validationFactory, filterFilter, helperFactory) {
    $scope.orderBy = { field: '', asc: true };
    $scope.currentRecord = {};
    $scope.isInlineInsert = false;
    // Only used when data is loaded for the first time, so we can display in the order the data arrived
    $scope.sortingStartingIndex = 0;

    $scope.setOrderBy = function (field) {
        var asc = $scope.orderBy.field === field ? !$scope.orderBy.asc : true;
        $scope.orderBy = { field: field, asc: asc };
    };

    $scope.customOrderBy = function (row, index) {
        if ($scope.orderBy.field === undefined || $scope.orderBy.field === '') {
            $scope.sortingStartingIndex++;
            return $scope.sortingStartingIndex;
        }
        $scope.sortingStartingIndex = 0;

        var columnDetails = helperFactory.getColumnDetails($scope.orderBy.field, $scope.settings.Columns);

        var text = '0';

        if (columnDetails !== null) {

            if (columnDetails.ColumnType === 'LookUp') {

                text = helperFactory.get5Chars(helperFactory.getLookUpValue(row[$scope.orderBy.field], columnDetails.LookUpValues));

            } else {

                text = helperFactory.get5Chars(row[$scope.orderBy.field]);

            }
        }

        return parseInt(text) * ($scope.orderBy.asc ? 1 : -1);
    };

    defaultFactory.getSettings().success(function (data, status, headers) {
        $scope.settings = data;
        $scope.prefix = data.Prefix;
        defaultFactory.settings = data;

        if (angularDebug) {
            console.log(data);
        }
    });

    $scope.refreshGrid = function () {
        defaultFactory.getObjects().success(function (data) {
            $scope.originalRows = data;
            $scope.rows = data;
        });

        $scope.newRecord = {};
        $scope.isInlineInsert = false;

        if (angularDebug) {
            console.log("Current Scope: refreshGrid");
            console.log($scope);
        }
    };

    $scope.toogletInlineInsert = function () {
        $scope.isInlineInsert = !$scope.isInlineInsert;

    };


    $scope.showEditMode = function (object, index) {
        object.editMode = true;

    };

    $scope.hideEditMode = function (object, restore) {
        object.editMode = false;

        if (restore) {
            $scope.refreshGrid();
        }
    };


    $scope.toggleFullEditMode = function (object) {
        var id = 0;

        if (object != undefined) {

            object.isUpdating = true;
            id = object.Id;
            defaultFactory.setActiveRecord(object);

        } else {

            defaultFactory.setActiveRecord({});

        }


        $location.path('/record/' + id);
    };

    $scope.deleteObject = function (object) {
        if (confirm('Are you sure you want to remove this record?')) {
            defaultFactory.deleteObject(object)
                .success(function () {
                    notificationFactory.success('Record deleted successfully');
                    $scope.refreshGrid();
                })
                .error(function () {
                    notificationFactory.error('Unable to delete the record');
                    $scope.refreshGrid();
                });

        }
    };

    $scope.updateObject = function (object, index) {
        var validated = validationFactory.validateFields($scope.settings.Columns, $scope.settings.Prefix, index);

        if (!validated) {

            notificationFactory.warning('Please correct the errors before saving');

        } else {

            defaultFactory.updateObject(object).success(function () {
                notificationFactory.success('Record saved successfully');
                $scope.refreshGrid();
            });

            $scope.hideEditMode(object, false);
        }
    };

    $scope.filterColumns = function (column) {
        return column.IncludeInGrid;
    };

    $scope.addNewRecord = function (object) {

        var validated = validationFactory.validateFields($scope.settings.Columns, "newRecord_", '');

        if (validated) {
            defaultFactory.createObject(object).success(function () {
                notificationFactory.success('Record saved successfully');
                $scope.refreshGrid();
            });
        }
    };

    $scope.getLookUpValue = function (value, collection) {
        return helperFactory.getLookUpValue(value, collection);
    };

    $scope.exportRecords = function () {
        window.open(apiURL + '/Export');
    };


    $scope.filterList = function (item) {

        if (angularDebug) {
            console.log('filtering ... ');
        }

        return $scope.filterByText(item, $scope.defaultFilter);
    };

    $scope.filterByText = function (item, query) {

        if (query === undefined || query === '') return true;

        query = query.toLowerCase();

        for (var i = 0; i < $scope.settings.Columns.length; i++) {
            var column = $scope.settings.Columns[i];

            if (item[column.Name] === null) {
                continue;
            }

            var data;

            if (column.ColumnType == 'LookUp') {
                data = $scope.getLookUpValue(item[column.Name], column.LookUpValues).toLowerCase();
            } else {
                data = item[column.Name].toString().toLowerCase();
            }

            if (data.indexOf(query) != -1) {
                if (angularDebug) {
                    console.log('Found : ' + query + ' in ' + data + ' on column ' + column.Name);
                }
                return true;
            } else {
                if (angularDebug) {
                    console.log('Not Found : ' + query + ' in ' + data + ' on column ' + column.Name);
                }
            }
        }

        return false;
    };

    $scope.buildHyperLink = function (row, hyperlink) {
        var link = hyperlink.HyperLinkURL;
        for (var i = 0; i < hyperlink.HyperLinkFields.length; i++) {
            link = link.replace('{' + i + '}', row[hyperlink.HyperLinkFields[i]]);
        }
        return link;
    };

    $scope.refreshGrid();
}]);
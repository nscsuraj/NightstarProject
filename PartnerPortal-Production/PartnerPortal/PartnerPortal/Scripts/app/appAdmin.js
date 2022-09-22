var app = angular.module('app', ['angularUtils.directives.dirPagination', 'ngJsonExportExcel', 'chart.js']);
var angularDebug = false;

app.factory('notificationFactory', function () {
    return {
        success: function (text) {
            toastr.success(text, "Success");
        },
        info: function (text) {
            toastr.info(text, "Info");
        },
        error: function (text) {
            toastr.error(text, "Error");
        },
        warning: function (text) {
            toastr.warning(text, "Warning");
        }
    };
});

app.factory('dbUtilFactory', ['$http', 'notificationFactory', function ($http, notificationFactory) {
    return {
        get: function (api, param, callback, success, sender) {
            $http({
                url: api + param,
                method: 'GET'
            }).success(function (data) {
                callback(data);
                if (success != '') { notificationFactory.info(success); }
            }).error(function (data, status, headers, config) {
                notificationFactory.error(sender + ": " + data.Message);
            });
        },
        postp: function (api, item, callback, success, sender) {
            $http({
                url: api,
                method: 'POST',
                data: item
            }).success(function (data) {
                callback(data);
                if (success != '') { notificationFactory.info(success); }
            }).error(function (data, status, headers, config) {
                notificationFactory.error(sender + ": " + data.Message);
            });
        }, put: function (api, item, success, sender) {
            $http({
                url: api,
                method: 'Put',
                data: item
            }).success(function (data) {
                if (success != '') { notificationFactory.info(success); }
            }).error(function (data, status, headers, config) {
                notificationFactory.error(sender + ": " + data.Message);
            });
        },
        post: function (api, item, success, sender) {
            $http({
                url: api,
                method: 'Post',
                data: item
            }).success(function (data) {
                if (success != '') { notificationFactory.info(success); }
            }).error(function (data, status, headers, config) {
                notificationFactory.error(sender + ": " + data.Message);
            });
        },
        del: function (api, param, success, sender) {
            //api = api string
            //item the object to be saved
            //success in notification message
            $http({
                url: api + param,
                method: 'DELETE',
            }).success(function (data) {
                if (success != '') { notificationFactory.info(success); }
            }).error(function (data, status, headers, config) {
                notificationFactory.error(sender + ": " + data.Message);
            });
        },
        request: function (method, param, data, callback, success, sender) {
            $http({
                url: api + param,
                method: method,
                data: data
            }).success(function (data) {
                callback(data);
                if (success != '') { notificationFactory.info(success); }
            }).error(function (data, status, headers, config) {
                notificationFactory.error(sender + ": " + data.Message);
            });
        }
    };
}]);

app.factory('validationFactory', function () {
    var intRegex = /^\d+$/;
    return {
        unbindFields: function (columns, prefix, sufix) {
            if (sufix === undefined) {
                sufix = '';
            }

            if (prefix === undefined) {
                prefix = '';
            }

            // Bind controls for validation
            for (var i = 0; i < columns.length; i++) {
                var column = columns[i];

                var controlId = '#' + prefix + column.Name + sufix;

                var control = $(controlId);

                if (control.next().hasClass('tooltip')) {

                    control.tooltip('hide');

                }

            }
        },
        validateFields: function (columns, prefix, sufix) {

            if (sufix === undefined) {
                sufix = '';
            }

            if (prefix === undefined) {
                prefix = '';
            }


            var validated = true;
            // Bind controls for validation
            for (var i = 0; i < columns.length; i++) {
                var column = columns[i];

                var controlId = '#' + prefix + column.Name + sufix;

                if (column.IsRequired && !column.IsKey) {

                    validated = validated & this.bindToolTip(controlId, column.IsRequired, column.ColumnType);

                }
            }
            return validated;
        },
        bindToolTip: function (selector, required, columnType) {
            var control = $(selector);

            var controlValue = $.trim(control.val());

            if (controlValue == '' && required) {

                this.setToolTip(control, 'Please fill out this field');

                return false;

            } else if ((columnType == 'Money' || columnType == 'Numeric') && !this.isNumeric(controlValue)) {

                this.setToolTip(control, 'Field must be a numeric value');

                return false;
            } else if (columnType == 'DateTime' && !this.isDate(controlValue)) {

                this.setToolTip(control, 'Field must be a valida date');

                return false;

            }

            control.tooltip('hide');
            return true;
        },
        isEmpty: function (t) {
            return ($.trim(t) === '');
        },
        isInteger: function (n) {
            var value = Number(n);
            return Math.floor(value) == value;
        },
        isDecimal: function (value) {
            return (!isNaN(value) && value.toString().indexOf('.') != -1);
        },
        isNumeric: function (value) {
            return (!isNaN(value));
        },
        isDate: function (value) {
            var m = moment(value);
            return m.isValid();
        },
        isEmail: function (value) {
            if (value === '') return true;
            var EMAIL_REGEXP = /^[_a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;
            var isMatchRegex = EMAIL_REGEXP.test(value);
            return isMatchRegex;
        },
        isUrl: function (value) {
            if (value === '') return true;
            var URL_REGEXP = /^((?:(ftp|http|https)?:\/\/)?(?:[\w-]+\.)+([a-z]|[A-Z]|[0-9]){2,6})|((?:[\w-]+\.)+([a-z]|[A-Z]|[0-9]){2,6})$/gi;
            var isMatchRegex = URL_REGEXP.test(value);
            return isMatchRegex;
        },
        setToolTip: function (control, message) {

            if (control.next().hasClass('tooltip')) {

                control.next().find('.tooltip-inner').html(message);

            } else {

                control.tooltip({ 'title': message, 'trigger': 'manual' });
                control.tooltip('show');

            }
        }
    };
});

app.factory('helperFactory', function() {
    return {
        getLookUpValue: function(value, collection) {

            if (value === undefined || value === null) return '';
            if (collection === undefined || collection === null) return '';


            value = value.toString().toLowerCase();

            for (var i = 0; i < collection.length; i++) {

                var collectionValue = collection[i].Value.toLowerCase();

                if (collectionValue == value) {
                    return collection[i].Key;
                }
            }
            if (angularDebug) {
                console.log('**Not Found');
                console.log('Value : ' + value);
                console.log('Collection ');
                console.log(collection);
            }

            return '';
        },

        getColumnDetails: function(name, columns) {
            if (name === undefined || name === null) return '';
            if (columns === undefined || columns === null) return '';

            for (var i = 0; i < columns.length; i++) {

                if (columns[i].Name.toLowerCase() == name.toLowerCase()) {
                    return columns[i];
                }
            }

            return null;
        },

        get5Chars: function(text) {
            if (text === undefined || text == '') return '0';

            if (!isNaN(parseInt(text))) return text;

            return text.toLowerCase().charCodeAt(0);
        },

        getAllCharCodes: function(text) {
            var out = [];

            for (var i = 0; i < text.length; i++) {
                out.push(text.charCodeAt(i));
            }

            return out.join('');

        },
        getSortNumber: function(text) {
            var out = [];

            text = text.toLowerCase();

            var n = text.length > 3 ? 3 : text.length;

            for (var i = 0; i < n; i++) {
                out.push(Math.pow(text.charCodeAt(i), n - i + 2))
            }

            return parseInt(out.join(''));
        }
    }
});

app.factory('Excel', function($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function(s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) };
    return {
        tableToExcel: function(tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
});
app.filter('to_trusted', ['$sce', function ($sce) {
    return function (text) {
        return $sce.trustAsHtml(text);
    };
}]);

app.config(function (ChartJsProvider) {
    // Configure all charts
    ChartJsProvider.setOptions({
        colors: ['#97BBCD', '#DCDCDC', '#F7464A', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'],
        chartColors:['#0074D9', '#FF4136', '#2ECC40', '#FF851B', '#7FDBFF', '#B10DC9', '#FFDC00', '#001f3f', '#39CCCC', '#01FF70', '#85144b', '#F012BE', '#3D9970', '#111111', '#AAAAAA']
    });
    // Configure all doughnut charts
    ChartJsProvider.setOptions('doughnut', {
        cutoutPercentage: 60
    });
    ChartJsProvider.setOptions('bubble', {
        tooltips: { enabled: false }
    });
});
/**
 * Module: snaAngular
 * Description: S&A Angularjs utilities
 * Version: 1.0.0
 * Last update: 11-13-2014
 */

angular.module('snaAngular', ['ui.bootstrap', 'ui.utils', 'textAngular'])
.directive('snaDocu', ['dbUtilFactory', '$sce', function (dbUtilFactory, $sce) {
    return {
        restrict: 'E',
        scope: { docuField: '@' },
        template: '<div><div ng-transclude></div><div ng-show="show" ng-bind-html="Documentation.Text" style="border:solid 1px gray;background-color:#eee;padding:10px;margin-top:10px"></div><br/></div>',
        transclude: true,
        link: function (scope, element, attrs) {
            scope.Documentation = {};
            scope.show = false;
            element.bind('click', function () {
                scope.$apply(function () {
                    if (scope.show) {
                        scope.Documentation = {};
                        scope.show = false;
                    } else {
                        dbUtilFactory.get('/api/Documentation/GetByName/', scope.docuField, function (d) {
                            scope.Documentation.Text = $sce.trustAsHtml(d.Text);
                            scope.show = true;
                        }, '', 'Error getting data ');
                        scope.show = true;
                    }
                });
            });
        }
    };
}])
.directive('snaInput', function () {
    return {
        restrict: 'E',
        //templateUrl: 'snaInput.html',
        template: '<div class="row form-group" ng-form="innerForm">'
                    + '<div class="{{columns1}}">{{title}}</div>'
                    + '<div class="{{columns2}}">'
                    + '<input type="text" class="form-control" name="fieldname" ng-model="bindModel" ng-maxlength="{{maxLength}}" ng-required="{{required}}"/>'
                    + '</div>'
                    + '<div class="{{columns3}}" ng-show="innerForm.fieldname.$error.required">required</div>'
                    + '<div class="{{columns3}}" ng-show="innerForm.fieldname.$error.maxlength">Max length exceeded</div>'
                    + '</div>',
        scope: {
            bindModel: '=ngModel',
            maxLength: '@ngMaxlength',
            required: '@ngRequired'
        },
        link: function (scope, element, attrs) {
            var cols = [];
            if (attrs["cols"]) {
                cols = attrs["cols"].split(',');
            }
            scope.columns1 = isNaN(cols[0]) ? "col-md-1" : "col-md-" + cols[0];
            scope.columns2 = isNaN(cols[1]) ? "col-md-1" : "col-md-" + cols[1];
            scope.columns3 = isNaN(cols[2]) ? "col-md-1" : "col-md-" + cols[2];
            scope.title = attrs["title"];
            scope.required = !scope.required ? "false" : scope.required;
            scope.maxLength = !scope.maxLength ? "0" : scope.maxLength;
        }
    };
})
.directive('snaTextarea', function () {
        return {
            restrict: 'E',
            //templateUrl: 'snaInput.html',
            template: '<div class="row form-group" ng-form="innerForm">'
                        + '<div class="{{columns1}}">{{title}}</div>'
                        + '<div class="{{columns2}}">'
                        + '<div text-angular  name="fieldname" ng-model="bindModel" ng-required="{{required}}"></div>'
                        + '<br/><div style="margin-top:-15px; margin-left:5px" ng-show="innerForm.fieldname.$error.required">required</div>'
                        + '</div>'
                        + '</div>',
            scope: {
                bindModel: '=ngModel',
                required: '@ngRequired'
            },
            link: function (scope, element, attrs) {
                var cols = [];
                if (attrs["cols"]) {
                    cols = attrs["cols"].split(',');
                }
                scope.columns1 = isNaN(cols[0]) ? "col-md-1" : "col-md-" + cols[0];
                scope.columns2 = isNaN(cols[1]) ? "col-md-1" : "col-md-" + cols[1];
                scope.columns3 = isNaN(cols[2]) ? "col-md-1" : "col-md-" + cols[2];
                scope.title = attrs["title"];
                scope.required = !scope.required ? "false" : scope.required;
            }
        };
    })
.directive('snaLabel', function () {
    return {
        restrict: 'E',
        //templateUrl: 'snaInput.html',
        template: '<div class="row form-group" ng-form="innerForm">'
                    + '<div class="{{columns1}}">{{title}}</div>'
                    + '<div class="{{columns2}}">'
                    + '<input type="text"  class="form-control" ng-model="bindModel" disabled/>'
                    + '</div>'
                    + '</div>',
        scope: {
            bindModel: '=ngModel'
        },
        link: function (scope, element, attrs) {
            var cols = [];
            if (attrs["cols"]) {
                cols = attrs["cols"].split(',');
            }
            scope.columns1 = isNaN(cols[0]) ? "col-md-1" : "col-md-" + cols[0];
            scope.columns2 = isNaN(cols[1]) ? "col-md-1" : "col-md-" + cols[1];
            scope.columns3 = isNaN(cols[2]) ? "col-md-1" : "col-md-" + cols[2];
            scope.title = attrs["title"];
        }
    };
})
.directive('snaCheckbox', function () {
    return {
        restrict: 'E',
        //templateUrl: 'snaInput.html',
        template: '<div class="row form-group" ng-form="innerForm">'
                    + '<div class="{{columns1}}">{{title}}</div>'
                    + '<div class="{{columns2}}">'
                    + '<input type="checkbox" class="form-control" ng-model="bindModel"/>'
                    + '</div>'
                    + '</div>',
        scope: {
            bindModel: '=ngModel'
        },
        link: function (scope, element, attrs) {
            var cols = [];
            if (attrs["cols"]) {
                cols = attrs["cols"].split(',');
            }
            scope.columns1 = isNaN(cols[0]) ? "col-md-1" : "col-md-" + cols[0];
            scope.columns2 = isNaN(cols[1]) ? "col-md-1" : "col-md-" + cols[1];
            scope.columns3 = isNaN(cols[2]) ? "col-md-1" : "col-md-" + cols[2];
            scope.title = attrs["title"];
        }
    };
})
.directive('snaDatepicker', function ($filter) {
    return {
        restrict: 'E',
        template: '<div class="row form-group" ng-form="innerForm">'
                    + '<div class="{{columns1}}">{{title}}</div>'
                    + '<div class="{{columns2}}">'
                        + '<div class="input-group">'
                            + '<input type="text" class="form-control" name="fieldname" ng-model="bindModel" datepicker-options="dateOptions" datepicker-popup="{{format}}" datepicker-mode="' + "'" + '{{mode}}' + "'" + '" max-date="' + "'" + '{{max}}' + "'" + '" min-date="' + "'" + '{{min}}' + "'" + '" '
                            + 'ng-required="{{required}}" close-text="Close" is-open="isOpen" datepicker-append-to-body="false" ng-disabled="true" show-weeks="false"/>'
                            + '<span class="input-group-btn">'
                                + '<button type="button" class="btn btn-default" ng-click="open($event)" style="height: 34px;">'
                                + '<i class="glyphicon glyphicon-calendar"></i>'
                                + '</button>'
                            + '</span>'
                        + '</div>'
                    + '</div>'
                    + '<div class="{{columns3}}" ng-show="innerForm.fieldname.$error.required">required</div>'
                    + '</div>',
        scope: {
            bindModel: '=ngModel',
            min: '@',
            max: '@',
            mode: '@',
            required: '@ngRequired',
            title: '@'
            //appendBody: '@'
        },
        link: function (scope, element, attrs) {
            var cols = [];
            if (attrs["cols"]) {
                cols = attrs["cols"].split(',');
            }
            scope.columns1 = isNaN(cols[0]) ? "col-md-1" : "col-md-" + cols[0];
            scope.columns2 = isNaN(cols[1]) ? "col-md-1" : "col-md-" + cols[1];
            scope.columns3 = isNaN(cols[2]) ? "col-md-1" : "col-md-" + cols[2];
         //   scope.
            scope.isOpen = false;
            scope.open = function (evt) {
                evt.preventDefault();
                evt.stopPropagation();
                scope.isOpen = !scope.isOpen;
            };
            scope.formats = {
                day_mode: 'M/d/yyyy',
                month_mode: 'M/yyyy',
                year_mode: 'yyyy'
            }
            scope.mode = scope.mode ? scope.mode : 'day';
            scope.format = scope.formats.day_mode;
            scope.dateOptions = {
                minMode: 'day',
                maxMode: 'year'
            };
            if (scope.mode == 'month') {
                scope.format = scope.formats.month_mode;
                scope.dateOptions.minMode = 'month';
                scope.dateOptions.maxMode = 'month';

            } else if (scope.mode == 'year') {
                scope.format = scope.formats.year_mode;
                scope.dateOptions.minMode = 'year';
                scope.dateOptions.maxMode = 'year';
            }
        }
    };
})
.directive('snaSelect', function () {
    return {
        replace: true,
        restrict: 'E',
        //templateUrl: '/Public/app/snaAngular/snaSelect.html',
        template: function (element, attrs) {
            var cols = [];
            if (attrs["cols"]) {
                cols = attrs["cols"].split(',');
            }
            var columns1 = isNaN(cols[0]) ? "col-md-1" : "col-md-" + cols[0];
            var columns2 = isNaN(cols[1]) ? "col-md-1" : "col-md-" + cols[1];
            var columns3 = isNaN(cols[2]) ? "col-md-1" : "col-md-" + cols[2];
            var isRequired = attrs['ngRequired'] == 'true';
            var required = isRequired ? "required ui-validate=" + '"' + "'$value != null && $value != 0'" + '"' : '';
            var nullOption = !isRequired ? '<option></option>' : '';

            var s = '<div class="row form-group" ng-form="innerFormName">'
                    + '<div class="{{col-md-1}}">title</div>'
                    + '<div class="{{col-md-2}}">'
                    + '<select type="text" class="form-control" name="fieldname" ng-model="ngModel" required ng-options="optexp">nullOption</select>'
                    + '</div>'
                    + '<div class="{{col-md-3}}" ng-show="innerFormName.fieldname.$invalid">required</div>'
                    + '</div>';

            var innerFormName = ('innerForm_' + attrs['ngModel']).replace('.', '_');
            s = s.replace("innerFormName", innerFormName);
            s = s.replace("innerFormName", innerFormName);
            s = s.replace("title", attrs["title"]);
            s = s.replace("{{col-md-1}}", columns1);
            s = s.replace("{{col-md-2}}", columns2);
            s = s.replace("{{col-md-3}}", columns3);
            s = s.replace("ngModel", attrs['ngModel']);
            s = s.replace("required", required);
            s = s.replace("optexp", attrs['optexp']);
            s = s.replace("nullOption", nullOption);

            return s;
        },
        scope: false,
        link: function (scope, element, attrs) {

        }
    };
});
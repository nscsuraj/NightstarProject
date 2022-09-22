/**
 * snaPivot - v1.0.0 - An AngularJS directive for data pivoting
 */

angular
  .module('snaPivot', ['ui.bootstrap'])
  .directive('snaPivot', ['$document', function ($document) {
      return {
          restrict: 'A',
          scope: {
              pivotDataSource: '=',
              pivotConfig: '=',
              pivotUpdating: '&',
              pivotUpdated: '&'
          },
          template: '<label ng-bind="DataFetchMsg" class="snapivot-msg"></label>' +
                    '<div class="pull-right" style="padding-bottom:10px;" ng-show="hasData">' +
                        '<a download="pivot-data.csv" href="javascript:void(0)" onclick="return ExcellentExport.csv(this, ' + "'pivot-table'" + ')"; style="margin-right:10px;">' +
                            'Export to CSV' +
                        '</a>' +
                    '</div>' +
                    '<div ng-show="hasData">' +
                        '<div class="subnav" style="background-color:#eeeeee;" ng-show="showBar">' +
                            '<ul class="nav nav-pills">' +
                                '<ul class="btn-group" dropdown>' +
                                    '<button type="button" class="btn btn-primary dropdown-toggle" ng-disabled="disabled">' +
                                    'Main Filter<span class="caret"></span>' +
                                    '</button>' +
                                    '<ul class="dropdown-menu stop-propagation" role="menu" id="filter-list" style="overflow:auto;max-height:450px;padding:10px;">' +
                                        '<li><label class="checkbox"><input type="checkbox" /> Hello</label></li>' +
                                    '</ul>' +
                                '</ul>' +
                                '<ul class="btn-group" dropdown>' +
                                    '<button type="button" class="btn btn-primary dropdown-toggle" ng-disabled="disabled">' +
                                    'Row <span class="caret"></span>' +
                                    '</button>' +
                                    '<ul class="dropdown-menu stop-propagation" role="menu" id="row-label-fields" style="overflow:auto;padding-left:10px;"></ul>' +
                                '</ul>' +
                                '<ul class="btn-group" dropdown>' +
                                    '<button type="button" class="btn btn-primary dropdown-toggle" ng-disabled="disabled">' +
                                    'Column <span class="caret"></span>' +
                                    '</button>' +
                                    '<ul class="dropdown-menu stop-propagation" role="menu" id="column-label-fields" style="overflow:auto;padding-left:10px;"></ul>' +
                                '</ul>' +
                                '<ul class="btn-group" dropdown>' +
                                    '<button type="button" class="btn btn-primary dropdown-toggle" ng-disabled="disabled">' +
                                    'Value <span class="caret"></span>' +
                                    '</button>' +
                                    '<ul class="dropdown-menu stop-propagation" role="menu" id="summary-fields" style="overflow:auto;padding-left:10px;"></ul>' +
                                '</ul>' +
                                '<ul class="btn-group pull-right" dropdown>' +
                                    '<button type="button" class="btn btn-primary dropdown-toggle" ng-disabled="disabled">' +
                                    'Fixed Reports <span class="caret"></span>' +
                                    '</button>' +
                                    '<ul class="dropdown-menu" role="menu" style="overflow:auto;padding-left:10px;" ng-show="pivotConfig.fixedReport && pivotConfig.fixedReport.length > 0">' +
                                        '<li ng-repeat="r in pivotConfig.fixedReport"><a href="javascript:void(0)" ng-click="clickFixedReport($index)">{{r.name}}</a></li>' +
                                    '</ul>' +
                                '</ul>' +
                            '</ul>' +
                        '</div>' +
                        '<br />' +
                        '<span id="pivot-detail" ng-show="showBar"></span>' +
                        '<div id="pivot-results"></div>' +
                    '</div>',
          compile: function (tElem, tAttrs) {
              return function (scope, element, attrs) {
                  var pivot = (function pivotFunc() {
                      /**
                    * @docauthor Jonathan Jackson
                    * @class Pivot
                    * # Welcome to Pivot.js
                    *
                    * Pivot.js is a simple way for you to get to your data.  It allows for the
                    * creation of highly customizable unique table views from your browser.
                    *
                    * > In data processing, a pivot table is a data summarization tool found in
                    * > data visualization programs such as spreadsheets or business intelligence
                    * > software. Among other functions, pivot-table tools can automatically sort,
                    * > count, total or give the average of the data stored in one table or
                    * > spreadsheet. It displays the results in a second table (called a "pivot
                    * > table") showing the summarized data.
                    *
                    * In our case, results (or the pivot-table) will be displayed as an HTML table
                    * pivoting from the input data (CSV or JSON). Without further ado let's get to usage.
                    *
                    * View an [example](http://rjackson.github.com/pivot.js/).
                    *
                    * #Usage
                    *
                    * Step one is to initialize the pivot object.  It expects the following attributes:
                    *
                    * - `csv` - which should contain a valid string of comma separated values.  It is
                    *   __important to note__ that you must include a header row in the CSV for pivot
                    *   to work properly  (you'll understand why in a minute).
                    *
                    * - `json` - which should contain a valid JSON string. At this time this string
                    *   must be an array of arrays, and not an array of objects (storing the field
                    *   names with each row consumes significantly more space).
                    *
                    * - `fields` - which should be an array of objects.  This is used to instruct
                    *   pivot on how to interact with the fields you pass in.  It keys off of the
                    *   header row names.  And is formated like so:
                    *
                    *     [ {name: 'header-name', type: 'string', optional_attributes: 'optional field' },
                    *     {name: 'header-name', type: 'string', optional_attributes: 'optional field' }]
                    *
                    *
                    * - `filters` (default is empty) - which should contain any filters you would like to restrict your data to.  A filter is defined as an object like so:
                    *
                    *     {zip_code: '34471'}
                    *
                    *
                    * Those are the options that you should consider.  There are other options that are well covered in the spec
                    * A valid pivot could then be set up from like so.
                    *
                    *
                    *     var field_definitions = [{name: 'last_name',   type: 'string',   filterable: true},
                    *             {name: 'first_name',        type: 'string',   filterable: true},
                    *             {name: 'zip_code',          type: 'integer',  filterable: true},
                    *             {name: 'pseudo_zip',        type: 'integer',  filterable: true },
                    *             {name: 'billed_amount',     type: 'float',    rowLabelable: false,},
                    *             {name: 'last_billed_date',  type: 'date',     filterable: true}
                    *
                    *     // from csv data:
                    *     var csv_string  =  "last_name,first_name,zip_code,billed_amount,last_billed_date\n" +
                    *                        "Jackson,Robert,34471,100.00,\"Tue, 24 Jan 2012 00:00:00 +0000\"\n" +
                    *                        "Jackson,Jonathan,39401,124.63,\"Fri, 17 Feb 2012 00:00:00 +0000\""
                    *     pivot.init({csv: csv_string, fields: field_definitions});
                    *
                    *     // from json data:
                    *     var json_string = '[["last_name","first_name","zip_code","billed_amount","last_billed_date"],' +
                    *                         ' ["Jackson", "Robert", 34471, 100.00, "Tue, 24 Jan 2012 00:00:00 +0000"],' +
                    *                         ' ["Smith", "Jon", 34471, 173.20, "Mon, 13 Feb 2012 00:00:00 +0000"]]'
                    *
                    *     pivot.init({json: json_string, fields: field_definitions});
                    *
                    */
                      'use strict';

                      var fields, filters, rawData, data, dataFilters, displayFields, results, resultsColumns;

                      init();
                      /**
                      * Initializes a new pivot.
                      * Optional parameters:
                      * * fields
                      * * filters
                      * * rowLabels
                      * * columnLabels
                      * * summaries
                      * @param {Object}
                      */
                      function init(options) {
                          rawData = [], data = [], dataFilters = {}, fields = {}, filters = {};
                          displayFields = { rowLabels: {}, columnLabels: {}, summaries: {} };

                          if (options === undefined) options = {};
                          if (options.fields !== undefined) setFields(options.fields);
                          if (options.filters !== undefined) setFilters(options.filters);
                          if (options.rowLabels !== undefined) setRowLabelDisplayFields(options.rowLabels);
                          if (options.columnLabels !== undefined) setColumnLabelDisplayFields(options.columnLabels);
                          if (options.summaries !== undefined) setSummaryDisplayFields(options.summaries);

                          if (options.csv !== undefined)
                              processCSV(options.csv)
                          if (options.json !== undefined)
                              processJSON(options.json)

                          return pivot;
                      }

                      /**
                      * Calls init with no options, which effectively resets the current pivot.
                      */
                      function reset() {
                          return init();
                      };

                      /**
                      * Very cool little function. If called like so: `pivot.config(true)` will return the exact object you would need
                      * to create the current pivot from scratch.  If passed with no argument will return everything except fields.
                      */
                      function config(showFields) {
                          var fields;
                          if (showFields === undefined)
                              fields = cloneFields()
                          else if (showFields === false)
                              fields = "Pass showFields as true in order to view fields here.";

                          return {
                              fields: fields,
                              filters: filters,
                              rowLabels: objectKeys(displayFields.rowLabels),
                              columnLabels: objectKeys(displayFields.columnLabels),
                              summaries: objectKeys(displayFields.summaries)
                          };
                      };
                      function pivotUtils() {
                          return {
                              pad: pad,
                              padRight: padRight,
                              padLeft: padLeft,
                              formatDate: formatDate,
                              formatTime: formatTime,
                              isArray: isArray,
                              isRegExp: isRegExp,
                              shallowClone: shallowClone,
                              objectKeys: objectKeys,
                              objectType: objectType,
                              sortNumerically: sortNumerically
                          }
                      };

                      function pad(sideToPad, input, width, padString) {
                          if (padString === undefined) padString = " ";

                          input = input.toString();
                          padString = padString.toString();

                          while (input.length < width) {
                              if (sideToPad === "left")
                                  input = padString + input;
                              else
                                  input = input + padString;
                          }

                          return input
                      };

                      function padRight(input, width, padString) {
                          return pad('right', input, width, padString)
                      };

                      function padLeft(input, width, padString) {
                          return pad('left', input, width, padString)
                      };

                      function formatDate(value) {
                          return value.getUTCFullYear() + '-' + padLeft((value.getUTCMonth() + 1), 2, "0") + '-' + padLeft(value.getUTCDate(), 2, '0');
                      };

                      function formatTime(value) {
                          return formatDate(value) + ' ' + padLeft(value.getUTCHours(), 2, '0') + ':' + padLeft(value.getUTCMinutes(), 2, '0');
                      };

                      function isArray(arg) {
                          if (!Array.isArray)
                              return objectType(arg) == 'array';
                          else
                              return Array.isArray(arg);
                      };

                      function isRegExp(arg) {
                          return objectType(arg) == 'regexp';
                      };

                      function shallowClone(input) {
                          var output = {};

                          for (var key in input) {
                              if (input.hasOwnProperty(key))
                                  output[key] = input[key];
                          }

                          return output;
                      };

                      function objectKeys(object) {
                          if (Object.keys) return Object.keys(object);

                          var output = [];

                          for (key in object) {
                              output.push(key);
                          }

                          return output;
                      };

                      function objectType(obj) {
                          return ({}).toString.call(obj).match(/\s([a-z|A-Z]+)/)[1].toLowerCase();
                      };

                      function sortNumerically(array) {
                          return array.sort(function (a, b) { return a - b; });
                      };
                      function processHeaderRow(row) {
                          var output = [];

                          var o = {}, i = -1, m = row.length;
                          while (++i < m) {
                              var field = fields[row[i]];
                              if (field === undefined) field = appendField(row[i]);
                              output.push(field);
                          };

                          return output;
                      };

                      function processJSON(input) {
                          var header,
                              pseudoFields = restrictFields('pseudo');

                          if (objectType(input) === 'string') input = JSON.parse(input);
                          rawData = [];

                          var o = {}, j = -1, m = input.length;
                          while (++j < m) {
                              if (j === 0)
                                  header = processHeaderRow(input[j]);
                              else
                                  rawData.push(processRow(input[j], header, pseudoFields));
                          };
                      };

                      // Accepts csv as a string
                      function processCSV(text) {
                          var header,
                              pseudoFields = restrictFields('pseudo');

                          rawData = processRows(text, function (row, i) {
                              if (i === 0)
                                  header = processHeaderRow(row);
                              else
                                  return processRow(row, header, pseudoFields);
                          });
                      };

                      function processRows(text, f) {
                          var EOL = {}, // sentinel value for end-of-line
                              EOF = {}, // sentinel value for end-of-file
                              rows = [], // output rows
                              re = /\r\n|[,\r\n]/g, // field separator regex
                              n = 0, // the current line number
                              t, // the current token
                              eol; // is the current token followed by EOL?

                          re.lastIndex = 0; // work-around bug in FF 3.6

                          /** @private Returns the next token. */
                          function token() {
                              if (re.lastIndex >= text.length) return EOF; // special case: end of file
                              if (eol) { eol = false; return EOL; } // special case: end of line

                              // special case: quotes
                              var j = re.lastIndex;
                              if (text.charCodeAt(j) === 34) {
                                  var i = j;
                                  while (i++ < text.length) {
                                      if (text.charCodeAt(i) === 34) {
                                          if (text.charCodeAt(i + 1) !== 34) break;
                                          i++;
                                      }
                                  }
                                  re.lastIndex = i + 2;
                                  var c = text.charCodeAt(i + 1);
                                  if (c === 13) {
                                      eol = true;
                                      if (text.charCodeAt(i + 2) === 10) re.lastIndex++;
                                  } else if (c === 10) {
                                      eol = true;
                                  }
                                  return text.substring(j + 1, i).replace(/""/g, "\"");
                              }

                              // common case
                              var m = re.exec(text);
                              if (m) {
                                  eol = m[0].charCodeAt(0) !== 44;
                                  return text.substring(j, m.index);
                              }
                              re.lastIndex = text.length;
                              return text.substring(j);
                          }

                          while ((t = token()) !== EOF) {
                              var a = [];
                              while ((t !== EOL) && (t !== EOF)) {
                                  a.push(t);
                                  t = token();
                              }
                              if (f && !(a = f(a, n++))) continue;
                              rows.push(a);
                          }

                          return rows;
                      };

                      function processRow(row, header, pseudoFields) {
                          // process actual fields
                          var o = {}, j = -1, m = header.length;
                          while (++j < m) {
                              var value = castFieldValue(header[j].name, row[j]);
                              o[header[j].name] = value;
                              addFieldValue(header[j].name, value);
                          };

                          // process pseudo fields
                          j = -1, m = pseudoFields.length;
                          while (++j < m) {
                              var field = pseudoFields[j],
                                  value = castFieldValue(field.name, field.pseudoFunction(o, field));
                              o[field.name] = value;
                              addFieldValue(field.name, value);
                          };

                          return o;
                      };
                      /**
                      * Entry point for several filter methods.
                      * See:
                      *
                      * * getFilters() - returns filters applied to current pivot
                      * * setFilters() - sets a series of filters
                      * * appendFilter() - adds a filter to current pivot filters
                      * * applyFilter() - runs the filters on the values
                      *
                      * @param {String}
                      * @return {function} One of the fucntions defined above.
                      */
                      function pivotFilters(type) {
                          var opts = {
                              all: getFilters,
                              set: setFilters,
                              apply: applyFilter,
                              add: appendFilter
                          }

                          if (type !== undefined) {
                              return opts[type]
                          } else {
                              return opts
                          };
                      };

                      function castFilterValues(restrictions) {
                          if (restrictions === undefined) restrictions = filters;

                          var field;
                          for (field in restrictions) {
                              if (restrictions.hasOwnProperty(field))
                                  if (isRegExp(restrictions[field])) {
                                      // no need to change
                                  } else if (isArray(restrictions[field])) {
                                      var i = -1, m = restrictions[field].length;
                                      while (++i < m) {
                                          restrictions[field][i] = castFieldValue(field, restrictions[field][i])
                                      };
                                  } else {
                                      restrictions[field] = castFieldValue(field, restrictions[field])
                                  }
                          };
                      };

                      /**
                      * Takes a new restrction (filter) and appends it to current pivot's filters
                      * @param {Object} newRestriction should looke like {"last_name":"Jackson"}
                      */
                      function appendFilter(newRestriction) {
                          for (var key in newRestriction) {
                              if (newRestriction.hasOwnProperty(key))
                                  filters[key] = newRestriction[key];
                          }

                          castFilterValues();
                      };

                      /**
                      * Returns current pivot's filters
                      */
                      function getFilters() {
                          return filters;
                      };

                      /**
                      * Accepts list of restrictions, assigns them  as current pivot's filters and casts their values.
                      * @param {Object} restrictions - should looke something like {"employer":"Acme Corp"}
                      */
                      function setFilters(restrictions) {
                          filters = restrictions;
                          resetResults();
                          castFilterValues();
                      };

                      /**
                      * Applies the current pivot's filters to the data returning a list of values
                      * Optionally allows you to set filters and apply them.
                      * @param {Object} restrictions allows you to pass the filters to apply without using set first.
                      */
                      function applyFilter(restrictions) {
                          var dataToFilter = data,
                              filteredData = [];

                          if (restrictions !== undefined) setFilters(restrictions);

                          var preserveFilter = preserveFilteredData();

                          if (preserveFilter) {
                              dataToFilter = data;
                          } else {
                              dataToFilter = rawData;
                          }

                          var dataToFilterLength = dataToFilter.length,
                              filterLength = objectKeys(filters).length,
                              i = -1;

                          while (++i < dataToFilterLength) {
                              var row = dataToFilter[i],
                                  matches = 0;

                              for (var key in filters) {
                                  if (filters.hasOwnProperty(key) && row.hasOwnProperty(key) && matchesFilter(filters[key], row[key]))
                                      matches += 1;
                              }

                              if (matches === filterLength) {
                                  filteredData.push(row);
                              };
                          };

                          data = filteredData;
                          dataFilters = shallowClone(filters);
                          resetResults();

                          return data;
                      };

                      function matchesFilter(filter, value) {
                          if (isArray(filter)) {
                              var i = -1, m = filter.length;
                              while (++i < m) {
                                  if (filter[i] === value) return true
                              };
                          } else if (isRegExp(filter)) {
                              return filter.test(value);
                          } else {
                              return value === filter;
                          }

                          return false
                      };

                      function preserveFilteredData() {
                          var matches = 0,
                              dataFiltersLength = objectKeys(dataFilters).length;

                          for (var key in dataFilters) {
                              if (dataFilters.hasOwnProperty(key) && dataFilters.hasOwnProperty(key) && filters[key] === dataFilters[key])
                                  matches += 1;
                          }

                          return dataFiltersLength > 0 && matches >= dataFiltersLength;
                      };
                      /**
                      * Entry point for several field methods.
                      * See:
                      *
                      * * restrictFields()
                      * * cloneFields()
                      * * appendField()
                      * * getFields()
                      * * getField()
                      * * setField()
                      *
                      * @param {String}
                      * @return {function} One of the fucntions defined above.
                      */
                      function pivotFields(type) {
                          var opts = {
                              columnLabelable: restrictFields('columnLabelable'),
                              rowLabelable: restrictFields('rowLabelable'),
                              summarizable: restrictFields('summarizable'),
                              filterable: restrictFields('filterable'),
                              pseudo: restrictFields('pseudo'),
                              clone: cloneFields,
                              add: appendField,
                              all: getFields,
                              set: setFields,
                              get: getField
                          }

                          if (type !== undefined) {
                              return opts[type]
                          } else {
                              return opts
                          };
                      };
                      /**
                      * Method for setting multiple fields.  Usually used on pivot.init().
                      * See {@link pivot#appendField} for more information.
                      * @param {Object}
                      * @return {undefined}
                      */
                      function setFields(listing) {
                          fields = {};
                          var i = -1, m = listing.length;
                          while (++i < m) {
                              appendField(listing[i]);
                          }
                      };

                      function cloneFields() {
                          var fieldsOutput = [];
                          for (var field in fields) {
                              var newField = {};
                              for (var key in fields[field]) {
                                  if (fields[field].hasOwnProperty(key) && key !== 'values')
                                      newField[key] = fields[field][key];
                              }
                              fieldsOutput.push(newField);
                          }

                          return fieldsOutput;
                      }

                      /**
                      * Returns array of defined field objects.
                      */
                      function getFields() {
                          var retFields = [];
                          for (var key in fields) {
                              if (fields.hasOwnProperty(key)) retFields[fields[key].index] = fields[key];
                          }

                          return retFields;
                      };

                      /**
                      * Returns list of defined fields filtered by type
                      * @param {String} 'columnLabelable', 'rowLabelable', 'summarizable', 'filterable', or 'pseudo'
                      */
                      function restrictFields(type) {
                          var retFields = [];
                          for (var key in fields) {
                              if (fields.hasOwnProperty(key) && fields[key][type] === true) retFields.push(fields[key]);
                          }

                          return retFields;
                      };

                      /**
                      * Attr reader for fields
                      * @param {String} Something like 'last_name'
                      */
                      function getField(name) {
                          return fields[name];
                      };

                      /**
                      * Returns the sum value of all rows passed to it.
                      */
                      function defaultSummarizeFunctionSum(rows, field) {
                          var runningTotal = 0,
                              i = -1,
                              m = rows.length;
                          while (++i < m) {
                              runningTotal += rows[i][field.dataSource];
                          };
                          return runningTotal;
                      };

                      /**
                      * Returns Average of values passed in from rows
                      */
                      function defaultSummarizeFunctionAvg(rows, field) {
                          return defaultSummarizeFunctionSum(rows, field) / rows.length;
                      };

                      /**
                      * Returns count of rows
                      */
                      function defaultSummarizeFunctionCount(rows, field) {
                          return rows.length;
                      }

                      /**
                      * The main engine by which you create and assign field.  Takes an object that should look something like {name: 'last_name',type: 'string', filterable: true}, and assigns all the associated attributes to their correct state.
                      * Allowed field attributes are
                      * * filterable - Allows you to filter based off this field
                      * * rowLabelable - Allows you to display rowLabels based off this field
                      * * columnLabelable - Allows you to display columnLabels based off this field
                      * * summarizable - Allows you to create a summary field.
                      * * pseudo - Allows you to treat an anonymous function as a field (ie you could treat the sum of a set of values as a field)
                      * * sortFunction - Allows you to override the default sort function for columnLabelable fields.
                      * * displayFunction - Allows you to override the default display function. Using this function you can completely customize the way a field is displayed without having to modify the internal storage.
                      * Be sure to run through the source on this one if you are unsure as to what it does.  It's pretty straightforward, but definitely bears looking into.
                      * @param {Object} field
                      */
                      function appendField(field) {
                          // if field is a simple string setup and object with that string as a name
                          if (objectType(field) === 'string') field = { name: field };

                          if (field.type === undefined) field.type = 'string';
                          if (field.pseudo === undefined) field.pseudo = false;
                          if (field.rowLabelable === undefined) field.rowLabelable = true;
                          if (field.columnLabelable === undefined) field.columnLabelable = false;
                          if (field.filterable === undefined) field.filterable = false;
                          if (field.dataSource === undefined) field.dataSource = field.name;

                          if (field.summarizable && (field.rowLabelable || field.columnLabelable || field.filterable)) {
                              var summarizable_field = shallowClone(field);
                              summarizable_field.rowLabelable = false;
                              summarizable_field.filterable = false;
                              summarizable_field.dataSource = field.name;

                              if (summarizable_field.summarizable !== true)
                                  summarizable_field.name = summarizable_field.name + '_' + summarizable_field.summarizable;
                              else
                                  summarizable_field.name = summarizable_field.name + '_count'

                              appendField(summarizable_field);

                              field.summarizable = false;
                              field.summarizeFunction = undefined;
                          } else if (field.summarizable) {
                              if (field.summarizeFunction === undefined) {
                                  switch (field.summarizable) {
                                      case 'sum':
                                          field.summarizeFunction = defaultSummarizeFunctionSum;
                                          break;
                                      case 'avg':
                                          field.summarizeFunction = defaultSummarizeFunctionAvg;
                                          break;
                                      default:
                                          field.summarizeFunction = defaultSummarizeFunctionCount;
                                          break;
                                  };

                                  field.summarizable = true;
                              };
                          } else {
                              field.summarizable = false
                          }

                          if (field.pseudo && field.pseudoFunction === undefined)
                              field.pseudoFunction = function (row) { return '' };

                          if (field.displayFunction === undefined)
                              field.displayFunction = displayFieldValue;

                          field.values = {};
                          field.displayValues = {};

                          field.index = objectKeys(fields).length;
                          fields[field.name] = field;

                          return field;
                      };

                      /**
                      * Adds value to field based off of the Fields' displayFunction, defaults to count.
                      */
                      function addFieldValue(field, value) {
                          if (fields[field] === undefined || fields[field].filterable === false) return;

                          if (fields[field].values[value] === undefined) {
                              fields[field].values[value] = { count: 1, displayValue: fields[field].displayFunction(value, field) };
                          } else {
                              fields[field].values[value].count += 1;
                          }
                      };

                      /**
                      * Helper for displaying properly formated field values.
                      */
                      function displayFieldValue(value, fieldName) {
                          var field;
                          if (objectType(fieldName) === 'string') field = fields[fieldName];
                          if (field === undefined) field = appendField(fieldName);

                          switch (field.type) {
                              case "cents":
                                  return '$' + (value / 100).toFixed(2);
                              case "currency":
                                  return '$' + value.toFixed(2);
                              case "date":
                                  return formatDate(new Date(value));
                              case "time":
                                  return formatTime(new Date(value));
                              default:
                                  return value;
                          }
                      }

                      /**
                      * Used to change the string value as parsed from the CSV into the type of field it expects.
                      */
                      function castFieldValue(fieldName, value) {
                          var field, retValue;
                          if (objectType(fieldName) === 'string') field = fields[fieldName];
                          if (field === undefined) field = appendField(fieldName);

                          switch (field.type) {
                              case "integer":
                              case "cents":
                                  if (objectType(value) === 'number')
                                      return value;
                                  else
                                      return parseInt(value, 10);
                              case "float":
                              case "currency":
                                  if (objectType(value) === 'number')
                                      return value;
                                  else
                                      return parseFloat(value, 10);
                              case "date":
                              case "time":
                                  switch (objectType(value)) {
                                      case 'number':
                                      case 'date':
                                          return value;
                                      default:
                                          if (/^\d+$/.test(value))
                                              return parseInt(value);
                                          else
                                              return Date.parse(value);
                                  };
                              default:
                                  return value === undefined || value === null ? "" : value.toString();
                          }
                      };
                      /**
                       * Returns object containing the raw fields(rawData) and filtered fields(data).
                       * @param  string, either 'raw', or 'all'.
                       * @return {Object} An object containing lists of fields
                       */
                      function pivotData(type) {
                          var opts = {
                              raw: rawData,
                              all: data
                          };

                          if (type !== undefined) {
                              return opts[type]
                          } else {
                              return opts
                          };
                      }
                      /**
                      * Entry point for several display methods.  See {@link pivot#pivotDisplayAll}, {@link pivot#pivotDisplayRowLabels}, {@link  pivot#pivotDisplaycolumnLabels}, and {@link pivot#pivotDisplaySummaries}
                      * @return {function} One of the fucntions defined above.
                      */
                      function pivotDisplay() {
                          return {
                              all: pivotDisplayAll,
                              rowLabels: pivotDisplayRowLabels,
                              columnLabels: pivotDisplayColumnLabels,
                              summaries: pivotDisplaySummaries
                          }
                      };

                      /**
                      * This will return an object containing rowLabels, summaries, and columnLabels that are currently applied to the pivot.
                      */
                      function pivotDisplayAll() {
                          return displayFields;
                      };

                      /**
                      * Returns either list of rowLabels or allows you to access the {@link pivot#setRowLabelDisplayFields}.
                      *
                      * Called from pivot like so: pivot.display().rowLabels().set() or pivot.display().rowLabels().get
                      */
                      function pivotDisplayRowLabels() {
                          return {
                              set: setRowLabelDisplayFields,
                              get: displayFields.rowLabels
                          }
                      };

                      /**
                      * Returns either list of columnLabels or allows you to access the {@link pivot#setColumnLabelDisplayFields}.
                      *
                      * Called from pivot like so: pivot.display().columnLabels().set() or pivot.display().columnLabels().get
                      */
                      function pivotDisplayColumnLabels() {
                          return {
                              set: setColumnLabelDisplayFields,
                              get: displayFields.columnLabels
                          }
                      };

                      /**
                      * Returns either list of summaries (labels) or allows you to access the {@link pivot#setSummaryDisplayFields}.
                      *
                      * Called from pivot like so: pivot.display().summaries().set() or pivot.display().summaries().get
                      */
                      function pivotDisplaySummaries() {
                          return {
                              set: setSummaryDisplayFields,
                              get: displayFields.summaries
                          }
                      };

                      /**
                      * This method allows you to append a new label field to the specified type. For example, you could set a new displayRowLabel by sending it as the type and 'city' as the field
                      * @param string type - must be either 'rowLabels', 'columnLabels', or 'summaries'
                      * @param string field - Specify the label you would like to add.
                      * @private
                      * @return {undefined}
                      */
                      function appendDisplayField(type, field) {
                          if (objectType(field) === 'string')
                              field = fields[field];

                          resetResults();
                          displayFields[type][field.name] = field;
                      };

                      /**
                      * This method simply calls appendDisplayField on a collection passing in each to appendDisplayField.  The object should look something like the following
                      *    {'rowLabels':['city','state'],'columnLabels':['billed_amount']}
                      * @private
                      * @return {undefined}
                      */
                      function setDisplayFields(type, listing) {
                          displayFields[type] = {};
                          resetResults();

                          var i = -1, m = listing.length;
                          while (++i < m) {
                              appendDisplayField(type, listing[i]);
                          };
                      };

                      /**
                      * Allows setting of row label fields
                      * @param listing Should look like ['city','state']
                      * @return {undefined}
                      */
                      function setRowLabelDisplayFields(listing) {
                          setDisplayFields('rowLabels', listing);
                      };

                      /**
                      * Allows setting of column label fields
                      * @param listing - Should look like ['city','state']
                      * @return {undefined}
                      */
                      function setColumnLabelDisplayFields(listing) {
                          setDisplayFields('columnLabels', listing);
                      };

                      /**
                      * Allows setting of summary label fields
                      * @param listing - Should look like ['billed_amount']
                      * @return {undefined}
                      */
                      function setSummaryDisplayFields(listing) {
                          setDisplayFields('summaries', listing);
                      };
                      /**
                      * Entry point for several results methods.
                      * See:
                      *
                      * * getDataResults() - returns filters applied to current pivot
                      * * getColumnResults() - sets a series of filters
                      *
                      * @return {function} One of the fucntions defined above.
                      */
                      function pivotResults() {
                          return {
                              all: getFormattedResults,
                              columns: getColumnResults
                          }
                      };

                      function resetResults() {
                          results = undefined; resultsColumns = undefined;
                      }

                      function getFormattedResults() {
                          if (results !== undefined && resultsColumns !== undefined) return getResultArray();

                          applyFilter();
                          results = {}; resultsColumns = [];

                          processRowLabelResults();

                          if (objectKeys(displayFields.columnLabels).length > 0)
                              processColumnLabelResults();
                          else {
                              populateSummaryColumnsResults();
                              processSummaryResults();
                          }

                          return getResultArray();
                      };

                      function processRowLabelResults() {
                          var i = -1, m = data.length, keys;

                          while (++i < m) {
                              var row = data[i],
                                  resultKey = '';

                              for (var key in displayFields.rowLabels) {
                                  if (displayFields.rowLabels.hasOwnProperty(key)) {
                                      if (i === 0) resultsColumns.push({ fieldName: key, width: 1, type: 'row' });

                                      resultKey += key + ':' + row[key] + '|';
                                  }
                              }
                              if (results[resultKey] === undefined) {
                                  results[resultKey] = {};

                                  for (var key in displayFields.rowLabels) {
                                      if (displayFields.rowLabels.hasOwnProperty(key))
                                          results[resultKey][key] = fields[key].displayFunction(row[key], key);
                                  }

                                  results[resultKey].rows = [];
                              };

                              results[resultKey].rows.push(row);
                          };
                      };

                      function processColumnLabelResults() {
                          for (var key in displayFields.columnLabels) {
                              if (displayFields.columnLabels.hasOwnProperty(key)) {
                                  var columnLabelColumns = {};
                                  for (var resultKey in results) {
                                      var values = pluckValues(results[resultKey].rows, fields[key]);

                                      for (var value in values) {
                                          if (columnLabelColumns[value] === undefined)
                                              columnLabelColumns[value] = 1;
                                          else
                                              columnLabelColumns[value] += 1;

                                          results[resultKey][value] = getSummaryResults(values[value]);
                                      };
                                  }

                                  populateColumnLabelColumnsResults(key, columnLabelColumns);
                              };
                          };

                          return results;
                      };

                      function pluckValues(rows, field) {
                          var i = -1, m = rows.length, output = {};
                          while (++i < m) {
                              var value = rows[i][field.name];
                              if (output[value] === undefined) output[value] = { rows: [] }

                              output[value].rows.push(rows[i]);
                          };
                          return output;
                      }

                      function processSummaryResults() {
                          for (var resultKey in results) {
                              getSummaryResults(results[resultKey])
                          };

                          return results;
                      };

                      function getSummaryResults(result) {
                          var output = {};
                          for (var key in displayFields.summaries) {
                              if (displayFields.summaries.hasOwnProperty(key)) {
                                  result[key] = fields[key].summarizeFunction(result.rows, fields[key]);
                                  result[key] = fields[key].displayFunction(result[key], key);
                              }
                          };

                          return result;
                      };

                      function getResultArray() {
                          var output = [], keys = objectKeys(results).sort(),
                              i = -1, m = keys.length;

                          while (++i < m) {
                              output.push(results[keys[i]])
                          };


                          return output;
                      };

                      function getColumnResults() {
                          if (results === undefined || resultsColumns === undefined)
                              getFormattedResults();

                          return resultsColumns;
                      }

                      function populateSummaryColumnsResults() {
                          for (var key in displayFields.summaries) {
                              if (displayFields.summaries.hasOwnProperty(key))
                                  resultsColumns.push({ fieldName: key, width: 1, type: 'summary' })
                          }

                          return resultsColumns;
                      };

                      function populateColumnLabelColumnsResults(key, columnLabels) {
                          //var keys  = objectKeys(columnLabels).sort(fields[key].sortFunction),  /// comment by Luke to work on IE8
                          var keys = objectKeys(columnLabels).sort(),
                            i = -1,
                            m = keys.length,
                            w = objectKeys(displayFields.summaries).length;

                          while (++i < m) {
                              resultsColumns.push({ fieldName: keys[i], width: w, type: 'column' })
                          };


                          return resultsColumns;
                      }
                      // Entry Point
                      return {
                          init: init,
                          reset: reset,
                          config: config,
                          utils: pivotUtils,
                          csv: processCSV,
                          json: processJSON,
                          data: pivotData,
                          results: pivotResults,
                          fields: pivotFields,
                          filters: pivotFilters,
                          display: pivotDisplay
                      }
                  })();
                  var customDisplay = (function () {
                      'use strict';

                      var element,
                          callbacks = {},
                          resultsTitle,
                          resultsDivID;

                      var methods = {
                          setup: function (options) {
                              element = this; // set element for build_containers()
                              if (options.callbacks) callbacks = options.callbacks;

                              if (options.resultsDivID) {

                                  resultsDivID = options.resultsDivID;

                              }
                              else {
                                  resultsDivID = 'results';
                              }

                              if (options.url !== undefined)
                                  methods.process_from_url(options);
                              else
                                  methods.process(options);
                          },
                          process: function (options) {
                              if (callbacks.beforePopulate) {
                                  callbacks.beforePopulate();
                              };

                              var self = methods;

                              pivot.init(options);

                              resultsTitle = options.resultsTitle;

                              if (options.skipBuildContainers === undefined || options.skipBuildContainers === false) self.build_containers();

                              self.populate_containers();

                              $(document).on('change', '.row-labelable', function (event) {
                                  self.update_label_fields('row');
                              });

                              $(document).on('change', '.column-labelable', function (event) {
                                  self.update_label_fields('column');
                              });

                              $(document).on('change', '.summary', function (event) {
                                  self.update_summary_fields();
                              });

                              methods.update_results();

                              if (callbacks.afterPopulate) {
                                  callbacks.afterPopulate();
                              };
                          },
                          process_from_url: function (options) {
                              var re = /\.json$/i,
                                  dataType;

                              if (re.test(options.url))
                                  dataType = 'text/json'
                              else
                                  dataType = 'text/csv'

                              $.ajax({
                                  url: options.url,
                                  dataType: "text",
                                  accepts: "text/csv",
                                  success: function (data, status) {
                                      if (dataType === 'text/json')
                                          options['json'] = data
                                      else
                                          options['csv'] = data

                                      methods.process(options)
                                  }
                              });
                          },
                          populate_containers: function () {
                              methods.build_toggle_fields('#row-label-fields', pivot.fields().rowLabelable, 'row-labelable');
                              methods.build_toggle_fields('#column-label-fields', pivot.fields().columnLabelable, 'column-labelable');
                              methods.build_toggle_fields('#summary-fields', pivot.fields().summarizable, 'summary');
                              methods.build_filter_list();
                          },
                          reprocess_display: function (options) {
                              if (options.rowLabels === undefined) options.rowLabels = [];
                              if (options.columnLabels === undefined) options.columnLabels = [];
                              if (options.summaries === undefined) options.summaries = [];
                              if (options.filters === undefined) options.filters = {};
                              if (options.callbacks === undefined) options.callbacks = {};

                              if (options.callbacks.beforeReprocessDisplay) {
                                  options.callbacks.afterReprocessDisplay();
                              }

                              pivot.filters().set(options.filters);
                              pivot.display().summaries().set(options.summaries);
                              pivot.display().rowLabels().set(options.rowLabels);
                              pivot.display().columnLabels().set(options.columnLabels);

                              methods.populate_containers();
                              methods.update_results();

                              if (options.callbacks.afterReprocessDisplay) {
                                  options.callbacks.afterReprocessDisplay();
                              }
                          },
                          build_containers: function () {

                              var containers = '<div class="pivot_header_fields">' +
                                               '  <div class="pivot_field">' +
                                               '  <span class="pivot_header2">Filter Fields</span>' +
                                               '   <div id="filter-list"></div>' +
                                               '  </div>' +
                                               '  <div class="pivot_field">' +
                                               '  <span class="pivot_header2">Row Label Fields</span>' +
                                               '   <div id="row-label-fields"></div>' +
                                               '  </div>' +
                                               '  <div class="pivot_field">' +
                                               '  <span class="pivot_header2">Column Label Fields</span>' +
                                               '   <div id="column-label-fields"></div>' +
                                               '  </div>' +
                                               '  <div class="pivot_field">' +
                                               '  <span class="pivot_header2">Summary Fields</span>' +
                                               '   <div id="summary-fields"></div>' +
                                               '  </div>' +
                                               '</div>';
                              $(element).append(containers);
                          },
                          // Filters
                          build_filter_list: function () {
                              var select = '<select id="select-constructor">'
                              select += '<option></option>'
                              $.each(pivot.fields().filterable, function (index, field) {
                                  select += '<option>' + field.name + '</option>';
                              })
                              select += '</select>'
                              $('#filter-list').empty().append(select);

                              // show pre-defined filters (from init)
                              $.each(pivot.filters().all(), function (fieldName, restriction) {
                                  methods.build_filter_field(fieldName, restriction);
                              });

                              // Bind build action to select-constructor explicitly
                              $('#select-constructor').change(function () {
                                  methods.build_filter_field($(this).val());
                              })
                          },
                          build_filter_field: function (fieldName, selectedValue) {
                              var snip,
                                  remove_filter,
                                  field = pivot.fields().get(fieldName);

                              if (fieldName === '') return;

                              // Check to see if this field has already been built
                              var filterExists = $('#filter-list select[data-field="' + field.name + '"]');
                              if (filterExists.length) return;

                              if (field.filterType === 'regexp')
                                  snip = methods.build_regexp_filter_field(field, selectedValue);
                              else
                                  snip = methods.build_select_filter_field(field, selectedValue);

                              remove_filter = '<a class="remove-filter-field" style="cursor:pointer;">(X)</a></label>';
                              $('#filter-list').append('<div><hr/><label>' + field.name + remove_filter + snip + '</div>');

                              //Optional Chosen/Select2 integration
                              if ($.fn.chosen !== undefined) $('select.filter').chosen();
                              else if ($.fn.select2 !== undefined) $('select.filter').select2();

                              // Update field listeners
                              $('select.filter').on('change', function (event) {
                                  methods.update_filtered_rows();
                              });

                              $('input[type=text].filter').on('keyup', function (event) {
                                  var filterInput = this,
                                      eventValue = $(filterInput).val();

                                  setTimeout(function () { if ($(filterInput).val() === eventValue) methods.update_filtered_rows() }, 500);
                              });

                              // remove_filter listener
                              $('.remove-filter-field').click(function () {
                                  $(this).parents('div').first().remove();
                                  methods.update_filtered_rows();
                              })
                          },
                          build_select_filter_field: function (field, selectedValue) {
                              var snip = '<select class="filter span3" ' + (field.filterType === 'multiselect' ? 'multiple' : '') + ' data-field="' + field.name + '">' +
                                          '<option></option>',
                                  orderedValues = [];

                              for (var value in field.values) {
                                  orderedValues.push(value);
                              };

                              orderedValues = orderedValues.sort();
                              jQuery.each(orderedValues, function (index, value) {
                                  var selected = "";
                                  if (value === selectedValue) selected = 'selected="selected"';
                                  snip += '<option value="' + value + '" ' + selected + '>' + field.values[value].displayValue + '</option>';
                              });
                              snip += '</select>'

                              return snip;
                          },
                          build_regexp_filter_field: function (field, value) {
                              if (value === undefined) value = "";
                              return '<input type="text" class="filter span3" data-field="' + field.name + '" value="' + value + '">';
                          },
                          update_filtered_rows: function () {
                              var restrictions = {}, field;

                              $('.filter').each(function (index) {
                                  field = pivot.fields().get($(this).attr('data-field'));

                                  if (field) {
                                      if ($(this).val() !== null && $(this).val()[0] !== '') {
                                          if (field.filterType === 'regexp')
                                              restrictions[$(this).attr('data-field')] = new RegExp($(this).val(), 'i');
                                          else
                                              restrictions[$(this).attr('data-field')] = $(this).val();
                                      }
                                  }
                              });
                              pivot.filters().set(restrictions);
                              methods.update_results();
                          },

                          //toggles

                          build_toggle_fields: function (div, fields, klass) {
                              $(div).empty();
                              $.each(fields, function (index, field) {
                                  $(div).append('<label class="checkbox">' +
                                                '<input type="checkbox" class="' + klass + '" ' +
                                                  'data-field="' + field.name + '" ' +
                                                '> ' + field.name +
                                                '</label>');
                              });

                              var displayFields;
                              if (klass === 'row-labelable')
                                  displayFields = pivot.display().rowLabels().get
                              else if (klass === 'column-labelable')
                                  displayFields = pivot.display().columnLabels().get
                              else
                                  displayFields = pivot.display().summaries().get

                              for (var fieldName in displayFields) {
                                  var elem = $(div + ' input[data-field="' + fieldName + '"]');
                                  elem.prop("checked", true);
                                  //methods.orderChecked(div, elem);
                              };

                              // order listener
                              /*
                            $(div + ' input').on("click", function(){
                              if (this.checked) {
                                methods.orderChecked(div, this);
                              } else {
                                var field = $(this).parent().detach()[0];
                                $(div).append( field );
                              };
                            });
                            */
                          },
                          orderChecked: function (parent, elem) {
                              var last_checked = $(parent + ' input:checked');     // last changed field (lcf)
                              var field = $(elem).parent().detach()[0]; // pluck item from div
                              var children = $(parent).children();

                              //subtract 1 because clicked field is already checked insert plucked item into div at index
                              if ((last_checked.length - 1) === 0)
                                  $(parent).prepend(field);
                              else if (children.length < last_checked.length)
                                  $(parent).append(field);
                              else
                                  $(children[last_checked.length - 1]).before(field);
                          },
                          update_result_details: function () {
                              var snip = '';
                              var filters = '';
                              $.each(pivot.filters().all(), function (k, v) {
                                  filters += '<em>' + k + '</em>' + " => " + v + " "
                              });

                              if ($('#pivot-detail').length !== 0)
                                  snip += '<b>Filters:</b> ' + filters + "<br/>"
                              $('#pivot-detail').html(snip);
                          },
                          update_results: function () {
                              if (callbacks && callbacks.beforeUpdateResults) {
                                  callbacks.beforeUpdateResults();
                              };

                              var results = pivot.results().all(),
                                  config = pivot.config(),
                                  columns = pivot.results().columns(),
                                  snip = '',
                                  fieldName;

                              var result_table = $('#' + resultsDivID),
                                  result_rows;
                              result_table.empty();

                              snip += '<table id="pivot-table" class="table table-striped table-condensed table-bordered fancyTable"><thead>';

                              // build columnLabel header row
                              if (config.columnLabels.length > 0 && config.summaries.length > 1) {
                                  var summarySnip = '', summaryRow = '';
                                  $.each(config.summaries, function (index, fieldName) {
                                      summarySnip += '<th>' + fieldName + '</th>';
                                  })

                                  snip += '<tr>'
                                  $.each(columns, function (index, column) {
                                      switch (column.type) {
                                          case 'row':
                                              snip += '<th rowspan="2">' + column.fieldName + '</th>';
                                              break;
                                          case 'column':
                                              snip += '<th colspan="' + column.width + '">' + column.fieldName + '</th>';
                                              summaryRow += summarySnip
                                              break;
                                      }
                                  });
                                  snip += '</tr><tr>' + summaryRow + '</tr>';
                              } else {
                                  snip += '<tr>'
                                  $.each(columns, function (index, column) {
                                      if (column.type !== 'column' || config.summaries.length <= 1) {
                                          snip += '<th>' + column.fieldName + '</th>';
                                      } else {
                                          $.each(config.summaries, function (index, fieldName) {
                                              snip += '<th>' + fieldName + '</th>';
                                          });
                                      }
                                  });
                                  snip += '</tr>'
                              }
                              snip += '</thead><tbody id="result-rows"></tbody></table>';
                              result_table.append(snip);

                              result_rows = $('#result-rows');

                              $.each(results, function (index, row) {
                                  snip = '<tr>';
                                  $.each(columns, function (index, column) {
                                      if (column.type !== 'column')
                                          snip += '<td>' + row[column.fieldName] + '</td>';
                                      else {
                                          $.each(config.summaries, function (index, fieldName) {
                                              if (row[column.fieldName] !== undefined)
                                                  snip += '<td>' + row[column.fieldName][fieldName] + '</td>';
                                              else
                                                  snip += '<td>&nbsp;</td>';
                                          });
                                      }
                                  });
                                  snip += '</tr>';

                                  result_rows.append(snip);
                              });
                              methods.update_result_details();

                              if (callbacks && callbacks.afterUpdateResults) {
                                  callbacks.afterUpdateResults();
                              };
                          },
                          update_label_fields: function (type) {
                              var display_fields = [];

                              $('.' + type + '-labelable:checked').each(function (index) {
                                  display_fields.push($(this).attr('data-field'));
                              });

                              pivot.display()[type + 'Labels']().set(display_fields);

                              methods.update_results();
                          },
                          update_summary_fields: function () {
                              var summary_fields = [];

                              $('.summary:checked').each(function (index) {
                                  summary_fields.push($(this).attr('data-field'));
                              });

                              pivot.display().summaries().set(summary_fields);

                              methods.update_results();
                          }
                      };

                      var pivot_display = function (method) {
                          if (methods[method]) {
                              return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
                          } else if (typeof method === 'object' || !method) {
                              return methods.init.apply(this, arguments);
                          } else {
                              $.error('Method ' + method + '  doesn\'t exists');
                          }
                      };
                      return {
                          pivot_display: pivot_display
                      }
                  })();
                  var getFieldArray = function (selectFields) {
                      var ret = [];
                      if (selectFields === null) {
                          return ret;
                      }
                      for (var property in selectFields) {
                          if (selectFields.hasOwnProperty(property)) {
                              ret.push(property);
                          }
                      }
                      return ret;
                  }
                  var getPivotArrays = function (rawData) {
                      var ret = [];
                      if (!rawData || !rawData.length || rawData.length === 0) {
                          return ret;
                      }
                      ret.push([]);
                      for (var prop in rawData[0]) {
                          if (rawData[0].hasOwnProperty(prop)) {
                              ret[0].push(prop);
                          }
                      }
                      for (var i = 0; i < rawData.length; ++i) {
                          ret.push([]);
                          for (var j = 0; j < ret[0].length; ++j) {
                              ret[i + 1].push(rawData[i][ret[0][j]]);
                          }
                      }
                      return ret;
                  }
                  scope.$watchCollection('[pivotDataSource,pivotConfig]', function (newCollection, oldCollection) {
                      var rowLabels = scope.pivotConfig && scope.pivotConfig.rowLabels ? scope.pivotConfig.rowLabels : [];
                      var columnLabels = scope.pivotConfig && scope.pivotConfig.columnLabels ? scope.pivotConfig.columnLabels : [];
                      var summaries = scope.pivotConfig && scope.pivotConfig.summaries ? scope.pivotConfig.summaries : [];
                      scope.showBar = (scope.pivotConfig && scope.pivotConfig.showBar === true) ? true : false;

                      if (!scope.pivotDataSource || !scope.pivotDataSource.data || !scope.pivotDataSource.fields) {
                          scope.hasData = false;
                          scope.DataFetchMsg = "";
                      }
                      else if (scope.pivotDataSource.data.length === 0) {
                          scope.hasData = false;
                          scope.DataFetchMsg = "This time period contains 0 data points";
                      } else {//if (newCollection[0] !== oldCollection[0]) {
                          customDisplay.pivot_display('setup', {
                              //csv: text,
                              json: getPivotArrays(scope.pivotDataSource.data),
                              fields: scope.pivotDataSource.fields,
                              resultsDivID: 'pivot-results',
                              skipBuildContainers: true,
                              rowLabels: rowLabels,
                              columnLabels: columnLabels,
                              summaries: summaries,
                              callbacks: {
                                  afterUpdateResults: function () {
                                      try {
                                          $.fn.dataTableExt.sErrMode = 'throw';
                                          $('#pivot-table').dataTable({
                                              //                                "aaSorting": [],
                                              'order': [],
                                              'paging': false,
                                              'info': false,
                                              'searching': false
                                          });
                                          //$('#pivot-results > table').dataTable();
                                      } catch (err) {
                                      }
                                  }
                              }
                          });
                          scope.hasData = true;
                          scope.DataFetchMsg = "This time period contains " + scope.pivotDataSource.data.length + " data points";
                      }
                      //} else {
                      //    customDisplay.pivot_display('reprocess_display', { rowLabels: rowLabels, columnLabels: columnLabels, summaries: summaries });
                      //}
                  });

                  scope.clickFixedReport = function (index) {
                      if (scope.pivotConfig && scope.pivotConfig.fixedReport && scope.pivotConfig.fixedReport.length > index) {
                          var rowLabels = scope.pivotConfig.fixedReport[index].rowLabels ? scope.pivotConfig.fixedReport[index].rowLabels : [];
                          var columnLabels = scope.pivotConfig.fixedReport[index].columnLabels ? scope.pivotConfig.fixedReport[index].columnLabels : [];
                          var summaries = scope.pivotConfig.fixedReport[index].summaries ? scope.pivotConfig.fixedReport[index].summaries : [];
                          customDisplay.pivot_display('reprocess_display', { rowLabels: rowLabels, columnLabels: columnLabels, summaries: summaries });
                      }
                  }

                  $('.stop-propagation').on('click', function (event) {
                      event.stopPropagation();
                  });
              }
          }
      }
  }]);
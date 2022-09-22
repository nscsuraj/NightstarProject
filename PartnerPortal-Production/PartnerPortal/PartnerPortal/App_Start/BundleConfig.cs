using System.Web;
using System.Web.Optimization;

namespace PartnerPortal
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                //"~/Scripts/Angular/angular-datepicker.js",
                //"~/Scripts/Angular/angular-gridview.js",
                //"~/Scripts/Angular/angular-jsonDate.js",
                //"~/Scripts/Angular/angular-locale_de-de.js",
                //"~/Scripts/Angular/angular-locale_en-us.js",
                //"~/Scripts/Angular/angular-mocks.js",
                //"~/Scripts/Angular/angular-resource.js",
                //"~/Scripts/Angular/angular-route.js",
                //"~/Scripts/Angular/angular-tooltips.js",
                "~/Scripts/Angular/angular.js",
                "~/Scripts/Angular/angular-ui.js",
                "~/Scripts/Angular/angular-ui-tree.js",
                "~/Scripts/Angular/sweet-alert.js",
                "~/Scripts/Angular/SweetAlert.js",
                "~/Scripts/Angular/lodash.min.js",
                "~/Scripts/Angular/angularjs-dropdown-multiselect.js",
                // "~/Scripts/Angular/angular-block-ui.js",
                "~/Scripts/ivh-treeview/ivh-treeview.js",
                "~/Scripts/Angular/dirPagination.js",
                "~/Scripts/acute.select/acute.select.js",
                "~/Scripts/json-export-excel.js",
                "~/Scripts/FileSaver.js",
                //"~/Scripts/Angular/loading-bar.js",
                //"~/Scripts/Angular/angular.min.js",
                //"~/Scripts/Angular/angular.min.js.map",
                //"~/Scripts/Angular/bootstrap-datetimepicker.js",
                //"~/Scripts/Angular/bootstrap.js",
                //"~/Scripts/Angular/functions.js",
                //"~/Scripts/Angular/jquery.blockUI.js",
                //"~/Scripts/Angular/jquery.fileupload-angular.js",
                //"~/Scripts/Angular/jquery.fileupload.js",
                //"~/Scripts/Angular/jquery.iframe-transport.js",
                //"~/Scripts/Angular/jquery.postmessage-transport.js",
                //"~/Scripts/Angular/jquery.tablesorter.js",
                //"~/Scripts/Angular/jquery.treetable.js",
                //"~/Scripts/Angular/jquery.ui.widget.js",
                //"~/Scripts/Angular/jquery.validate.js",
                //"~/Scripts/Angular/jquery.validate.unobtrusive.js",
                //"~/Scripts/Angular/jquery.xdr-transport.js",
                //"~/Scripts/Angular/json2.js",
                //"~/Scripts/Angular/moment.js",
                //"~/Scripts/Angular/respond.min.js",
                //"~/Scripts/Angular/snaAngular.js",
                //"~/Scripts/Angular/snaPivot.js",
                //"~/Scripts/Angular/textAngular-sanitize.js",
                //"~/Scripts/Angular/textAngular.js",
                //"~/Scripts/Angular/toastr.js",
                "~/Scripts/Angular/ui-bootstrap-tpls-0.10.0.js",
                "~/Scripts/Angular/ui-bootstrap-tpls.js",
                "~/Scripts/Angular/fileUploader.js",
                "~/Scripts/Angular/ui-utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                "~/Scripts/app/app.js",
                "~/Templates/CMS/SelectedParamModal.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                "~/Scripts/app/appMain.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/customjsmain").Include(
                "~/Scripts/Editor/ParsedHtmlSupportingFunctions.js",
                "~/Scripts/EditorElem.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/JQuery/jquery-1.10.2.js",
                "~/Scripts/JQuery/jquery-1.8.2.intellisense.js",
                "~/Scripts/JQuery/jquery-1.8.2.js",
                "~/Scripts/JQuery/jquery-1.8.2.min.js",
                "~/Scripts/JQuery/jquery-ui-1.8.24.js",
                "~/Scripts/JQuery/jquery-ui-1.8.24.min.js",
                "~/Scripts/JQuery/jquery.unobtrusive-ajax.js",
                "~/Scripts/JQuery/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/JQuery/jquery.validate-vsdoc.js",
                "~/Scripts/JQuery/jquery.validate.js",
                "~/Scripts/JQuery/jquery.validate.min.js",
                "~/Scripts/JQuery/jquery.validate.unobtrusive.js",
                "~/Scripts/JQuery/jquery.validate.unobtrusive.min.js",
                "~/Scripts/JQuery/jquery.blockUI.js",
                "~/Scripts/JQuery/jquery.easytabs.js",
                "~/Scripts/JQuery/jquery.json-viewer.js",
                "~/Scripts/JQuery/dateFormat.js",
                "~/Scripts/JQuery/jquery.dateFormat.js",
                //"~/Scripts/JQuery/jquery.dataTables.js",
                //"~/Scripts/JQuery/jquery.dataTables.rowGrouping.js",
                //"~/Scripts/JQuery/jquery.dataTables.rowReordering.js",
                //"~/Scripts/JQuery/knockout-2.2.0.debug.js",
                // Add Js file
                "~/Scripts/Site.js",
                "~/Scripts/enum-2.3.0.js"
                //"~/Scripts/mp_linkcode.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Styles/elementsize.css",
                "~/Styles/elementposition.css",
                "~/Styles/bootstrap.css",
                "~/Styles/angular-ui-tree.css",
               // "~/Styles/angular-block-ui.css",
                "~/Styles/treeapp.css",
                "~/Styles/sweet-alert.css",
                "~/Styles/modal.css",
                "~/Styles/jquery.json-viewer.css",
                "~/Styles/Editor/Symbols.css",
                "~/Styles/font-awesome.css",
                "~/Styles/AngularHideAnimate.css",
                "~/Scripts/acute.select/acute.select.css",
                "~/Styles/ResponsiveVideo.css"
                ));

            bundles.Add(new StyleBundle("~/Content/editorcmscss").Include(
                "~/Styles/Editor_style.css",
                "~/Styles/Editor2.css"
            ));


            bundles.Add(new StyleBundle("~/Content/Maincss").Include(
             "~/Styles/homeStyle.css"
             ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Scripts/ivh-treeview/ivh-treeview.css",
                "~/Scripts/ivh-treeview/ivh-treeview-theme-basic.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}
using System.Web;
using System.Web.Optimization;

namespace ProductWebPortal
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

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
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/all/css").Include(
                        "~/Content/themes/base/jquery.ui.all.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalSimple").Include(
                        "~/Scripts/jquery.validate.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //javascript helper and toolkit
            bundles.Add(new ScriptBundle("~/bundles/jshelper").Include(
                "~/Scripts/json2.js",
                "~/Scripts/toolkit/ajaxHelper.js"));

            //slickgrid css and javascript
            bundles.Add(new StyleBundle("~/Scripts/slickgrid/css").Include(
            "~/Scripts/slickgrid/slick.grid.css",
            "~/Scripts/slickgrid/css/smoothness/jquery-ui-1.8.16.custom.css",
            "~/Scripts/slickgrid/examples/examples.css",
            "~/Scripts/slickgrid/controls/slick.pager.css",
            "~/Scripts/slickgrid/controls/slick.columnpicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/slickgrid").Include(
                "~/Scripts/slickgrid/lib/firebugx.js",
                "~/Scripts/slickgrid/slick.core.js",
                "~/Scripts/slickgrid/slick.grid.js",
                "~/Scripts/slickgrid/slick.formatters.js",
                "~/Scripts/slickgrid/slick.editors.js",
                "~/Scripts/slickgrid/slick.dataview.js",
                "~/Scripts/slickgrid/controls/slick.pager.js",
                "~/Scripts/slickgrid/controls/slick.columnpicker.js",
                "~/Scripts/slickgrid/plugins/slick.checkboxselectcolumn.js",
                "~/Scripts/slickgrid/plugins/slick.cellrangedecorator.js",
                "~/Scripts/slickgrid/plugins/slick.cellrangeselector.js",
                "~/Scripts/slickgrid/plugins/slick.cellselectionmodel.js",
                "~/Scripts/slickgrid/plugins/slick.autotooltips.js",
                "~/Scripts/slickgrid/plugins/slick.cellcopymanager.js",
                "~/Scripts/slickgrid/plugins/slick.rowselectionmodel.js"));

            bundles.Add(new ScriptBundle("~/bundles/slickgrid/drag").Include(
                "~/Scripts/slickgrid/lib/jquery-ui-1.8.16.custom.min.js",
                "~/Scripts/slickgrid/lib/jquery.event.drag-2.0.min.js"));

            //easy ui css and javascript
            bundles.Add(new StyleBundle("~/bundles/easyui/css").Include(
                "~/Scripts/easyui/themes/default/easyui.css",
                "~/Scripts/easyui/themes/icon.css"));

            bundles.Add(new ScriptBundle("~/bundles/easyui/js").Include(
                "~/Scripts/easyui/jquery-1.8.0.minx.js",
                "~/Scripts/easyui/jquery.easyui.minx.js"));

        }
    }
}



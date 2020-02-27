using System.Web;
using System.Web.Optimization;

namespace SteelStudio.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            //------------------------javascript----------------------------//

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap*"));

            bundles.Add(new ScriptBundle("~/bundles/ace").Include(
                      "~/Scripts/ace*"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                      "~/Scripts/dataTables.bootstrap.min.js"));

            //--------------------------css----------------------------//

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap*",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/Date/css").Include(
                      "~/Content/assets/css/daterangepicker.css",
                      "~/Content/assets/css/datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/Font/css").Include(
                      "~/Content/assets/css/font-awesome*"));

            bundles.Add(new StyleBundle("~/Content/Ace/css").Include(
                      "~/Content/assets/css/ace*",
                      "~/Content/assets/css/chosen.css"));

            bundles.Add(new StyleBundle("~/Content/dataTables/css").Include(
                      "~/Content/dataTables.bootstrap.min.css"));
        }
    }
}

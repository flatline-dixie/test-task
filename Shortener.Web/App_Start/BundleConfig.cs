using System.Web;
using System.Web.Optimization;

namespace Shortener.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                          "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapvalidator").Include(
                      "~/Scripts/bootstrapvalidator.min.js"));




            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/shortener").Include(
                      "~/Content/Shortener.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrapvalidator").Include(
                      "~/Content/bootstrapvalidator.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/pager").Include(
                "~/Scripts/ko.pager.js"));

            bundles.Add(new ScriptBundle("~/bundles/storage").Include(
                "~/Scripts/jquery.storage.js"));
        }
    }
}

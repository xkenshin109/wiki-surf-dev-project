using System.Web;
using System.Web.Optimization;

namespace WikiSurfWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //Dependencies
            bundles.Add(new ScriptBundle("~/bundles/dep").IncludeDirectory(
                "~/Scripts/dep/",
                "*.js"));

            //Dependencies
            bundles.Add(new ScriptBundle("~/bundles/routes").IncludeDirectory(
                "~/Scripts/game/routes/",
                "*.js"));

            //Controls Scripts
            bundles.Add(new ScriptBundle("~/bundles/controls").IncludeDirectory(
                "~/Scripts/game/controls/",
                "*.js"));

            //Configuration settings
            bundles.Add(new ScriptBundle("~/bundles/config").IncludeDirectory(
                "~/Scripts/game/config",
                "*.json"));

            //Game Scripts
            bundles.Add(new ScriptBundle("~/bundles/game").IncludeDirectory(
                "~/Scripts/game/",
                "*.js"));

            //View Scripts
            bundles.Add(new ScriptBundle("~/bundles/views").IncludeDirectory(
                "~/Scripts/game/views",
                "*.js"));

            //Model Scripts
            bundles.Add(new ScriptBundle("~/bundles/models").IncludeDirectory(
                "~/Scripts/game/models/",
                "*.js"));

        }
    }
}

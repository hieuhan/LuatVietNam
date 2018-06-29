using System.Web.Optimization;

namespace LawsVN
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/assets/scripts/libs/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/assets/scripts/libs/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/assets/scripts/libs/jquery.unobtrusive*",
                "~/assets/scripts/libs/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/assets/scripts/libs/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/assets/css/style.css",
                "~/assets/css/responsive.css"));

            bundles.Add(new StyleBundle("~/bundles/jqueryUICss").Include(
                "~/assets/css/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/bundles/js").IncludeDirectory("~/assets/scripts/ui", "*.js", true));

            BundleTable.EnableOptimizations = true; 
        }
    }
}
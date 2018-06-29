using System.Web;
using System.Web.Optimization;

namespace LawsVNEN
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/scripts/libs/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/assets/scripts/libs/jquery-ui-{version}.js",
                        "~/assets/scripts/libs/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/assets/scripts/libs/jquery.unobtrusive*",
                        "~/assets/scripts/libs/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/scripts/Libs/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/Css").IncludeDirectory("~/assets/css", "*.css", true));

            bundles.Add(new ScriptBundle("~/bundles/Js").IncludeDirectory("~/assets/scripts/ui", "*.js", true));
            bundles.Add(new ScriptBundle("~/bundles/Js01").Include(
                        "~/assets/scripts/ui/alawsvn.js",
                        "~/assets/scripts/ui/jquery.mCustomScrollbar.js",
                        "~/assets/scripts/ui/popup.js",
                        "~/assets/scripts/ui/slider-carousel.js",
                        "~/assets/scripts/ui/slider-effect.js",
                        "~/assets/scripts/ui/tooltip.js"));

            bundles.Add(new ScriptBundle("~/bundles/documentJs").Include(
                "~/assets/scripts/libs/jquery-{version}.js",
                "~/assets/scripts/libs/jquery-ui-{version}.js",
                "~/assets/scripts/ui/alawsvn.js",
                "~/assets/scripts/ui/slider-carousel.js",
                "~/assets/scripts/libs/jquery.cookie.js",
                "~/assets/scripts/libs/jquery.unobtrusive*",
                "~/assets/scripts/libs/jquery.validate*"
            ));

            BundleTable.EnableOptimizations = false; 
        }
    }
}
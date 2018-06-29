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
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/scripts/Libs/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/Css").IncludeDirectory("~/assets/css","*.css",true));
            bundles.Add(new StyleBundle("~/bundles/CssMobile").IncludeDirectory("~/assets/mobile/css", "*.css", true));
            //bundles.Add(new ScriptBundle("~/bundles/JsMobile").IncludeDirectory("~/assets/mobile/scripts/", "*.js", true));
            bundles.Add(new ScriptBundle("~/bundles/Js").IncludeDirectory("~/assets/scripts/ui", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/mobileJs").Include(
                "~/assets/scripts/libs/jquery-{version}.js",
                "~/assets/scripts/libs/jquery-ui-{version}.js",
                "~/assets/mobile/scripts/alawsvn.js",
                "~/assets/mobile/scripts/accordion.js",
                "~/assets/mobile/scripts/nav-bar.js",
                "~/assets/mobile/scripts/nav-drop.js",
                "~/assets/mobile/scripts/popup.js",
                "~/assets/mobile/scripts/ie10-viewport-bug-workaround.js",
                "~/assets/mobile/scripts/dropdown2.js",
                "~/assets/scripts/libs/jquery.unobtrusive*",
                "~/assets/scripts/libs/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/documentJs").Include(
                "~/assets/scripts/libs/jquery-{version}.js",
                "~/assets/scripts/libs/jquery-ui-{version}.js",
                "~/assets/scripts/ui/alawsvn.js",
                "~/assets/scripts/ui/jquery.mCustomScrollbar.js",
                "~/assets/scripts/ui/tooltip.js",
                "~/assets/scripts/ui/slider-carousel.js",
                "~/assets/scripts/libs/jquery.unobtrusive*",
                "~/assets/scripts/libs/jquery.validate*"
                ));

            BundleTable.EnableOptimizations = true; 
        }
    }
}
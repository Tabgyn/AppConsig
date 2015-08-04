using System.Web.Optimization;

namespace AppConsig.Web.Gestor
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/jquery.slimscroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/skin").Include(
                      "~/Scripts/skins.js"));

            bundles.Add(new ScriptBundle("~/bundles/beyond").Include(
                "~/Scripts/beyond.js",
                "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/weather").Include(
                "~/Scripts/jquery.simpleWeather.js"));

            //Styles
            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/css/appconsig").Include(
                "~/Content/appconsig.css"));

            bundles.Add(new StyleBundle("~/css/beyond").Include(
                "~/Content/beyond.css",
                "~/Content/font-awesome.css",
                "~/Content/animate.css"
                ));

            bundles.Add(new StyleBundle("~/css/weather").Include(
                "~/Content/simpleWeather.css"));
        }
    }
}

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
                        "~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/respond.min.js",
                        "~/Scripts/jquery.slimscroll.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/skin").Include(
                      "~/Scripts/skins.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/beyond").Include(
                "~/Scripts/beyond.min.js",
                "~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/weather").Include(
                "~/Scripts/jquery.simpleWeather.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/appconsig").Include(
                "~/Scripts/appconsig.min.js"));

            //Styles
            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/css/appconsig").Include(
                "~/Content/appconsig.min.css"));

            bundles.Add(new StyleBundle("~/css/beyond").Include(
                "~/Content/beyond.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/animate.min.css"
                ));

            bundles.Add(new StyleBundle("~/css/weather").Include(
                "~/Content/simpleWeather.min.css"));
        }
    }
}

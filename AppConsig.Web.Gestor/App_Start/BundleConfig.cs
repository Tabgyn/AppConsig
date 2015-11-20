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
                        "~/Scripts/jquery-2.1.4.js"));

            bundles.Add(new ScriptBundle("~/bundles/widgets").Include(
                        "~/Scripts/datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/jquery.slimscroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/skin").Include(
                      "~/Scripts/skins.js"));

            bundles.Add(new ScriptBundle("~/bundles/beyond").Include(
                "~/Scripts/beyond.js",
                "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/appconsig").Include(
                "~/Scripts/appconsig.js"));

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

            BundleTable.EnableOptimizations = true;
        }
    }
}

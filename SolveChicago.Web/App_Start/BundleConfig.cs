using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Optimization;

namespace SolveChicago.Web
{
    public class BundleConfig
    {
        [ExcludeFromCodeCoverage]
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/functions").Include(
                      "~/Scripts/functions.js"));

            bundles.Add(new ScriptBundle("~/bundles/landing").Include(
                      "~/Scripts/landing.js"));

            bundles.Add(new ScriptBundle("~/bundles/profile_member").Include(
                      "~/Scripts/profile_member.js"));

            bundles.Add(new ScriptBundle("~/bundles/tokenfield").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/semantic").Include(
                      "~/Scripts/semantic.js"));


            bundles.Add(new StyleBundle("~/Content/tokenfield").Include(
                      "~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/overwrites.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/landing").Include(
                       "~/Content/bootstrap.css",
                       "~/Content/landing.css"));

            bundles.Add(new StyleBundle("~/Content/semantic").Include(
                       "~/Content/semantic/semantic.css"));

            bundles.Add(new StyleBundle("~/Content/foundation").Include(
                       "~/dist/css/foundation.css",
                       "~/Content/foundation-datepicker.css"));
        }
    }
}

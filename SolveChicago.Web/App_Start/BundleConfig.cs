using SolveChicago.Common;
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
            bundles.UseCdn = Settings.Website.UseCdn;
            BundleTable.EnableOptimizations = Settings.Website.UseCdn;


            bundles.Add(new ScriptBundle("~/bundles/jquery", "https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js").Include(
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

            bundles.Add(new ScriptBundle("~/bundles/profile").Include(
                      "~/Scripts/profile.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui", "https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/semantic", "https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.2.10/semantic.min.js").Include(
                      "~/Scripts/semantic.js"));


            bundles.Add(new StyleBundle("~/Content/jqueryui", "https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css").Include(
                      "~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/overwrites.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap", "https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.min.css").Include(
                        "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/landing").Include(
                       "~/Content/bootstrap.css",
                       "~/Content/landing.css"));

            bundles.Add(new StyleBundle("~/Content/semanticui", "https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.2.10/semantic.min.css").Include(
                       "~/Content/semantic/semantic.css"));
        }
    }
}

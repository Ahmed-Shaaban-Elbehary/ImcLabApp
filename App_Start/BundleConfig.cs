using System.Web;
using System.Web.Optimization;

namespace ImcLabApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap-Lumen.min.css",
                      "~/Content/site.css")
                .Include("~/Content/ionicons-2.0.1/css/ionicons.min.css", new CssRewriteUrlTransform()));
            
            bundles.Add(new StyleBundle("~/bundles/alertfiyCss").Include(
                      "~/alertifyjs/css/alertify.rtl.min.css",
                      "~/alertifyjs/css/themes/default.rtl.min.css")
                .Include("~/Content/ionicons-2.0.1/css/ionicons.min.css", new CssRewriteUrlTransform()));


            bundles.Add(new ScriptBundle("~/bundles/jQuery").Include(
                        "~/Scripts/jquery-3.4.1.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/custome.js"
                      ));
            
            bundles.Add(new ScriptBundle("~/bundles/notify").Include(
                      "~/Scripts/notify.min.js"
                      ));
            
            bundles.Add(new ScriptBundle("~/bundles/alertfiyJs").Include(
                      "~/alertifyjs/alertify.min.js"
                      ));
        }
    }
}



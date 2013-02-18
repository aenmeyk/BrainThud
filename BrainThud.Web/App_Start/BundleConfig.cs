using System.Web.Optimization;

namespace BrainThud.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
//            bundles.IgnoreList.Clear();
            bundles.UseCdn = true;

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif

            // Modernizr should be seperate since it loads first
            bundles.Add(new ScriptBundle(BundlePaths.MODERNIZR).Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle(BundlePaths.JQUERY, "//ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js").Include(
                "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle(BundlePaths.JQUERY_UI, "//ajax.googleapis.com/ajax/libs/jqueryui/1.8.23/jquery-ui.min.js").Include(
                "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle(BundlePaths.EXTERNAL_LIBS).Include(
                "~/Scripts/account.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery-ui*",
                "~/Scripts/plugins.js",
                "~/Scripts/knockout*",
                "~/Scripts/amplify.*",
                "~/Scripts/bootstrap.*",
                "~/Scripts/Markdown.*",
                "~/Scripts/toastr.js",
                "~/Scripts/moment.*",
                "~/Scripts/underscore.*",
                "~/Scripts/sammy.js"));

            bundles.Add(new ScriptBundle(BundlePaths.PUBLIC).IncludeDirectory(
                "~/Scripts/public", "*.js", searchSubdirectories: false));

            bundles.Add(new ScriptBundle(BundlePaths.APP_LIBS).IncludeDirectory(
                "~/Scripts/app", "*.js", searchSubdirectories: false));

            bundles.Add(new StyleBundle(BundlePaths.CSS).Include(
                "~/Content/main.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/toastr.css",
                "~/Content/markdown.css",
                "~/Content/themes/cupertino/jquery-ui*"
                ));

            bundles.Add(new Bundle(BundlePaths.LESS, new LessTransform(), new CssMinify()).Include(
                "~/Content/Site.less"));
        }
    }
}
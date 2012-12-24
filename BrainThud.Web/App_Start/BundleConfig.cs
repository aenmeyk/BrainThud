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
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/plugins.js",
                "~/Scripts/knockout-*",
                "~/Scripts/amplify.*",
                "~/Scripts/bootstrap.*",
                "~/Scripts/Markdown.*",
                "~/Scripts/toastr.js",
                "~/Scripts/moment.*",
                "~/Scripts/sammy.js"));

            bundles.Add(new ScriptBundle(BundlePaths.APP_LIBS).IncludeDirectory(
                "~/Scripts/app", "*.js", searchSubdirectories: false));

            bundles.Add(new StyleBundle(BundlePaths.CSS).Include(
                "~/Content/main.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/toastr.css",
                "~/Content/markdown.css",
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css"
                ));

            bundles.Add(new Bundle(BundlePaths.LESS, new LessTransform(), new CssMinify()).Include(
                "~/Content/Site.less"));
        }
    }
}
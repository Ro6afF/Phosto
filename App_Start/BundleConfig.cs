using System.Web;
using System.Web.Optimization;

namespace Phosto
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory("~/Scripts", "*.js", true));
            bundles.Add(new StyleBundle("~/bundles/styles").IncludeDirectory("~/Content", "*.css", true));
        }
    }
}

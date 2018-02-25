using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace WebApplication1
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/js/angular.js",
                "~/js/app.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                "~/css/bootstrap.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}

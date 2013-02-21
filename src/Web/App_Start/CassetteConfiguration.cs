using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace Dolstagis.Web.App_Start
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            bundles.AddPerSubDirectory<ScriptBundle>("Scripts");

            /*
             * Stylesheets are configured per file. This is because Less CSS
             * files can specify their own imports, which are incorporated into
             * the output for that file and are not rendered independently.
             * This means that to have one stylesheet (and therefore, one
             * bundle) per layout page, we can reference a single file in the
             * root of the stylesheets folder, and still modularise our
             * dependencies.
             */

            bundles.AddPerIndividualFile<StylesheetBundle>("Content");
        }
    }
}

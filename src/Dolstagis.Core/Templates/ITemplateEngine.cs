
namespace Dolstagis.Core.Templates
{
    /// <summary>
    ///  Provides an interface to whichever template engine we need to use.
    /// </summary>

    public interface ITemplateEngine
    {
        /// <summary>
        ///  Processes the specified template and returns the output.
        /// </summary>
        /// <param name="templateName">
        ///  The name of the template to process.
        /// </param>
        /// <param name="model">
        ///  The object to pass as data.
        /// </param>
        /// <returns>
        ///  The processed template.
        /// </returns>

        string Process(string templateName, object model);

        /// <summary>
        ///  Processes the specified template and returns the output.
        /// </summary>
        /// <typeparam name="T">
        ///  The type of the model to process.
        /// </typeparam>
        /// <param name="templateName">
        ///  The name of the template to process.
        /// </param>
        /// <param name="model">
        ///  The object to pass as data.
        /// </param>
        /// <returns>
        ///  The processed template.
        /// </returns>

        string Process<T>(string templateName, T model);
    }
}

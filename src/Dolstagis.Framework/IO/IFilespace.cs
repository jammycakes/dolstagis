using System.IO;

namespace Dolstagis.Framework.IO
{
    /// <summary>
    ///  Provides a mockable, injectable interface around a filespace.
    ///  This may have multiple implementations including Local, HTTP and Resources.
    /// </summary>
    public interface IFilespace
    {
        /// <summary>
        ///  Appends some text to an existing file.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to append to.
        /// </param>
        /// <param name="contents">
        ///  The text to append.
        /// </param>

        void Append(string filename, string contents);

        /// <summary>
        ///  Appends one or more lines to an existing file.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to append to.
        /// </param>
        /// <param name="contents">
        ///  The lines to append.
        /// </param>

        void AppendLines(string filename, params string[] lines);

        /// <summary>
        ///  Opens a text file for reading.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to open.
        /// </param>
        /// <returns>
        ///  A <see cref="TextReader"/> instance.
        /// </returns>

        TextReader OpenReader(string filename);

        /// <summary>
        ///  Opens a text file for writing.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to open.
        /// </param>
        /// <param name="append">
        ///  true to append to the file, otherwise false.
        /// </param>
        /// <returns>
        ///  A <see cref="TextWriter"/> instance.
        /// </returns>

        TextWriter OpenWriter(string filename, bool append = false);

        /// <summary>
        ///  Reads the entire contents of a file to a string.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to read.
        /// </param>
        /// <returns>
        ///  A string containing the contents of the file.
        /// </returns>

        string Read(string filename);

        /// <summary>
        ///  Writes a string to a file.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to write to.
        /// </param>
        /// <param name="contents">
        ///  The contents of the file to write.
        /// </param>
        
        void Write(string filename, string contents);

        /// <summary>
        ///  Gets a value indicating whether the filespace is read-only or writable.
        /// </summary>

        bool ReadOnly { get; }
    }
}

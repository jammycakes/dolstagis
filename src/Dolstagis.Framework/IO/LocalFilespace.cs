using System;
using System.IO;
using System.Text;

namespace Dolstagis.Framework.IO
{
    /// <summary>
    ///  Represents a filespace in the local filesystem.
    /// </summary>

    public class LocalFilespace : IFilespace
    {
        public string Root { get; private set; }

        public Encoding Encoding { get; private set; }

        /// <summary>
        ///  Gets a value indicating whether the filespace is read-only or writable.
        /// </summary>

        public bool ReadOnly { get { return false; } }

        /// <summary>
        ///  Creates a new instance of the <see cref="LocalFilespace"/> class,
        ///  rooted at a specified directory.
        /// </summary>
        /// <param name="root">
        ///  The root directory of the filespace.
        /// </param>
        /// <param name="encoding">
        ///  The default encoding to use to read and write to the files.
        ///  If none is specified, UTF-8 is assumed.
        /// </param>

        public LocalFilespace(string root, Encoding encoding = null)
        {
            this.Root = new DirectoryInfo(root).FullName;
            if (!this.Root.EndsWith(Path.DirectorySeparatorChar.ToString()))
                this.Root += Path.DirectorySeparatorChar;
            this.Encoding = encoding ?? Encoding.UTF8;
        }

        /// <summary>
        ///  Converts a relative path within the filespace to an absolute one.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file.
        /// </param>
        /// <returns>
        ///  The absolute path to the file.
        /// </returns>

        public string GetAbsolutePath(string filename)
        {
            string path = Path.Combine(this.Root, filename);
            return new FileInfo(path).FullName;
        }

        /// <summary>
        ///  Ensures that we have specified a file within the filespace, that it is not
        ///  a directory, and that its containing directory exists.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file we are reading or writing.
        /// </param>
        /// <returns>
        ///  The absolute path to the file.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///  The filename specifies a path to a file outside this filespace, or,
        ///  the caller has specified a directory, not a file.
        /// </exception>

        private string EnsureFile(string filename)
        {
            string path = GetAbsolutePath(filename);
            if (!path.StartsWith(this.Root, StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("Specified file is not within the filespace.", "filename");
            if (Directory.Exists(path))
                throw new ArgumentException("You have specified a directory, not a file.", "filename");
            var parent = Path.GetDirectoryName(path);
            Directory.CreateDirectory(parent);
            return path;
        }

        /// <summary>
        ///  Appends some text to an existing file.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to append to.
        /// </param>
        /// <param name="contents">
        ///  The text to append.
        /// </param>
        /// <exception cref="ArgumentException">
        ///  The filename specifies a path to a file outside this filespace.
        /// </exception>

        public void Append(string filename, string contents)
        {
            File.AppendAllText(EnsureFile(filename), contents, this.Encoding);
        }

        /// <summary>
        ///  Appends one or more lines to an existing file.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to append to.
        /// </param>
        /// <param name="contents">
        ///  The lines to append.
        /// </param>
        /// <exception cref="ArgumentException">
        ///  The filename specifies a path to a file outside this filespace.
        /// </exception>

        public void AppendLines(string filename, params string[] lines)
        {
            File.AppendAllLines(EnsureFile(filename), lines, this.Encoding);
        }

        /// <summary>
        ///  Opens a text file for reading.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to open.
        /// </param>
        /// <returns>
        ///  A <see cref="TextReader"/> instance.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///  The filename specifies a path to a file outside this filespace.
        /// </exception>

        public TextReader OpenReader(string filename)
        {
            return new StreamReader(EnsureFile(filename), this.Encoding);
        }

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
        /// <exception cref="ArgumentException">
        ///  The filename specifies a path to a file outside this filespace.
        /// </exception>

        public TextWriter OpenWriter(string filename, bool append = false)
        {
            return new StreamWriter(EnsureFile(filename), append, this.Encoding);
        }

        /// <summary>
        ///  Reads the entire contents of a file to a string.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to read.
        /// </param>
        /// <returns>
        ///  A string containing the contents of the file.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///  The filename specifies a path to a file outside this filespace.
        /// </exception>

        public string Read(string filename)
        {
            return File.ReadAllText(EnsureFile(filename), this.Encoding);
        }

        /// <summary>
        ///  Writes a string to a file.
        /// </summary>
        /// <param name="filename">
        ///  The name of the file to write to.
        /// </param>
        /// <param name="contents">
        ///  The contents of the file to write.
        /// </param>
        /// <exception cref="ArgumentException">
        ///  The filename specifies a path to a file outside this filespace.
        /// </exception>

        public void Write(string filename, string contents)
        {
            File.WriteAllText(EnsureFile(filename), contents, this.Encoding);
        }
    }
}

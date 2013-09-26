using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Static
{
    public class FileResource : IResource
    {
        private FileInfo _file;

        public FileResource(FileInfo file)
        {
            this._file = file;
        }

        public DateTime DateModified
        {
            get { return _file.LastWriteTimeUtc; }
        }

        public long Length
        {
            get { return _file.Length; }
        }

        public string Name
        {
            get { return _file.Name; }
        }

        public System.IO.Stream Open()
        {
            return new FileStream(_file.FullName, FileMode.Open, FileAccess.Read);
        }
    }
}

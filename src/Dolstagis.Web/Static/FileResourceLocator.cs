using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dolstagis.Web.Static
{
    public class FileResourceLocator : IResourceLocator
    {
        private string _root;

        private static string NormPath(string path)
        {
            return Path.GetFullPath(path);
        }

        public FileResourceLocator(string root)
        {
            _root = NormPath(root);
            if (!_root.EndsWith(Path.DirectorySeparatorChar.ToString())) {
                _root += Path.DirectorySeparatorChar;
            }
        }

        public IResource GetResource(string path)
        {
            var abspath = NormPath(Path.Combine(_root, path));
            if (!abspath.StartsWith(_root, StringComparison.OrdinalIgnoreCase)) return null;
            var file = new FileInfo(abspath);
            if (file.Exists) {
                return new FileResource(file);
            }
            else {
                return null;
            }
        }
    }
}

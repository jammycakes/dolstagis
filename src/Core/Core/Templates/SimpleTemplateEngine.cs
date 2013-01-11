using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;

namespace Dolstagis.Core.Templates
{
    public class SimpleTemplateEngine : ITemplateEngine
    {
        private string root;

        public SimpleTemplateEngine(string root)
        {
            if (root == null) throw new ArgumentNullException("root");
            var directory = new DirectoryInfo(root);
            if (!directory.Exists) {
                throw new IOException("The directory " + root + "does not exist.");
            }
            this.root = directory.FullName;
        }

        private string ProcessInternal(string templateName, object model)
        {
            var fi = new FileInfo(Path.Combine(this.root, templateName));
            if (!fi.FullName.StartsWith(this.root)) {
                throw new ArgumentException("Badly formed template name");
            }

            if (!fi.Exists) {
                throw new IOException("Template at " + templateName + " not found.");
            }

            return null;
        }

        public string Process(string templateName, object model)
        {
            return ProcessInternal(templateName, model);
        }

        public string Process<T>(string templateName, T model)
        {
            return ProcessInternal(templateName, model);
        }
    }
}

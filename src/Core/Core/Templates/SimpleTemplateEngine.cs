using Dolstagis.Core.IO;
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
        private IFilespace filespace;

        public SimpleTemplateEngine(IFilespace filespace)
        {
            this.filespace = filespace;
        }

        private string ProcessInternal(string templateName, object model)
        {
            string template = filespace.Read(templateName);
            return template;
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

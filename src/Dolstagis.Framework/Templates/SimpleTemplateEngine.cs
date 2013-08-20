using System;
using System.Linq;
using System.Text.RegularExpressions;
using Dolstagis.Framework.IO;

namespace Dolstagis.Framework.Templates
{
    public class SimpleTemplateEngine : ITemplateEngine
    {
        private IFilespace filespace;

        public SimpleTemplateEngine(IFilespace filespace)
        {
            this.filespace = filespace;
        }

        private static readonly Regex re = new Regex(@"\{\{(.+?)\}\}");

        private string ProcessInternal(string templateName, object model)
        {
            string template = filespace.Read(templateName);
            return re.Replace(template, x => {
                var prop = model.GetType().GetProperty(x.Groups[1].Value);
                if (prop == null) return x.Value;
                if (prop.GetIndexParameters().Any()) return x.Value;
                object obj = prop.GetValue(model, null);
                return (obj != null ? obj.ToString() : String.Empty);
            });
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

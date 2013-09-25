using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Http;

namespace Dolstagis.Web.Views
{
    public class TextView : IView
    {
        private string content;

        public TextView(string content)
        {
            this.content = content;
        }

        public void Render(IResponse response)
        {
            response.StatusCode = 200;
            response.StatusDescription = "OK";
            response.AddHeader("Content-Type", "text/plain");
            response.AddHeader("Content-Encoding", "utf-8");
            using (var writer = new StreamWriter(response.ResponseStream, Encoding.UTF8)) {
                writer.Write(this.content);
            }
        }
    }
}

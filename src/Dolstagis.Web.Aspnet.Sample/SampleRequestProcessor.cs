using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dolstagis.Web.Aspnet.Sample
{
    public class SampleRequestProcessor : IRequestProcessor
    {
        public void Process(IRequestContext context)
        {
            context.Response.AddHeader("Content-Type", "text/plain");
            context.Response.AddHeader("Content-Encoding", "utf-8");
            using (var writer = new StreamWriter(context.Response.ResponseStream, Encoding.UTF8)) {
                writer.WriteLine("Hello world");
            }
        }
    }
}

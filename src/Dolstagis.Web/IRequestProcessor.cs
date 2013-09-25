using System;
using Dolstagis.Web.Http;

namespace Dolstagis.Web
{
    public interface IRequestProcessor
    {
        void Process(IRequestContext context);
    }
}

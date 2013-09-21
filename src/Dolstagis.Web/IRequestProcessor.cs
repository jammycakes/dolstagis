using System;
namespace Dolstagis.Web
{
    public interface IRequestProcessor
    {
        void Process(IRequestContext context);
    }
}

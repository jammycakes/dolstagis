using Ninject.Modules;
using Ninject.Web.Common;

namespace Dolstagis.Web.Helpers
{
    public class HelperNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Flash.IFlashMessageStore>().To<Flash.FlashMessageStore>().InRequestScope();
        }
    }
}

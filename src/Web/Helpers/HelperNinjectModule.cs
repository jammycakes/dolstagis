using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

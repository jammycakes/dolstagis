using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core.Time
{
    public class TimeNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClock>().To<SystemClock>().InSingletonScope();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core.WebInfo
{
    public class UserAgent
    {
        public virtual int UserAgentID { get; protected set; }

        public virtual string String { get; set; }
    }
}

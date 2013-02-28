using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Dolstagis.Core.WebInfo;

namespace Dolstagis.Core.WebInfo.Mappings
{
    public class UserAgentMap : ClassMap<UserAgent>
    {
        public UserAgentMap()
        {
            Table("UserAgents");
            Id(x => x.UserAgentID).GeneratedBy.Native().UnsavedValue(default(long));
            Map(x => x.String).Not.Nullable();
        }
    }
}

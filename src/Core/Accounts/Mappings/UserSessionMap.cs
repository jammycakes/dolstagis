using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts.Mappings
{
    public class UserSessionMap : ClassMap<UserSession>
    {
        public UserSessionMap()
        {
            Table("UserSessions");
            Id(x => x.SessionID).Length(32).GeneratedBy.Assigned();
            References(x => x.User, "UserID").Not.LazyLoad();
            References(x => x.UserAgent, "UserAgentID");
            Map(x => x.IPAddress);
            Map(x => x.DateCreated);
            Map(x => x.DateLastAccessed);
        }
    }
}

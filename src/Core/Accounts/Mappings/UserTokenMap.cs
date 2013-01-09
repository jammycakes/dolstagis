using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts.Mappings
{
    public class UserTokenMap : ClassMap<UserToken>
    {
        public UserTokenMap()
        {
            Table("UserTokens");
            Id(x => x.Token).GeneratedBy.Assigned();
            this.References(x => x.User, "UserID").Not.LazyLoad();
            this.Map(x => x.Action).Length(32);
            this.Map(x => x.DateCreated);
        }
    }
}

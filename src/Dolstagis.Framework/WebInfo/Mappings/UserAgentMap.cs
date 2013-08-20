using FluentNHibernate.Mapping;

namespace Dolstagis.Framework.WebInfo.Mappings
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

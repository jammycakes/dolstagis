using Dolstagis.Contrib.Auth.Models;
using FluentNHibernate.Mapping;

namespace Dolstagis.Contrib.Auth.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.UserID).GeneratedBy.Native().UnsavedValue(default(long));
            Map(x => x.UserName).Not.Nullable().Length(20);
            Map(x => x.EmailAddress).Not.Nullable().Length(250);
            Map(x => x.PasswordHash).Not.Nullable().Length(100);
            Map(x => x.DisplayName).Not.Nullable().Length(100);
            Map(x => x.IsSuperUser).Not.Nullable();
            Map(x => x.Invitations).Not.Nullable();
            HasMany<UserSession>(x => x.Sessions).KeyColumn("UserID").LazyLoad().Inverse();
        }
    }
}

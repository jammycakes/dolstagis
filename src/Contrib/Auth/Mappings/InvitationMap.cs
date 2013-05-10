using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace Dolstagis.Contrib.Auth.Mappings
{
    public class InvitationMap : ClassMap<Invitation>
    {
        public InvitationMap()
        {
            Table("Invitations");
            Id(x => x.InvitationID).Length(32).GeneratedBy.Assigned().UnsavedValue(null);
            References(x => x.InvitingUser, "InvitingUserID").Nullable();
            Map(x => x.InviteeName).Not.Nullable().Length(100);
            Map(x => x.InviteeEmail).Not.Nullable().Length(250);
            References(x => x.Invitee, "InviteeID").Nullable();
            Map(x => x.DateCreated).Not.Nullable();
            Map(x => x.DateSent).Nullable();
        }
    }
}

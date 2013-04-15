using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dolstagis.Core.Mail;

namespace Dolstagis.Accounts
{
    public class Invitation : IMailable
    {
        public virtual string InvitationID { get; set; }

        public virtual User InvitingUser { get; set; }

        public virtual string InviteeName { get; set; }

        public virtual string InviteeEmail { get; set; }

        public virtual User Invitee { get; set; }

        public virtual DateTime DateCreated { get; set; }

        public virtual DateTime? DateSent { get; set; }

        string IMailable.Name
        {
            get { return this.InviteeName; }
        }

        string IMailable.EmailAddress
        {
            get { return this.InviteeEmail; }
        }

        public Invitation()
        {
        }
    }
}

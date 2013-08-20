using System;
using Dolstagis.Framework.Mail;

namespace Dolstagis.Contrib.Auth.Models
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


        public virtual InvitationState State
        {
            get
            {
                /*
                 * An invitation has been accepted if it has an invitee user account.
                 */
                if (this.Invitee != null)
                    return InvitationState.Accepted;

                /*
                 * An invitation has been orphaned if it has neither an inviting user
                 * nor an invitee e-mail address
                 */

                if (String.IsNullOrEmpty(this.InviteeEmail) && this.InvitingUser == null)
                    return InvitationState.Orphaned;

                /*
                 * An invitation has been sent if it has a send date.
                 */
                if (DateSent.HasValue)
                    return InvitationState.Sent;

                /*
                 * An invitation has been requested if it has an invitee e-mail
                 * but has not been sent.
                 */
                if (!String.IsNullOrEmpty(InviteeEmail))
                    return InvitationState.Requested;

                /*
                 * An invitation has been allocated if it has an inviting user
                 * but no invitee e-mail. If we get this far, this should always
                 * be true.
                 */
                if (InvitingUser != null)
                    return InvitationState.Allocated;

                /*
                 * This shouldn't ever run, but if it does, it will be orphaned.
                 */
                return InvitationState.Orphaned;
            }
        }
    }
}

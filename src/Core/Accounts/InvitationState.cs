using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    [Serializable]
    public enum InvitationState
    {
        /// <summary>
        ///  The invitation has been requested by a prospective invitee.
        /// </summary>
        Requested,

        /// <summary>
        ///  The invitation has been allocated to a registered user to send to a friend.
        /// </summary>
        Allocated,

        /// <summary>
        ///  The invitation has been sent to the invitee.
        /// </summary>
        Sent,

        /// <summary>
        ///  The invitation has been accepted by the invitee.
        /// </summary>
        Accepted,

        /// <summary>
        ///  The invitation has been "orphaned" -- most likely because the prospective
        ///  inviter has deleted his account.
        /// </summary>
        Orphaned
    }
}

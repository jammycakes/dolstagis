using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dolstagis.Core;
using NHibernate;

namespace Dolstagis.Accounts
{
    public class InvitationManager : ManagerBase
    {
        public InvitationManager(ISessionFactory sessionFactory) : base(sessionFactory) { }

        public InvitationManager(ISessionFactory sessionFactory, ISession session)
            : base(sessionFactory, session)
        { }

        private static RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

        private string GenerateInvitationID()
        {
            var bytes = new byte[24];
            random.GetBytes(bytes);
            return Convert.ToBase64String(bytes, Base64FormattingOptions.None).Substring(0, 32);
        }

        private Invitation CreateInvitation(User sender)
        {
            var invitation = new Invitation();
            invitation.InvitationID = GenerateInvitationID();
            invitation.InvitingUser = sender;
            invitation.DateCreated = DateTime.UtcNow;
            return invitation;
        }


        /// <summary>
        ///  Assigns a given number of invitations to all selected users.
        /// </summary>
        /// <param name="users">
        ///  The users to whom invitations are to be assigned.
        /// </param>
        /// <param name="invitationsPerUser">
        ///  The number of invitations to assign to each user.
        /// </param>

        public void AssignInvitations(IEnumerable<User> users, int invitationsPerUser)
        {
            foreach (var user in users) {
                for (int i = 0; i < invitationsPerUser; i++) {
                    this.Session.Save(CreateInvitation(user));
                }
            }
        }
    }
}

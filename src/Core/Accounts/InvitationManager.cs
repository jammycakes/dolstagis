using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dolstagis.Core;
using Dolstagis.Core.Mail;
using Dolstagis.Core.Templates;
using NHibernate;
using NHibernate.Linq;
using Ninject;

namespace Dolstagis.Accounts
{
    public class InvitationManager : ManagerBase
    {
        public InvitationManager(ISessionFactory sessionFactory) : base(sessionFactory) { }

        public InvitationManager(ISessionFactory sessionFactory, ISession session)
            : base(sessionFactory, session)
        { }

        [Inject]
        public IMailer Mailer { get; set; }

        [Inject]
        public ITemplateEngine TemplateEngine { get; set; }

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

        /// <summary>
        ///  Gets all unsent invitations available to a user.
        /// </summary>
        /// <param name="sender">
        ///  The user to whom the invitations are to be allocated.
        /// </param>
        /// <returns>
        ///  A query yielding a list of available invitations.
        /// </returns>

        private IQueryable<Invitation> GetAvailableInvitationsFor(User sender)
        {
            return this.Session.Query<Invitation>()
                .Where(x => x.InvitingUser == sender)
                .Where(x => x.Invitee == null && x.InviteeEmail == null);
        }

        /// <summary>
        ///  Gets the number of invitations available to a user.
        /// </summary>
        /// <param name="sender">
        ///  The user being queried for invitation availability.
        /// </param>
        /// <returns>
        ///  The number of unsent invitations available to the user.
        /// </returns>

        public int GetAvailableInvitationCountFor(User sender)
        {
            return GetAvailableInvitationsFor(sender).Count();
        }

        /// <summary>
        ///  Gets an invitation object from one user to another.
        /// </summary>
        /// <param name="sender">
        ///  The sender of the invitation.
        /// </param>
        /// <param name="inviteeName">
        ///  The name of the invitee.
        /// </param>
        /// <param name="inviteeEmail">
        ///  The email address of the invitee.
        /// </param>
        /// <returns>
        ///  An <see cref="Invitation"/> object.
        /// </returns>
        /// <exception cref="UserException">
        ///  The user has no invitations left to send.
        /// </exception>

        public Invitation GetInvitation(User sender, string inviteeName, string inviteeEmail)
        {
            var invitation = GetAvailableInvitationsFor(sender).FirstOrDefault();
            if (invitation == null) {
                if (sender.IsSuperUser) {
                    invitation = CreateInvitation(sender);
                    this.Session.Save(invitation);
                }
                else {
                    throw new UserException("Sorry, you do not have any invitations to send.");
                }
            }
            invitation.InviteeName = inviteeName;
            invitation.InviteeEmail = inviteeEmail;
            return invitation;
        }

        /// <summary>
        ///  Create and send an invitation.
        /// </summary>
        /// <param name="sender">
        ///  The user sending the invitation.
        /// </param>
        /// <param name="inviteeName">
        ///  The name of the user receiving the invitation.
        /// </param>
        /// <param name="inviteeEmail">
        ///  The e-mail address of the user receiving the invitation.
        /// </param>

        public void Invite(User sender, string inviteeName, string inviteeEmail)
        {
            var invitation = GetInvitation(sender, inviteeName, inviteeEmail);
            var message = new Message() {
                Html = this.TemplateEngine.Process("Accounts/Invitation.html", invitation),
                Text = this.TemplateEngine.Process("Accounts/Invitation.txt", invitation),
                Subject = "Your invitation"
            };
            this.Mailer.Send(invitation, message);
        }
    }
}

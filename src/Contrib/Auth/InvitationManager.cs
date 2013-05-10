using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dolstagis.Contrib.Auth.Models;
using Dolstagis.Core;
using Dolstagis.Core.Mail;
using Dolstagis.Core.Templates;
using NHibernate;
using NHibernate.Linq;
using Ninject;

namespace Dolstagis.Contrib.Auth
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
                user.Invitations += invitationsPerUser;
            }
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
            if (!sender.CanInvite) {
                throw new UserException("Sorry, you do not have any invitations to send.");
            }

            var invitation = new Invitation() {
                InvitationID = GenerateInvitationID(),
                InvitingUser = sender,
                DateCreated = DateTime.UtcNow,
                InviteeName = inviteeName,
                InviteeEmail = inviteeEmail
            };

            this.Session.Save(invitation);
            sender.Invitations--;
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

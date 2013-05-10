using Dolstagis.Core.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Dolstagis.Accounts
{
    public class User : IMailable, IIdentity
    {
        public virtual long UserID { get; protected set; }

        public virtual string UserName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual bool IsSuperUser { get; set; }

        public virtual int Invitations { get; set; }

        public virtual IList<UserSession> Sessions { get; protected set; }

        public User()
        {
            this.UserID = default(long);
            this.PasswordHash = "$";
            this.Sessions = new List<UserSession>();
        }

        string IMailable.Name
        {
            get { return this.DisplayName; }
        }

        string IMailable.EmailAddress
        {
            get { return this.EmailAddress; }
        }

        string IIdentity.AuthenticationType
        {
            get { return "Dolstagis"; }
        }

        bool IIdentity.IsAuthenticated
        {
            get { return true; }
        }

        string IIdentity.Name
        {
            get { return this.UserName; }
        }

        public virtual bool CanInvite
        {
            get
            {
                return this.Invitations > 0 || this.IsSuperUser;
            }
        }
    }
}

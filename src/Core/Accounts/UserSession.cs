using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Dolstagis.Accounts
{
    public class UserSession : IPrincipal
    {
        public virtual string SessionID { get; protected set; }

        public virtual User User { get; protected set; }

        public virtual DateTime DateCreated { get; protected set; }

        public virtual DateTime DateLastAccessed { get; set; }

        protected UserSession()
        {
        }

        public UserSession(User user, DateTime date) : this()
        {
            byte[] sid = new byte[24];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(sid);
            this.SessionID = Convert.ToBase64String(sid);
            this.User = user;
            this.DateCreated = date;
            this.DateLastAccessed = date;
        }

        IIdentity IPrincipal.Identity
        {
            get { return this.User; }
        }

        bool IPrincipal.IsInRole(string role)
        {
            return false;
        }
    }
}

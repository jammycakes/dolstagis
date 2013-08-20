using System;
using System.Security.Cryptography;
using System.Security.Principal;
using Dolstagis.Framework.WebInfo;

namespace Dolstagis.Contrib.Auth.Models
{
    public class UserSession : IPrincipal
    {
        public virtual string SessionID { get; protected set; }

        public virtual User User { get; protected set; }

        public virtual UserAgent UserAgent { get; protected set; }

        public virtual DateTime DateCreated { get; protected set; }

        public virtual DateTime DateLastAccessed { get; set; }

        public virtual string IPAddress { get; set; }

        protected UserSession()
        {
        }

        public UserSession(User user, UserAgent userAgent, DateTime date) : this()
        {
            byte[] sid = new byte[24];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(sid);
            this.SessionID = Convert.ToBase64String(sid);
            this.User = user;
            this.UserAgent = userAgent;
            this.DateCreated = date;
            this.DateLastAccessed = date;
        }

        IIdentity IPrincipal.Identity
        {
            get { return this.User; }
        }

        bool IPrincipal.IsInRole(string role)
        {
            return this.User.IsSuperUser;
        }
    }
}

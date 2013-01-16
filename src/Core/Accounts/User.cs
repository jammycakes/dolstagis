using Dolstagis.Core.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    public class User : IMailable
    {
        public virtual long UserID { get; protected set; }

        public virtual string UserName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string PasswordHash { get; protected set; }

        public virtual string DisplayName { get; set; }

        public virtual bool IsSuperUser { get; set; }

        public User()
        {
            this.UserID = default(long);
            this.PasswordHash = "$";
        }

        string IMailable.Name
        {
            get { return this.DisplayName; }
        }

        string IMailable.EmailAddress
        {
            get { return this.EmailAddress; }
        }
    }
}

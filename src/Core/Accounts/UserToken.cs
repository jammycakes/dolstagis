using Dolstagis.Core.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    public class UserToken
    {
        public virtual Guid Token { get; protected set; }

        public virtual User User { get; protected set; }

        public virtual string Action { get; set; }

        public virtual DateTime DateCreated { get; protected set; }

        protected UserToken()
        { }

        public UserToken(User user, string action, IClock clock)
        {
            this.Token = Guid.NewGuid();
            this.User = user;
            this.Action = action;
            this.DateCreated = clock.UtcNow();
        }
    }
}

using Dolstagis.Core.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts
{
    /// <summary>
    ///  Represents a token that is e-mailed to the user to perform a specific action.
    /// </summary>

    public class UserToken
    {
        /// <summary>
        ///  The token identifier.
        /// </summary>

        public virtual Guid Token { get; protected set; }

        /// <summary>
        ///  The user to whom this token was sent.
        /// </summary>

        public virtual User User { get; protected set; }

        /// <summary>
        ///  The controller action to be carried out when this token is used
        ///  on the website.
        /// </summary>

        public virtual string Action { get; protected set; }

        /// <summary>
        ///  The UTC date and time that the token expires.
        /// </summary>

        public virtual DateTime Expires { get; protected set; }

        protected UserToken()
        { }

        /// <summary>
        ///  Creates a new instance of the <see cref="UserToken"/> instance
        ///  for a specific user, action and expiry date.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="action"></param>
        /// <param name="expires"></param>

        public UserToken(User user, string action, DateTime expires)
        {
            this.Token = Guid.NewGuid();
            this.User = user;
            this.Action = action;
            this.Expires = expires;
        }

        /// <summary>
        ///  Gets a value indicating whether the token is valid.
        /// </summary>
        /// <param name="now">
        ///  The current time.
        /// </param>
        /// <returns>
        ///  true if the token is valid and has not expired; otherwise false.
        /// </returns>

        public virtual bool IsValid(DateTime now)
        {
            return now < this.Expires;
        }
    }
}

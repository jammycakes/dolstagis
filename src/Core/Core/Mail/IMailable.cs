using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core.Mail
{
    /// <summary>
    ///  An object with an address to which email can be sent.
    /// </summary>

    public interface IMailable
    {
        /// <summary>
        ///  The display name to show in the address.
        /// </summary>

        string Name { get; }

        /// <summary>
        ///  The email address to which the message should be sent.
        /// </summary>

        string EmailAddress { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts.Passwords
{
    public interface IPasswordProvider
    {
        /// <summary>
        ///  Hashes a password, auto-generating a salt.
        /// </summary>
        /// <param name="password">
        ///  The password to hash.
        /// </param>
        /// <returns>
        ///  A hash of the password.
        /// </returns>

        string ComputeHash(string password);

        /// <summary>
        ///  Verifies a password against its salted hash.
        /// </summary>
        /// <param name="password">
        ///  The password to verify.
        /// </param>
        /// <param name="hash">
        ///  The password's hash and salt.
        /// </param>
        /// <returns>
        ///  A <see cref="PasswordResult"/> object giving details of whether
        ///  the password is valid or not, and whether it requires upgrading.
        /// </returns>

        PasswordResult Verify(string password, string hash);

        /// <summary>
        ///  Returns a human readable description of the password hash method.
        /// </summary>

        string Description { get; }
    }
}

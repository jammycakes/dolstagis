using bc = BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts.Passwords.BCrypt
{
    /// <summary>
    ///  Hashes and verifies passwords using the Bcrypt algorithm.
    /// </summary>

    public class BCryptPasswordProvider : IPasswordProvider
    {
        public IBCryptSettings Settings { get; private set; }

        public BCryptPasswordProvider(IBCryptSettings settings)
        {
            this.Settings = settings;
        }

        public string ComputeHash(string password)
        {
            return bc.BCrypt.HashPassword(password, this.Settings.WorkFactor);
        }

        public PasswordResult Verify(string password, string hash)
        {
            if (bc.BCrypt.Verify(password, hash)) {
                return PasswordResult.Correct;
            }
            else {
                return PasswordResult.Incorrect;
            }
        }
    }
}

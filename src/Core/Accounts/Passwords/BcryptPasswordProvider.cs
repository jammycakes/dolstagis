using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Accounts.Passwords
{
    /// <summary>
    ///  Hashes and verifies passwords using the Bcrypt algorithm.
    /// </summary>

    public class BcryptPasswordProvider : IPasswordProvider
    {
        public BcryptSettings Settings { get; private set; }

        public BcryptPasswordProvider(BcryptSettings settings)
        {
            this.Settings = settings;
        }

        public string ComputeHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, this.Settings.WorkFactor);
        }

        public PasswordResult Verify(string password, string hash)
        {
            if (BCrypt.Net.BCrypt.Verify(password, hash)) {
                return PasswordResult.Correct;
            }
            else {
                return PasswordResult.Incorrect;
            }
        }
    }
}

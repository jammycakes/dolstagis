using bc = BCrypt.Net;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Contrib.Auth.Passwords.BCrypt
{
    /// <summary>
    ///  Hashes and verifies passwords using the Bcrypt algorithm.
    /// </summary>

    public class BCryptPasswordProvider : IPasswordProvider
    {
        Logger log = LogManager.GetCurrentClassLogger();

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
            string[] parts = hash.Split('$');

            // "$2a$10$x3A4kgKFKj3ojOhqfm2NgOq.4R5lQl/kWsOAxi7IfQs/VF.YmUTN6"

            if (parts.Length < 4) return PasswordResult.Unrecognised;
            if (parts[0] != String.Empty) return PasswordResult.Unrecognised;
            if (parts[1] != "2a") return PasswordResult.Unrecognised;
            int workFactor;
            if (!Int32.TryParse(parts[2], out workFactor)) return PasswordResult.Unrecognised;

            bool isCorrect;

            try {
                isCorrect = bc.BCrypt.Verify(password, hash);
            }
            catch (bc.SaltParseException) {
                log.Warn(() => "Invalid salt: " + hash);
                return PasswordResult.Unrecognised;
            }

            if (isCorrect) {
                if (workFactor < Settings.WorkFactor) {
                    return PasswordResult.CorrectButInsecure;
                }
                else {
                    return PasswordResult.Correct;
                }
            }
            else {
                return PasswordResult.Incorrect;
            }
        }

        public string Description
        {
            get
            {
                return "BCrypt with a work factor of " + this.Settings.WorkFactor;
            }
        }
    }
}

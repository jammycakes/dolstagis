using System;
using System.Security.Cryptography;
using System.Text;

namespace Dolstagis.Contrib.Auth.Passwords.Sha512
{
    public class Sha512PasswordProvider : IPasswordProvider
    {
        string prefix = "$6$";

        private string ComputeHash(string password, string salt)
        {
            using (var csp = SHA512.Create()) {
                var bytes = new UTF8Encoding().GetBytes(salt + password);
                var hash = csp.ComputeHash(bytes);
                return prefix + salt + "$" + Convert.ToBase64String(hash);
            }
        }

        public string ComputeHash(string password)
        {
            string salt;
            using (var rng = new RNGCryptoServiceProvider()) {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                salt = Convert.ToBase64String(bytes);
                return ComputeHash(password, salt);
            }
        }

        public PasswordResult Verify(string password, string hash)
        {
            if (!hash.StartsWith(prefix)) return PasswordResult.Unrecognised;
            var parts = hash.Split('$');
            if (parts.Length < 4) return PasswordResult.Unrecognised;
            string salt = parts[2];
            string check = ComputeHash(password, salt);
            return String.Compare(check, hash, true) == 0
                ? PasswordResult.Correct : PasswordResult.Incorrect;
        }

        public string Description
        {
            get
            {
                return "SHA512 with a 128-bit salt";
            }
        }
    }
}

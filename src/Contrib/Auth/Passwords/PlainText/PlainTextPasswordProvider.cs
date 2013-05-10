#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Contrib.Auth.Passwords.PlainText
{
    /// <summary>
    ///  Stores a password in plain text.
    /// </summary>
    /// <remarks>
    ///  This password provider is for testing purposes only.
    ///  It is only available when the project is compiled with the DEBUG configuration.
    ///  DO NOT USE IT IN PRODUCTION!!!!!!
    /// </remarks>

    public class PlainTextPasswordProvider : IPasswordProvider
    {
        private static string Prefix = "$pt$";

        public string ComputeHash(string password)
        {
            return Prefix + password;
        }

        public PasswordResult Verify(string password, string hash)
        {
            if (hash.StartsWith(Prefix)) {
                if (hash.Substring(Prefix.Length) == password)
                    return PasswordResult.CorrectButInsecure;
                else
                    return PasswordResult.Incorrect;
            }
            return PasswordResult.Unrecognised;
        }

        public string Description
        {
            get
            {
                return "Plain text";
            }
        }
    }
}

#endif
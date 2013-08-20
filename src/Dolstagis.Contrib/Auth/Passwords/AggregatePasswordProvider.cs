using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolstagis.Contrib.Auth.Passwords
{
    /// <summary>
    ///  Hashes and verifies passwords using the preferred algorithm from
    ///  a list of available providers.
    /// </summary>

    public class AggregatePasswordProvider : IPasswordProvider
    {
        private IList<IPasswordProvider> providers;

        public AggregatePasswordProvider(IEnumerable<IPasswordProvider> providers)
        {
            if (!providers.Any()) {
                throw new ArgumentException("No password providers were specified.");
            }

            this.providers = providers.ToList();
        }

        public string ComputeHash(string password)
        {
            return providers.First().ComputeHash(password);
        }

        public PasswordResult Verify(string password, string hash)
        {
            bool first = true;
            foreach (var provider in providers) {
                var result = provider.Verify(password, hash);
                switch (result) {
                    case PasswordResult.Correct:
                        return first ? PasswordResult.Correct : PasswordResult.CorrectButInsecure;
                    case PasswordResult.Unrecognised:
                        break;
                    default:
                        return result;
                }
                first = false;
            }
            return PasswordResult.Unrecognised;
        }

        public string Description
        {
            get
            {
                return this.providers.Select(x => x.Description).FirstOrDefault();
            }
        }
    }
}

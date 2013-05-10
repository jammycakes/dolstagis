using System;

namespace Dolstagis.Accounts.Passwords.BCrypt
{
    public interface IBCryptSettings
    {
        int WorkFactor { get; }
    }
}

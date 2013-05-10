using System;

namespace Dolstagis.Accounts
{
    public interface IAccountSettings
    {
        TimeSpan TokenLifetime { get; }
    }
}

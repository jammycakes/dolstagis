using System;

namespace Dolstagis.Contrib.Auth
{
    public interface IAccountSettings
    {
        TimeSpan TokenLifetime { get; }
    }
}

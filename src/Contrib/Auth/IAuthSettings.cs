using System;

namespace Dolstagis.Contrib.Auth
{
    public interface IAuthSettings
    {
        TimeSpan TokenLifetime { get; }
    }
}

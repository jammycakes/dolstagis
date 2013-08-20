using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dolstagis.Contrib.Auth.Models;
using Dolstagis.Framework.Data;

namespace Dolstagis.Contrib.Auth
{
    public interface IUserRepository : IRepository
    {
        void DeleteOtherSessionsForUser(string sessionID, User user);
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Models = Dolstagis.Contrib.Auth.Models;

namespace Dolstagis.Web.Areas.User.ViewModels
{
    public class Invitation
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public Models.User User { get; set; }
    }
}
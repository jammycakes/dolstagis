﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dolstagis.Accounts;

namespace Dolstagis.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class UsersController : Controller
    {
        private UserManager userManager;

        public UsersController(UserManager userManager)
        {
            this.userManager = userManager;
        }

        //
        // GET: /Admin/Users/

        public ActionResult Index()
        {
            var users = userManager.GetAllUsers();
            return View(users);
        }
    }
}

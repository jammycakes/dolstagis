﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dolstagis.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminAjaxController : Controller
    {
    }
}

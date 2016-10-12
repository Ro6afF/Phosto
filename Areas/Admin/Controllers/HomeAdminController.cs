using Phosto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phosto.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeAdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
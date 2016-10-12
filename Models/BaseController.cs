using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phosto.Models
{
    public class BaseController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
    }
}
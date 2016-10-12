using Phosto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phosto.Controllers
{
    public class GetImageController : BaseController
    {
        // GET: GetImage
        public ActionResult GetImage(string path)
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            return File(@"C:" + path, "image/*");
        }
    }
}
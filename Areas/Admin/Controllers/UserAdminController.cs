using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Phosto.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Phosto.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class UserAdminController : BaseController
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ban()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ban(BanModel model)
        {
            model.To = DateTime.Now.AddDays(1);
            db.Bans.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Admin(string username)
        {
            
            var userr = db.Users.Where(u => u.UserName.Equals(username)).FirstOrDefault();
            UserManager.AddToRole(userr.Id, "Admin");
            UserManager.RemoveFromRole(userr.Id, "User");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserM()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UserM(string username)
        {
            
            var userr = db.Users.Where(u => u.UserName.Equals(username)).FirstOrDefault();
            UserManager.AddToRole(userr.Id, "User");
            UserManager.RemoveFromRole(userr.Id, "Admin");
            return View();
        }

    }
}
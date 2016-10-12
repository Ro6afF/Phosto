using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phosto.Models;

namespace Phosto.Areas.Admin.Controllers
{
    public class CommentsAdminController : BaseController
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        public ActionResult Delete(int? id)
        {
            var comment = db.Comments.Find(id);
            int commented = comment.Commented;
            if (User.IsInRole("Admin") || comment.Author == User.Identity.Name)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
                return Redirect("/Home/Details/" + commented.ToString());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAdmin(int? id)
        {
            var comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return Redirect("/Admin/CommentsAdmin/Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

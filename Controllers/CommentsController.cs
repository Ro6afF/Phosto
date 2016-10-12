using Phosto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phosto.Controllers
{
    [Authorize]
    public class CommentsController : BaseController
    {
        
        public ActionResult Create(string Content, int Commented)
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            var comment = new CommentModel();
            comment.Content = Content;
            comment.Author = User.Identity.Name;
            comment.Commented = Commented;
            comment.Created = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Home", new { id = Commented });
        }
    }
}
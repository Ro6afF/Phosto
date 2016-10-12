using Phosto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Phosto.Areas.Admin.Controllers
{
    public class ImageAdminController : BaseController
    {
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageModel imageModel = db.ImageModels.Find(id);
            if(!(User.IsInRole("Admin") || imageModel.Author == User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (imageModel == null)
            {
                return HttpNotFound();
            }
            return View(imageModel);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            ImageModel imageModel = db.ImageModels.Find(id);
            if (!(User.IsInRole("Admin") || imageModel.Author == User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.ImageModels.Remove(imageModel);
            var comments = db.Comments.Where(x => x.Commented == id).ToList();
            foreach (var item in comments)
            {
                db.Comments.Remove(item);
            }
            var rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
            var ratings = db.RatedFrom.Where(x => x.iId == id).ToList();
            foreach (var item in ratings)
            {
                db.RatedFrom.Remove(item);
            }
            var ratingsv = db.RatedFromViews.Where(x => x.iId == id).ToList();
            foreach (var item in ratingsv)
            {
                db.RatedFromViews.Remove(item);
            }
            db.SaveChanges();
            System.IO.File.Delete(imageModel.Path);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
    }
}
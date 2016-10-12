using Phosto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phosto.Controllers
{
    [Authorize]
    public class RatingsController : BaseController
    {
        
        public ActionResult Like(int? id)
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            bool conatins = false;
            try
            {
                var ratingsFor = db.RatedFrom.Where(x => x.iId == id);
                foreach (var item in ratingsFor)
                {
                    if (item.user == User.Identity.Name)
                    {
                        conatins = true;
                        break;
                    }
                }
                if (!conatins)
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException)
            {
                var plok = new RatedFromModel();
                plok.user = User.Identity.Name;
                plok.iId = id;
                db.RatedFrom.Add(plok);
                var asd = db.Ratings.FirstOrDefault(x => x.id == id);
                asd.Likes++;
            }
            finally
            {
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Home", db.ImageModels.FirstOrDefault(x => x.id == id));
        }
        public ActionResult Hate(int? id)
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            bool conatins = false;
            try
            {
                var ratingsFor = db.RatedFrom.Where(x => x.iId == id);
                foreach (var item in ratingsFor)
                {
                    if (item.user == User.Identity.Name)
                    {
                        conatins = true;
                        break;
                    }
                }
                if (!conatins)
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException)
            {
                var plok = new RatedFromModel();
                plok.user = User.Identity.Name;
                plok.iId = id;
                db.RatedFrom.Add(plok);
                var asd = db.Ratings.FirstOrDefault(x => x.id == id);
                asd.Hates++;
            }
            finally
            {
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Home", db.ImageModels.FirstOrDefault(x => x.id == id));
        }
        public ActionResult Look(int? id)
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            bool conatins = false;
            try
            {
                var ratingsFor = db.RatedFromViews.Where(x => x.iId == id);
                foreach (var item in ratingsFor)
                {
                    if (item.user == User.Identity.Name)
                    {
                        conatins = true;
                        break;
                    }
                }
                if (!conatins)
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException)
            {
                var plok = new RatedFromViewsModel();
                plok.user = User.Identity.Name;
                plok.iId = id;
                db.RatedFromViews.Add(plok);
                var asd = db.Ratings.FirstOrDefault(x => x.id == id);
                asd.Views++;
            }
            finally
            {
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Home", new { id = id });
        }

    }
}
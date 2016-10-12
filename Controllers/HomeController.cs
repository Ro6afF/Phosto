using Phosto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Phosto.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string name = null, int type = 0)
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            if (type == 1)
            {
                return View((db.ImageModels.Where(x => x.Name.Contains(name))).ToList());
            }
            else if (type == 2)
            {
                return View((db.ImageModels.Where(x => x.Author.Contains(name))).ToList());
            }
            else
            {
                return View(db.ImageModels.ToList());
            }
        }


        public ActionResult Details(int? id)
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageModel imageModel = db.ImageModels.Find(id);
            if (imageModel == null)
            {
                return HttpNotFound();
            }
            return View(imageModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }

        public static bool IsImage(HttpPostedFileBase postedFile)
        {
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                byte[] buffer = new byte[512];
                postedFile.InputStream.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,Name,Description")] ImageModel imageModel, HttpPostedFileBase imageFile)
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            /*if(!IsImage(imageFile))
            {
                ModelState.AddModelError("", "Upload image");
            }*/
            if (ModelState.IsValid)
            {
                ImageModel asd = new ImageModel();
                db.Ratings.Add(new RatingModel());
                db.RatedFromViews.Add(new RatedFromViewsModel());
                asd.id = imageModel.id;
                asd.Name = imageModel.Name;
                asd.Description = imageModel.Description;
                asd.Author = User.Identity.Name;
                string ext = ".";
                int extlen = 0;
                for (int i = imageFile.FileName.Length - 1; imageFile.FileName[i] != '.'; i--)
                {
                    extlen++;
                }
                ext += imageFile.FileName.Substring(imageFile.FileName.Length - extlen);
                asd.Path = @"\Images\" + ((db.ImageModels.ToList()).Count + 1) + ext.ToString();
                db.ImageModels.Add(asd);
                BinaryReader b = new BinaryReader(imageFile.InputStream);
                byte[] binData = b.ReadBytes((int)imageFile.InputStream.Length);
                Directory.CreateDirectory(@"\Images\");
                System.IO.File.WriteAllBytes(@"\Images\" + ((db.ImageModels.ToList()).Count + 1).ToString() + ext, binData);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(imageModel);
        }

        public ActionResult Rules()
        {
            var bans = db.Bans.Where(x => x.Username == User.Identity.Name && x.To > DateTime.Now).ToList();
            if (bans.Count > 0)
            {
                return RedirectToAction("LogOff", "Account");
            }
            return View();
        }
    }
}
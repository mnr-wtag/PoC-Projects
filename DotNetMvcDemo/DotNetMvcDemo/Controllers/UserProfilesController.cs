using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DotNetMvcDemo.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();


        public ActionResult Index()
        {
            var userProfiles = _db.UserProfiles.Include(u => u.AuthUser);
            return View(userProfiles.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userProfile = _db.UserProfiles.Find(id);
            return userProfile == null ? HttpNotFound() : (ActionResult)View(userProfile);
        }


        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(_db.AuthUsers, "Id", "UserName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserPhoto,Gender")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                _db.UserProfiles.Add(userProfile);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(_db.AuthUsers, "Id", "UserName", userProfile.Id);
            return View(userProfile);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userProfile = _db.UserProfiles.Find(id);
            if (userProfile == null) return HttpNotFound();
            ViewBag.Id = new SelectList(_db.AuthUsers, "Id", "UserName", userProfile.Id);
            return View(userProfile);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserPhoto,Gender")] UserProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(userProfile).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(_db.AuthUsers, "Id", "UserName", userProfile.Id);
            return View(userProfile);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userProfile = _db.UserProfiles.Find(id);
            return userProfile == null ? HttpNotFound() : (ActionResult)View(userProfile);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userProfile = _db.UserProfiles.Find(id);
            _db.UserProfiles.Remove(userProfile);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
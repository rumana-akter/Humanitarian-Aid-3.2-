using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumanitarianAid.Models;

namespace HumanitarianAid.Controllers
{
    public class DonorratingsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: Donorratings
        public ActionResult Index()
        {
            var donorratings = db.Donorratings.Include(d => d.DonorDetail);
            return View(donorratings.ToList());
        }

        // GET: Donorratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donorrating donorrating = db.Donorratings.Find(id);
            if (donorrating == null)
            {
                return HttpNotFound();
            }
            return View(donorrating);
        }

        // GET: Donorratings/Create
        public ActionResult Create()
        {
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name");
            return View();
        }

        // POST: Donorratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "D_ID,DonorRatings")] Donorrating donorrating)
        {
            if (ModelState.IsValid)
            {
                db.Donorratings.Add(donorrating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donorrating.D_ID);
            return View(donorrating);
        }

        // GET: Donorratings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donorrating donorrating = db.Donorratings.Find(id);
            if (donorrating == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donorrating.D_ID);
            return View(donorrating);
        }

        // POST: Donorratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "D_ID,DonorRatings")] Donorrating donorrating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donorrating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donorrating.D_ID);
            return View(donorrating);
        }

        // GET: Donorratings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donorrating donorrating = db.Donorratings.Find(id);
            if (donorrating == null)
            {
                return HttpNotFound();
            }
            return View(donorrating);
        }

        // POST: Donorratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donorrating donorrating = db.Donorratings.Find(id);
            db.Donorratings.Remove(donorrating);
            db.SaveChanges();
            return RedirectToAction("Index");
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

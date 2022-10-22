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
    public class DonationCountsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: DonationCounts
        public ActionResult Index()
        {
            var donationCounts = db.DonationCounts.Include(d => d.DonorDetail);
            return View(donationCounts.ToList());
        }

        // GET: DonationCounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationCount donationCount = db.DonationCounts.Find(id);
            if (donationCount == null)
            {
                return HttpNotFound();
            }
            return View(donationCount);
        }

        // GET: DonationCounts/Create
        public ActionResult Create()
        {
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name");
            return View();
        }

        // POST: DonationCounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "D_ID,NoOfDonatedProducts")] DonationCount donationCount)
        {
            if (ModelState.IsValid)
            {
                db.DonationCounts.Add(donationCount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donationCount.D_ID);
            return View(donationCount);
        }

        // GET: DonationCounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationCount donationCount = db.DonationCounts.Find(id);
            if (donationCount == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donationCount.D_ID);
            return View(donationCount);
        }

        // POST: DonationCounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "D_ID,NoOfDonatedProducts")] DonationCount donationCount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donationCount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donationCount.D_ID);
            return View(donationCount);
        }

        // GET: DonationCounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationCount donationCount = db.DonationCounts.Find(id);
            if (donationCount == null)
            {
                return HttpNotFound();
            }
            return View(donationCount);
        }

        // POST: DonationCounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonationCount donationCount = db.DonationCounts.Find(id);
            db.DonationCounts.Remove(donationCount);
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

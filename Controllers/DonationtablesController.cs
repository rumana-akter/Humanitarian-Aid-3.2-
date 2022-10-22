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
    public class DonationtablesController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();
        //DonorDetailsController donor = new DonorDetailsController();
        
        

        // GET: Donationtables
        public ActionResult Index()
        {
            var donationtables = db.Donationtables.Include(d => d.DonorDetail);
            return View(donationtables.ToList());
        }

        // GET: Donationtables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donationtable donationtable = db.Donationtables.Find(id);
            if (donationtable == null)
            {
                return HttpNotFound();
            }
            return View(donationtable);
        }

        // GET: Donationtables/Create
        public ActionResult Create()
        {
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id");
            return View();
        }

        // POST: Donationtables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Productid,Producttype,Productname,Quantity,Unit,D_id")] Donationtable donationtable)
        {
            donationtable.DateOfDonation = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Donationtables.Add(donationtable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donationtable.D_ID);
            return View(donationtable);
        }

        // GET: Donationtables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donationtable donationtable = db.Donationtables.Find(id);
            if (donationtable == null)
            {
                return HttpNotFound();
            }
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donationtable.D_ID);
            return View(donationtable);
        }

        // POST: Donationtables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Productid,Producttype,Productname,Quantity,Unit,DateOfDonation,D_id")] Donationtable donationtable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donationtable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", donationtable.D_ID);
            return View(donationtable);
        }

        // GET: Donationtables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donationtable donationtable = db.Donationtables.Find(id);
            if (donationtable == null)
            {
                return HttpNotFound();
            }
            return View(donationtable);
        }

        // POST: Donationtables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donationtable donationtable = db.Donationtables.Find(id);
            db.Donationtables.Remove(donationtable);
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

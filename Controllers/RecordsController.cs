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
    public class RecordsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: Records
        public ActionResult Index()
        {
            var records = db.Records.Include(r => r.Donationtable).Include(r => r.DonorDetail).Include(r => r.NeedyDetail);
            return View(records.ToList());
        }

        // GET: Records/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // GET: Records/Create
        public ActionResult Create()
        {
            ViewBag.P_ID = new SelectList(db.Donationtables, "Productid", "Producttype");
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name");
            ViewBag.N_ID = new SelectList(db.NeedyDetails, "N_id", "N_name");
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SL,D_ID,N_ID,P_ID,R_ProductName,Quantity,unit")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Records.Add(record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.P_ID = new SelectList(db.Donationtables, "Productid", "Producttype", record.P_ID);
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", record.D_ID);
            ViewBag.N_ID = new SelectList(db.NeedyDetails, "N_id", "N_name", record.N_ID);
            return View(record);
        }

        // GET: Records/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            ViewBag.P_ID = new SelectList(db.Donationtables, "Productid", "Producttype", record.P_ID);
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", record.D_ID);
            ViewBag.N_ID = new SelectList(db.NeedyDetails, "N_id", "N_name", record.N_ID);
            return View(record);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SL,D_ID,N_ID,P_ID,R_ProductName,Quantity,unit")] Record record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.P_ID = new SelectList(db.Donationtables, "Productid", "Producttype", record.P_ID);
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", record.D_ID);
            ViewBag.N_ID = new SelectList(db.NeedyDetails, "N_id", "N_name", record.N_ID);
            return View(record);
        }

        // GET: Records/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return HttpNotFound();
            }
            return View(record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Record record = db.Records.Find(id);
            db.Records.Remove(record);
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

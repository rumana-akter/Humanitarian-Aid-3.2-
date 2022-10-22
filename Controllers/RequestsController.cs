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
    public class RequestsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Donationtable).Include(r => r.DonorDetail).Include(r => r.NeedyDetail);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.P_ID = new SelectList(db.Donationtables, "Productid", "Producttype");
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name");
            ViewBag.N_ID = new SelectList(db.NeedyDetails, "N_id", "N_name");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SL,D_ID,N_ID,P_ID,ProductName,Quantity,unit")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.P_ID = new SelectList(db.Donationtables, "Productid", "Producttype", request.P_ID);
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", request.D_ID);
            ViewBag.N_ID = new SelectList(db.NeedyDetails, "N_id", "N_name", request.N_ID);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.P_ID = new SelectList(db.Donationtables, "Productid", "Producttype", request.P_ID);
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", request.D_ID);
            ViewBag.N_ID = new SelectList(db.NeedyDetails, "N_id", "N_name", request.N_ID);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SL,D_ID,N_ID,P_ID,ProductName,Quantity,unit")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.P_ID = new SelectList(db.Donationtables, "Productid", "Producttype", request.P_ID);
            ViewBag.D_ID = new SelectList(db.DonorDetails, "D_id", "D_name", request.D_ID);
            ViewBag.N_ID = new SelectList(db.NeedyDetails, "N_id", "N_name", request.N_ID);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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

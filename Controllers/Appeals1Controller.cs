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
    public class Appeals1Controller : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: Appeals1
        public ActionResult Index()
        {
            return View(db.Appeals.ToList());
        }

        // GET: Appeals1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appeal appeal = db.Appeals.Find(id);
            if (appeal == null)
            {
                return HttpNotFound();
            }
            return View(appeal);
        }

        // GET: Appeals1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appeals1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppealId,N_id,Productname,Quantity,Unit,Story,Status")] Appeal appeal)
        {
            if (ModelState.IsValid)
            {
                db.Appeals.Add(appeal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appeal);
        }

        // GET: Appeals1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appeal appeal = db.Appeals.Find(id);
            appeal.Status = "Approve";

            if (appeal == null)
            {
                return HttpNotFound();
            }
            return View(appeal);
        }

        // POST: Appeals1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppealId,N_id,Productname,Quantity,Unit,Story,Status")] Appeal appeal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appeal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appeal);
        }

        // GET: Appeals1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appeal appeal = db.Appeals.Find(id);
            if (appeal == null)
            {
                return HttpNotFound();
            }
            return View(appeal);
        }

        // POST: Appeals1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appeal appeal = db.Appeals.Find(id);
            db.Appeals.Remove(appeal);
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

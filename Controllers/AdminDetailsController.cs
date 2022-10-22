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
    public class AdminDetailsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: AdminDetails
        public ActionResult Index()
        {
            return View(db.AdminDetails.ToList());
        }

        // GET: AdminDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminDetail adminDetail = db.AdminDetails.Find(id);
            if (adminDetail == null)
            {
                return HttpNotFound();
            }
            return View(adminDetail);
        }

        // GET: AdminDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "A_id,A_name,A_mail,A_pass,A_gender,A_DOB,A_address,A_phoneNo")] AdminDetail adminDetail)
        {
            if (ModelState.IsValid)
            {
                db.AdminDetails.Add(adminDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminDetail);
        }

        // GET: AdminDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminDetail adminDetail = db.AdminDetails.Find(id);
            if (adminDetail == null)
            {
                return HttpNotFound();
            }
            return View(adminDetail);
        }

        // POST: AdminDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "A_id,A_name,A_mail,A_pass,A_gender,A_DOB,A_address,A_phoneNo")] AdminDetail adminDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminDetail);
        }

        // GET: AdminDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminDetail adminDetail = db.AdminDetails.Find(id);
            if (adminDetail == null)
            {
                return HttpNotFound();
            }
            return View(adminDetail);
        }

        // POST: AdminDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminDetail adminDetail = db.AdminDetails.Find(id);
            db.AdminDetails.Remove(adminDetail);
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

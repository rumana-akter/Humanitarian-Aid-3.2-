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
    public class DonationsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: Donations
        public ActionResult Index()
        {
            return View(db.Donations.ToList());
        }

        // GET: Donations/Details/5
      /*  public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }
      */
        // GET: Donations/Create
      /*  public ActionResult Create()
        {
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonationId,D_id,Productname,Quantity,Unit,Status")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Donations.Add(donation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donation);
        }*/

        // GET: Donations/Edit/5

        //admin accept donation request
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            donation.Status = "Approved";
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationId,D_id,Productname,Quantity,Unit,Status")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donation);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       /* protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/



        //ADmin donationrequest more

        public ActionResult DetailsOfDonor(int? id)
        {
            Donation ap = db.Donations.Find(id);
            String query = $"select DonorDetails. * , Donation. *  FROM DonorDetails FULL JOIN Donation ON DonorDetails.D_id=Donation.D_id WHERE Donation.DonationId={id}";
            var d = db.Database.SqlQuery<Donation>(query).ToList();
            var result = (from a in d
                          join c in db.DonorDetails on a.D_id equals c.D_id
                          select new viewmodel3
                          {
                              DonationId = a.DonationId,
                              ProductName = a.Productname,
                              DonorName = c.D_name,
                              Address = c.D_address,
                              PhoneNo = c.D_phoneNo,
                              Email = c.D_mail,
                              AnnualIncome = c.D_annualincome,
                              Occupation = c.D_occupation

                          }).ToList();



            ViewBag.user = result;
            return View();
        }





    }
}

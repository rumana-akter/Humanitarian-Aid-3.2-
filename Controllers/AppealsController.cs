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
    public class AppealsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();      
       
        
        //Admin 
        
        public ActionResult Index()
        {
            return View(db.Appeals.ToList());
        } 

        //donor
        public ActionResult Index2()
        {
            return View(db.Appeals.ToList());
        }            
                    
        public ActionResult Create()
        {
            return View();
        }
       
      //  [HttpPost]
        //[ValidateAntiForgeryToken]
       /* public ActionResult Create([Bind(Include = "AppealId,N_id,Productname,Quantity,Unit,Story,Status")] Appeal appeal)
        {
            if (ModelState.IsValid)
            {
                db.Appeals.Add(appeal);
                db.SaveChanges();
                Response.Write("<script>alert('Successfully Appealed');</script>");
                
            }

            return View();
        }*/
            

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
               
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appeal appeal = db.Appeals.Find(id);
            db.Appeals.Remove(appeal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appeal appeal = db.Appeals.Find(id);
            appeal.Status = "Approved";
            if (appeal == null)
            {
                return HttpNotFound();
            }
            return View(appeal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve([Bind(Include = "AppealId,N_id,Productname,Quantity,Unit,Story,Status")] Appeal appeal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appeal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appeal);
        }


        //Admin Appeals request more
        public ActionResult DetailsOfSeeker(int? id)
        {
            Donation ap = db.Donations.Find(id);
            String query = $"select NeedyDetails. * , Appeal. *  FROM NeedyDetails FULL JOIN Appeal ON NeedyDetails.N_id=Appeal.N_id WHERE Appeal.AppealId={id}";
            var d = db.Database.SqlQuery<Appeal>(query).ToList();
            var result = (from a in d
                          join c in db.NeedyDetails on a.N_id equals c.N_id
                          select new viewmodel4
                          {
                              AppealId = a.AppealId,
                              ProductName = a.Productname,
                              SeekerName = c.N_name,
                              Address = c.N_address,
                              PhoneNo = c.N_phoneNo,
                              Email = c.N_mail,
                              AnnualIncome = c.N_annualincome,
                              Occupation = c.N_occupation,
                              Story = a.Story

                          }).ToList();

            ViewBag.user = result;
            return View();
        }







    }
}

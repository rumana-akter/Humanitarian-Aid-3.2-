using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HumanitarianAid.Models;

namespace HumanitarianAid.Controllers
{
    public class DonorDetailsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: DonorDetails
       //admin 
        public ActionResult Index()
        {
            return View(db.DonorDetails.ToList());
        }

        // GET: DonorDetails/Details/5
       /* public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonorDetail donorDetail = db.DonorDetails.Find(id);
            if (donorDetail == null)
            {
                return HttpNotFound();
            }
            return View(donorDetail);
        }
       */


        // GET: DonorDetails/Create
    /*    public ActionResult Create()
        {
            return View();
        }

        // POST: DonorDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "D_id,D_name,D_mail,D_Password,D_ConfirmPassword,D_dob,D_age,D_annualincome,D_gender,D_address,D_phoneNo,D_nationality,D_occupation")] DonorDetail donorDetail)
        {
            if (ModelState.IsValid)
            {
                db.DonorDetails.Add(donorDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donorDetail);
        }*/

        // GET: DonorDetails/Edit/5
      /*  public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonorDetail donorDetail = db.DonorDetails.Find(id);
            if (donorDetail == null)
            {
                return HttpNotFound();
            }
            return View(donorDetail);
        }

        // POST: DonorDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "D_id,D_name,D_mail,D_Password,D_ConfirmPassword,D_dob,D_age,D_annualincome,D_gender,D_address,D_phoneNo,D_nationality,D_occupation")] DonorDetail donorDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donorDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donorDetail);
        }
      */

        // GET: DonorDetails/Delete/5
      /*  public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonorDetail donorDetail = db.DonorDetails.Find(id);
            if (donorDetail == null)
            {
                return HttpNotFound();
            }
            return View(donorDetail);
        }

        // POST: DonorDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonorDetail donorDetail = db.DonorDetails.Find(id);
            db.DonorDetails.Remove(donorDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        */
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public ActionResult SignUp(DonorDetail user)
        {
            if (ModelState.IsValid)
            {
               

                var isExist = IsEmailExist(user.D_mail);
                if (isExist)
                {
                    
                    ViewBag.Loginfailed = "Email AlreadyExist";
                    return View();

                  
                }
                else
                {


                    db.DonorDetails.Add(user);
                    db.SaveChanges();
                    Response.Write("<script>alert('Registration successful Check your email to verify');</script>");
                    SendActivationEmail(user);
                    
                }

            }
            return View();
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {


            {
                var v = db.DonorDetails.Where(a => a.D_mail == emailID).FirstOrDefault();
                return v != null;
            }
        }





        public ActionResult Activation()
        {
            ViewBag.Message = "Invalid Activation code.";
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());
                // UsersEntities usersEntities = new UsersEntities();
                DonorActivation userActivation = db.DonorActivations.Where(p => p.ActivationCode== activationCode).FirstOrDefault();
                if (userActivation != null)
                {
                    db.DonorActivations.Remove(userActivation);
                    db.SaveChanges();
                    ViewBag.Message = "Activation successful.";
                }
            }

            return View();
        }

        private void SendActivationEmail(DonorDetail user)
        {
            Guid activationCode = Guid.NewGuid();
            // UsersEntities usersEntities = new UsersEntities();
            db.DonorActivations.Add(new DonorActivation
            {
                D_Id = user.D_id,
                ActivationCode = activationCode
            });
            db.SaveChanges();

            using (MailMessage mm = new MailMessage("groupprojectara@gmail.com", user.D_mail))
            {
                var verifyUrl = "/DonorDetails/Activation/" + activationCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                mm.Subject = "Account Activation";
                string body = "Hello " + user.D_name + ",";
                //body += "<br /><br />Please click the following link to activate your account";
                //body += "<br /><a href = '" + string.Format("{0}://{1}/DonorDetails/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
                body += "<br/> Your Account is successfully created.Please click to verify your account"+"<br/> <a href='"+link+"'>"+link+" </a>";
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("groupprojectara@gmail.com", "ydnurtuhoordscru ");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

























        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }







        public ActionResult Login(TempUser TempUser)
        {

            if (ModelState.IsValid)
            {
                var user = db.DonorDetails.Where(u => u.D_mail.Equals(TempUser.D_mail)
                  && u.D_Password.Equals(TempUser.D_Password)).FirstOrDefault();

                if (user != null)
                {
                    // return Content("Login Successfull");
                    Session["D_Mail"] = user.D_mail;
                    Session["D_id"] = user.D_id;
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    ViewBag.Loginfailed = "User not found or password mismatched";
                    return View();
                    // return Content("Login Failed");
                }



            }
            return View();
        }
        public ActionResult DashBoard()
        {
            string email = Convert.ToString(Session["D_Mail"]);
            string id = Convert.ToString(Session["D_id"]);
            var user = db.DonorDetails.Where(u => u.D_mail.Equals(email)).FirstOrDefault();
            return View(user);

        }
        [HttpGet]
        public ActionResult Donate()
        {
            return View();
        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Donate(Donation user)
        { // Session["D_id"] = user.D_id;
            String id = Convert.ToString(Session["D_id"]);
            int did = Convert.ToInt32(id);
            // ViewBag.showId = id;

            if (ModelState.IsValid)
            {

                user.Status = "Pending";
                int x = db.Database.ExecuteSqlCommand
                    ("insert into Donation (D_id,Productname,Quantity,Unit,Status) " +
                    "values('" + did + "', '" + user.Productname + "', '" + user.Quantity + "'" +
                    ",'" + user.Unit + "','" + user.Status + "')");
                db.SaveChanges();
                Response.Write("<script>alert('Donation request has been sent successfully');</script>");

            }
            return View();
        }


        public ActionResult RequestNotification()
        {
            String id = Convert.ToString(Session["D_id"]);
            int nid = Convert.ToInt32(id);
            var user = db.Donations.Where(u => u.D_id == nid).ToList();
            return View(user);
        }

        //from donor view appeals
        public ActionResult Index2()
        {
            return View(db.Appeals.ToList());
        }





        [HttpGet]
        public ActionResult Help(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appeal appeal = db.Appeals.Find(id);
            //appeal.Status = "Approve";

            if (appeal == null)
            {
                return HttpNotFound();
            }
            return View(appeal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Help([Bind(Include = "AppealId,N_id,Productname,Quantity,Unit")] Appeal user)
        {
            String id = Convert.ToString(Session["D_id"]);
            int did = Convert.ToInt32(id);
            if (ModelState.IsValid)
            {
                if (user.Status == "Pending")
                {
                    Response.Write("<script>alert('Donated  unsuccessfull as admin did not approve');</script>");
                }
                else
                {
                    //user.Status = "Pending";
                    int x = db.Database.ExecuteSqlCommand
                        ("insert into Records1 (D_ID,N_id,AppealId,Productname,Quantity,Unit) " +
                        "values('" + did + "','" + user.N_id + "', '" + user.AppealId + "','" + user.Productname + "', '" + user.Quantity + "'" +
                        ",'" + user.Unit + "')");

                    user = db.Appeals.Find(user.AppealId);
                    db.Appeals.Remove(user);

                    db.SaveChanges();
                    Response.Write("<script>alert('Donated  successfully');</script>");

                }
               
            }
            return View(user);

        }



        public ActionResult Group(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonorDetail ap = db.DonorDetails.Find(id);
            if (ap == null)
            {
                return HttpNotFound();
            }
            List<Donation> sameSeeker = db.Donations.Where(a => a.D_id == ap.D_id).ToList<Donation>();
            
            ViewBag.user = sameSeeker;
            return View();
        }




        /*  protected override void Dispose(bool disposing)
          {
              if (disposing)
              {
                  db.Dispose();
              }
              base.Dispose(disposing);
          }*/
    }
}

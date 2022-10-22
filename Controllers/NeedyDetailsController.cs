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
    public class NeedyDetailsController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();

        // GET: NeedyDetails
        public ActionResult Index()
        {
            return View(db.NeedyDetails.ToList());
        }

        // GET: NeedyDetails/Details/5
    /*    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedyDetail needyDetail = db.NeedyDetails.Find(id);
            if (needyDetail == null)
            {
                return HttpNotFound();
            }
            return View(needyDetail);
        }*/

        // GET: NeedyDetails/Create
       /* public ActionResult Create()
        {
            return View();
        }

        // POST: NeedyDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "N_id,N_name,N_mail,N_Password,N_ConfirmPassword,N_dob,N_age,N_annualincome,N_gender,N_address,N_phoneNo,N_nationality,N_occupation")] NeedyDetail needyDetail)
        {
            if (ModelState.IsValid)
            {
                db.NeedyDetails.Add(needyDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(needyDetail);
        }

        // GET: NeedyDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedyDetail needyDetail = db.NeedyDetails.Find(id);
            if (needyDetail == null)
            {
                return HttpNotFound();
            }
            return View(needyDetail);
        }

        // POST: NeedyDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "N_id,N_name,N_mail,N_Password,N_ConfirmPassword,N_dob,N_age,N_annualincome,N_gender,N_address,N_phoneNo,N_nationality,N_occupation")] NeedyDetail needyDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(needyDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(needyDetail);
        }

        // GET: NeedyDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedyDetail needyDetail = db.NeedyDetails.Find(id);
            if (needyDetail == null)
            {
                return HttpNotFound();
            }
            return View(needyDetail);
        }

        // POST: NeedyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NeedyDetail needyDetail = db.NeedyDetails.Find(id);
            db.NeedyDetails.Remove(needyDetail);
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
       */

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public ActionResult SignUp(NeedyDetail user)
        {
            if (ModelState.IsValid)
            {
                

                var isExist = IsEmailExist(user.N_mail);
                if (isExist)
                {
                    
                    ViewBag.Loginfailed = "Email AlreadyExist";
                    return View();

                    
                }
                else
                {


                    db.NeedyDetails.Add(user);
                    db.SaveChanges();
                    Response.Write("<script>alert('Registration successful');</script>");
                    SendActivationEmail(user);

                }

            }
            return View();
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {


            {
                var v = db.NeedyDetails.Where(a => a.N_mail == emailID).FirstOrDefault();
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
                NeedyActivation userActivation = db.NeedyActivations.Where(p => p.ActivationCode == activationCode).FirstOrDefault();
                if (userActivation != null)
                {
                    db.NeedyActivations.Remove(userActivation);
                    db.SaveChanges();
                    ViewBag.Message = "Activation successful.";
                }
            }

            return View();
        }

        private void SendActivationEmail(NeedyDetail user)
        {
            Guid activationCode = Guid.NewGuid();
            // UsersEntities usersEntities = new UsersEntities();
            db.NeedyActivations.Add(new NeedyActivation
            {
                N_Id = user.N_id,
                ActivationCode = activationCode
            });
            db.SaveChanges();

            using (MailMessage mm = new MailMessage("groupprojectara@gmail.com", user.N_mail))
            {
                var verifyUrl = "/NeedyDetails/Activation/" + activationCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                mm.Subject = "Account Activation";
                string body = "Hello " + user.N_name + ",";
                //body += "<br /><br />Please click the following link to activate your account";
                //body += "<br /><a href = '" + string.Format("{0}://{1}/DonorDetails/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
                body += "<br/> Your Account is successfully created.Please click to verify your account" + "<br/> <a href='" + link + "'>" + link + " </a>";
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





























        public ActionResult Group(int? id)
            {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NeedyDetail ap = db.NeedyDetails.Find(id);
            if (ap == null)
            {
                return HttpNotFound();
            }
            List<Appeal> sameSeeker = db.Appeals.Where(a => a.N_id == ap.N_id).ToList<Appeal>();
          


            ViewBag.user = sameSeeker;

            return View();
            }


        


        public ActionResult Login(TempUser TempUser)
        {

            if (ModelState.IsValid)
            {
                var user = db.NeedyDetails.Where(u => u.N_mail.Equals(TempUser.D_mail)
                  && u.N_Password.Equals(TempUser.D_Password)).FirstOrDefault();

                if (user != null)
                {
                    
                    Session["N_Mail"] = user.N_mail;
                    Session["N_id"] = user.N_id;
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    ViewBag.Loginfailed = "User not found or password mismatched";
                    return View();
                    
                }



            }
            return View();
        }

        public ActionResult DashBoard()
        {
            string email = Convert.ToString(Session["N_Mail"]);
           
            var user = db.NeedyDetails.Where(u => u.N_mail.Equals(email)).FirstOrDefault();
            return View(user);

        }



        [HttpGet]
        public ActionResult Appeal()
        {
            return View();
        }

        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Appeal( Appeal user)
        {
            String id = Convert.ToString(Session["N_id"]);
            int nid = Convert.ToInt32(id);

            

            if (ModelState.IsValid)
            {
                
                user.Status = "Pending";
               
                int x = db.Database.ExecuteSqlCommand
                    ("insert into Appeal (N_id,Productname,Quantity,Unit,Story,Status) " +
                    "values('" + nid + "', '" + user.Productname + "', '" + user.Quantity + "'" +
                    ",'" + user.Unit + "','" + user.Story + "','" + user.Status + "')");
                
                db.SaveChanges();
                Response.Write("<script>alert('Appealed successfully');</script>");

            }
            return View();
        }



        public ActionResult RequestNotification()
        {
            String id = Convert.ToString(Session["N_id"]);
            int nid = Convert.ToInt32(id);
            var user = db.Appeals.Where(u => u.N_id==nid).ToList();
            return View(user);
        }



    }
}

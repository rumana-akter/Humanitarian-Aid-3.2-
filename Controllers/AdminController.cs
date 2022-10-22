using HumanitarianAid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumanitarianAid.Controllers
{
    public class AdminController : Controller
    {
        private HumanitarianAidEntities db = new HumanitarianAidEntities();
        // GET: Admin
      /*  public ActionResult Index()
        {
            return View();
        }*/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login(TempUser TempUser)
        {

            if (ModelState.IsValid)
            {
              
                if (TempUser.D_mail=="m@gmail.com" && TempUser.D_Password=="1234567")
                {
                    
                    db.SaveChanges();
                    Session["D_Mail"] = TempUser.D_mail;
                 
                    return RedirectToAction("AdminInfo");
                }
                else
                {
                    ViewBag.Loginfailed = "User not found or password mismatched";
                    return View();
                    
                }
            }
            return View();
        }

        public ActionResult AdminInfo()
        {
            List<Admin> admins = new List<Admin>() {
            new Admin()
            {


                // Admin_Id = 1,
                A_name = "Rumana Akter",
                A_mail = "rumananipa143@gmail.com",
                A_pass = "qwert",
                A_address = "Dhanmondi Dhaka",
                A_gender = "Female",
               A_phoneNo = 0177777777,
               // Admin_Password = "qwerty"
            },
              new Admin()
            {

                  A_name = "Asifur Rahim",
                A_mail = "mohammmadasifurrahim@gmail.com",
                A_pass = "1234567",
                A_address = "Md.pur Dhaka",
                A_gender = "Male",
               A_phoneNo = 01621714558,
            },

                new Admin()
            {

                 A_name = "Ayesha Afroze",
                A_mail = "ayeshaaust@gmail.com",
                A_pass = "qwert",
                A_address = "Housing Dhaka",
                A_gender = "Female",
               A_phoneNo = 0177777777,


            },

         };

            

            return View(admins);
        }



        public ActionResult record()
        {
            return View(db.Records1.ToList());
        }

        public ActionResult Storage()
        {
            String stat = "Approved";


           
             var temp = db.Donations.Where(s =>s.Status == stat).ToList<Donation>();
            
            
            ViewBag.user = temp;
            return View();
        }

           public ActionResult Storagefordonor()
        {
            String stat = "Approved";


            
             var temp = db.Donations.Where(s =>s.Status == stat).ToList<Donation>();
            
            ViewBag.user = temp;
            return View();
        }

           public ActionResult Storageforneedy()
        {
            String stat = "Approved";


          
             var temp = db.Donations.Where(s =>s.Status == stat).ToList<Donation>();
          
            ViewBag.user = temp;
            return View();
        }


        //from donation table more
        public ActionResult More(int? id)
        {
            Donation ap = db.Donations.Find(id);


            String query = $"select DonorDetails. * , Donation. *  FROM DonorDetails FULL JOIN Donation ON DonorDetails.D_id=Donation.D_id WHERE Donation.DonationId={id}";
            var d = db.Database.SqlQuery<Donation>(query).ToList();
            var result = (from a in d
                          join c in db.DonorDetails on a.D_id equals c.D_id
                          select new viewmodel
                          {
                              DonationId = a.DonationId,
                              ProductName = a.Productname,
                              DonorName = c.D_name,
                              Address = c.D_address,
                              PhoneNo = c.D_phoneNo
                          }).ToList();



            
            ViewBag.user = result;
            
            return View();

        }

        //from records more

        public ActionResult Info(int? id)
        {
            Records1 rec = db.Records1.Find(id);
            String query = $"select DonorDetails.* , NeedyDetails.* , Records1.* FROM Records1  FULL JOIN DonorDetails ON Records1.D_ID= DonorDetails.D_id  FULL JOIN NeedyDetails ON Records1.N_id = NeedyDetails.N_id  Where Records1.SL ={ id}   ";
            var record = db.Database.SqlQuery<Records1>(query).ToList();

            var result = (from a in record
                          join b in db.DonorDetails on a.D_ID equals b.D_id
                          join c in db.NeedyDetails on a.N_id equals c.N_id
                          select new viewmodel2
                          {
                              ProductName = a.Productname,
                              Quantity = a.Quantity,
                              DonorName = b.D_name,
                              DonorAddress = b.D_address,
                              DonorPhoneNo = b.D_phoneNo,
                              SeekerName = c.N_name,
                              SeekerAddress = c.N_address,
                              SeekerPhoneNo = c.N_phoneNo

                          }).ToList();

            ViewBag.user = result;
            return View();
        }



        //remove from storage
        public ActionResult Remove(int? id)
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


        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
            db.SaveChanges();
            return RedirectToAction("Storage");
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
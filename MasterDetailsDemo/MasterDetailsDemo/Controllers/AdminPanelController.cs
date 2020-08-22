using MasterDetailsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MasterDetailsDemo.Controllers
{
    public class AdminPanelController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();
        // GET: AdminPanel
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Models.AdminUser Stu, string ReturnUrl = "")
        {
            string message = "";
            //if (!Pat.IsEmaiVerified)
            //{
            //    ViewBag.Message = "Please verify your email first";
            //    return View();
            //}


            var login = db.AdminUsers.Where(e => e.UserName == Stu.UserName).FirstOrDefault();


            //if (login.Count <= 0)
            //{
            //    //System.Threading.Thread.Sleep(4000);
            //    Session["AdminMsg"] = "Invalid User Name or Password";
            //    System.Threading.Thread.Sleep(10000);
            //    return View(Stu);
            //}

            if (login != null)
            {
                if (string.Compare(Crypto.Hash(Stu.Password), login.Password) == 0)
                {
                    //int timeout =/* Stu.RememberMe ?*/ 525600 : 20; // 525600 min = 1 year
                    //var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                    //string encrypted = FormsAuthentication.Encrypt(ticket);
                    //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    //cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    //cookie.HttpOnly = true;
                    //Response.Cookies.Add(cookie);


                 
                    System.Threading.Thread.Sleep(5000);
                    //StudentRoll
                    Session["AdminName"] = login.UserName;

                    Session["AdminLogin"] = "True";
                    return RedirectToAction("Index", "Orders");

                }
                else
                {
                    message = "";
                }

            }
            else
            {
                System.Threading.Thread.Sleep(10000);
                message = "";
            }
            //SqlConnection con = null;
            //SqlCommand cmd = null;
            //try
            //{
            //    con = new SqlConnection();
            //    con.ConnectionString = @"Data Source=SQL5020.site4now.net;Initial Catalog=DB_A3F778_school;User Id=DB_A3F778_school_admin;Password=12@Arshuvocse;";
            //    cmd = new SqlCommand();
            //    cmd.Connection = con;
            //    con.Open();
            //    cmd.CommandText = "Update AdminInfo Set Attempt=Attempt+1";
            //    //cmd.CommandText = "insert into ClientIp(IpAddress, Date, Country) values('" + Request.UserHostAddress+"','"+DateTime.Now+"','"+ RegionInfo.CurrentRegion.DisplayName + "')";



            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    if (con.State.Equals(ConnectionState.Open)) con.Close();
            //}

            //if (db.AdminInfoes.Sum(d => d.Attempt) >= 10)
            //{
            //    SendVerificationLinkEmailFotAlert();
            //    System.Threading.Thread.Sleep(90000);
            //}

            ViewBag.Message = message;
            return View();

        }

        public ActionResult AdminLogOut()
        {
            Session["AdminName"] = new Models.AdminUser();
            Session["AdminLogin"] = "";
            Session["AdminMsg"] = "";
            return RedirectToAction("Login", "AdminPanel");
        }

        public ActionResult AdminSignUp()
        {
            if (Session["AdminLogin"].ToString() != "")
            {
                if (db.AdminUsers.Count() >= 5)
                {
                return RedirectToAction("Login", "AdminPanel");
            }

                return View();
            }

            return RedirectToAction("Login", "AdminPanel");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminSignUp([Bind(Include = "Id,UserName,Password")] AdminUser adminInfo)
        {
            //if (Session["AdminLogin"].ToString() != "")
            //{
                if (ModelState.IsValid)
                {
                    #region Generate Activation Code 




                    #endregion

                    adminInfo.Password = Crypto.Hash(adminInfo.Password);
                    db.AdminUsers.Add(adminInfo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(adminInfo);
            //}


            //return RedirectToAction("Index", "Home");

        }


        public ActionResult Index()
        {
            if (Session["AdminLogin"].ToString() != "")
            {
                return View(db.AdminUsers.ToList());
            }

            return RedirectToAction("Login", "AdminPanel");
        }


        // GET: AdminInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminLogin"].ToString() != "")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
               AdminUser adminInfo = db.AdminUsers.Find(id);
                if (adminInfo == null)
                {
                    return HttpNotFound();
                }
                return View(adminInfo);
            }

            return RedirectToAction("Login", "AdminPanel");
        }

        // POST: AdminInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminLogin"].ToString() != "")
            {
                AdminUser adminInfo = db.AdminUsers.Find(id);
                db.AdminUsers.Remove(adminInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Login", "AdminPanel");
        }
    }
}
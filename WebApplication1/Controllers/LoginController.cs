using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [System.Web.Mvc.HttpPost]
        public ActionResult Index(User user)
        {
            string passEncode = EncodePassword(user.Password);
            int count = new Model1().Users.Where(x => x.Account == user.Account && x.Password == passEncode).Count();
            if (count == 1)
            {
                FormsAuthentication.SetAuthCookie(user.Account, false);
                Session["login"] = user.Account;
                return RedirectToAction("ForecastGHI", "Home");
            }
            ViewBag.LoginError = "Account or Password is incorrect";
            return View(user);
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [System.Web.Mvc.HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult CreateAccount(User user)
        {
            if (ModelState.IsValid)
            {
                Model1 db = new Model1();
                user.Password = EncodePassword(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public static string EncodePassword(string password)
        {
            MD5 md5Hash = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
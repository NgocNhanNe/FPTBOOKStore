using FPTBook.DB;
using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class UserController : Controller
    {
       private MyApplicationDbContext _db = new MyApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        //GET: Register

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.UserName == user.UserName);
                if (check == null)
                {
                   user.Password = GetMD5(user.Password);
                   user.ConfirmPassword = GetMD5(user.ConfirmPassword);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    user.state = 0;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ViewBag.error = "User already exists";
                    return View();
                }


            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
               var Password = GetMD5(password);
                var data = _db.Users.Where(s => s.UserName.Equals(username) && s.Password.Equals(Password)).ToList();
                if (data.Count() > 0)
                {
                    if (data.FirstOrDefault().state == 0)
                    {
                        Session["UserName"] = data.FirstOrDefault().UserName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["UserAdmin"] = data.FirstOrDefault().UserName;
                        return RedirectToAction("Index", "Admin");
                    }

                }
                else
                {
                    ViewBag.ErrorMessage = "User name and Password wrong";
                }
            }
            return View();
        }
        public ActionResult EditInfor()
        {
            var user = Session["UserName"];
            User obj = _db.Users.ToList().Find(x => x.UserName.Equals(user));
            if (obj == null)
            {
                return RedirectToAction("Login");
            }
            
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfor(User obj)
        {
            if (ModelState.IsValid)
            {
                User tmp = _db.Users.ToList().Find(x => x.UserName == obj.UserName);
                if (tmp.Password != obj.Password)  //if find out the customer
                {
                    tmp.Password = GetMD5(obj.Password);
                    tmp.ConfirmPassword = GetMD5(obj.ConfirmPassword);
                }
                tmp.UserName = obj.UserName;
                tmp.FullName = obj.FullName;
                tmp.Telephone = obj.Telephone;
                tmp.Email = obj.Email;
                tmp.Gender = obj.Gender;
                tmp.Birthday = obj.Birthday;
                tmp.Address = obj.Address;
                tmp.state = obj.state = 0;
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View("EditInFor");
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}
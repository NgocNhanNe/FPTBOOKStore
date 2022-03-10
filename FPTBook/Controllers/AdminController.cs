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
    public class AdminController : Controller
    {
        private MyApplicationDbContext _db = new MyApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {

            var books = _db.Books.ToList();
            if (Session["UserName"] == Session["UserName"] && Session["UserAdmin"]!=null)
            {
                var totalBook = _db.Books.Count();
                var totalCate = _db.Categories.Count();
                var totalUser = _db.Users.Count();
                var totalOrder = _db.Orders.Count();

                ViewBag.TotalBook = totalBook;
                ViewBag.TotalCate = totalCate;
                ViewBag.TotalUser = totalUser;
                ViewBag.TotalOrder = totalOrder;
                return View(books);
            }
            return RedirectToAction("Error");
        }

        public ActionResult Error()
        {
           return View();
        }
        public ActionResult EditInforAdmin()
        {
            var user = Session["UserAdmin"];
            User objadmin = _db.Users.ToList().Find(x => x.UserName.Equals(user));
            if (objadmin == null)
            {
                return HttpNotFound();
            }
            return View(objadmin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInforAdmin(User objadmin)
        {
            if (ModelState.IsValid)
            {
                User tmp = _db.Users.ToList().Find(x => x.UserName == objadmin.UserName);
                if (tmp.Password != objadmin.Password)  //if find out the customer
                {
                    tmp.Password = GetMD5(objadmin.Password);
                    tmp.ConfirmPassword = GetMD5(objadmin.ConfirmPassword);
                }
                tmp.UserName = objadmin.UserName;
                tmp.FullName = objadmin.FullName;
                tmp.Telephone = objadmin.Telephone;
                tmp.Email = objadmin.Email;
                tmp.Gender = objadmin.Gender;
                tmp.Birthday = objadmin.Birthday;
                tmp.Address = objadmin.Address;
                tmp.state = objadmin.state = 1;
                _db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View("EditInforAdmin");
            
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
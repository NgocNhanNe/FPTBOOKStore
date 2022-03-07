using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FPTBook.DB;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class ManageUserController : Controller
    {
        private MyApplicationDbContext db = new MyApplicationDbContext();

        // GET: ManageUser
        public ActionResult Index()
        {
                return View(db.Users.ToList());
        }

        public ActionResult EditInforAdmin()
        {
            var user = Session["UserAdmin"];
            User obj = db.Users.ToList().Find(x => x.UserName.Equals(user));
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInforAdmin(User obj)
        {
            User tmp = db.Users.ToList().Find(x => x.UserName == obj.UserName); //find the customer in a list have the same ID with the ID input
            if (tmp != null)  //if find out the customer
            {
                tmp.UserName = obj.UserName;
                tmp.FullName = obj.FullName;
                tmp.Password = GetMD5(obj.Password);
                tmp.Telephone = obj.Telephone;
                tmp.Email = obj.Email;
                tmp.Birthday = obj.Birthday;
                tmp.Address = obj.Address;
                tmp.ConfirmPassword = GetMD5(obj.ConfirmPassword);
                tmp.state = obj.state = 0;
            }
            db.SaveChanges();
            return View("Index","ManagementUser");
        }

        // GET: ManageUser/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: ManageUser/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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

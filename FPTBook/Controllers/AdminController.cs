using FPTBook.DB;
using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(books);
            //if (Session["UserName"]!=null)
            //{
            //    return View();
            //}
            ////return View("Error");
            //return RedirectToAction("Error");
        }
        [HttpPost]
        public ActionResult Index(string searchstring)
        {
            List<Book> data = new List<Book>();
            data = _db.Books.Where(x => x.BookName.Contains(searchstring)).ToList();
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
        //public ActionResult Error()
        //{
        //    return View();
        //}
    }
}
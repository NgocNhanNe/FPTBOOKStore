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
            if (Session["UserName"] == Session["UserName"] && Session["UserAdmin"]!=null)
            {
                var totalBook = _db.Books.Count();
                var totalCate = _db.Categories.Count();
                var totalUser = _db.Users.Count();

                

                ViewBag.TotalBook = totalBook;
                ViewBag.TotalCate = totalCate;
                ViewBag.TotalUser = totalUser;
                return View(books);
            }
            return RedirectToAction("Error");
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
        public ActionResult Error()
        {
           return View();
        }
    }
}
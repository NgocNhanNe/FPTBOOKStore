using FPTBook.DB;
using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class HomeController : Controller
    {
        private MyApplicationDbContext _db = new MyApplicationDbContext();
        public ActionResult Index()
        {
            var books = _db.Books.ToList();
            return View(books);
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
        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

    }
}
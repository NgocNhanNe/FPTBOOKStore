using FPTBook.DB;
using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class CategoryController : Controller
    {
        private MyApplicationDbContext _db = new MyApplicationDbContext();
        // GET: Category
        public ActionResult Index()
        {
            return View(_db.Books.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book objbook)
        {
            _db.Books.Add(objbook);
            _db.SaveChanges();
            return RedirectToAction("index");
            //return View("Index",customers);
        }
    }
}
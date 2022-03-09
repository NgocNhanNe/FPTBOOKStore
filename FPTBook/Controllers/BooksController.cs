using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTBook.DB;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class BooksController : Controller
    {
        private MyApplicationDbContext db = new MyApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Category);
            return View(books.ToList());
        }

        // GET: Books/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.Cat_ID = new SelectList(db.Categories, "Cat_ID", "CatName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Book_ID,BookName,Quantity,Img,Price,Description,Cat_ID")] Book book, HttpPostedFileBase file)
        {
            string pic = System.IO.Path.GetFileName(file.FileName);
            string checkImg = Path.GetExtension(file.FileName);
            if (checkImg.ToLower() == ".jpg" || checkImg.ToLower() == ".jpeg" || checkImg.ToLower() == ".png" && file != null && file.ContentLength>0)
            {
                string path = Path.Combine(Server.MapPath("~/assets/img"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                book.Img = pic.ToString();
                if (ModelState.IsValid)
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.CheckError = "*Invavlid file";
            }
            ViewBag.Cat_ID = new SelectList(db.Categories, "Cat_ID", "CatName", book.Cat_ID);
            return View(book);
        }

        // GET: Books/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_ID = new SelectList(db.Categories, "Cat_ID", "CatName", book.Cat_ID);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Book_ID,BookName,Quantity,Img,Price,Description,Cat_ID")] Book book, HttpPostedFileBase file, int id)
        {
            if (ModelState.IsValid)
            {
                Book rebook = db.Books.Find(id);
                if (file != null && file.ContentLength > 0)
                {

                    string pic = "";
                    string file_name = book.Img;
                    string path1 = Server.MapPath("~/assets/img/");
                    string checkimg = Path.GetExtension(file.FileName);
                    if (checkimg.ToLower() == ".jpg" || checkimg.ToLower() == ".jpeg" || checkimg.ToLower() == ".png")
                    {
                        FileInfo file1 = new FileInfo(path1 + file_name);
                        if (file1.Exists)
                        {
                            file1.Delete();
                        }
                        pic = System.IO.Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/assets/img/"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        rebook.Img = pic.ToString();
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.CheckError = "*Invavlid file";
                    }
                }
                else
                {
                    rebook.BookName = book.BookName;
                    rebook.Quantity = book.Quantity;
                    rebook.Price = book.Price;
                    rebook.Description = book.Description;
                    rebook.Cat_ID = book.Cat_ID;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            ViewBag.Cat_ID = new SelectList(db.Categories, "Cat_ID", "CatName", book.Cat_ID);
            return View(book);
        }

        // GET: Books/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
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

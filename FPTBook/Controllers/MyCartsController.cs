using FPTBook.DB;
using FPTBook.Models;
using FPTBook.Models.FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class MyCartsController : Controller
    {
        // GET: MyCarts
        private MyApplicationDbContext _db = new MyApplicationDbContext();
        //GET: ShoppingCart
        public MyCart GetCart()
        {
            MyCart cart = Session["Cart"] as MyCart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new MyCart();
                Session["Cart"] = cart;
            }
            return cart;

        }

        public ActionResult AddtoCart(int id)
        {
            var book = _db.Books.SingleOrDefault(s => s.Book_ID == id);
            if (book != null)
            {
                GetCart().AddtoCart(book);
            }
            return RedirectToAction("ViewCart", "MyCarts");
        }

        public ActionResult UpdateQuantity(FormCollection form)
        {
            MyCart cart = Session["Cart"] as MyCart;
            int id_book = int.Parse(form["Book_ID"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Book(id_book, quantity);
            return RedirectToAction("ViewCart", "MyCarts");
        }

        public ActionResult ViewCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ViewCart", "MyCarts");
            MyCart cart = Session["Cart"] as MyCart;
            return View(cart);

        }
        public ActionResult Delete(int id)
        {
            MyCart cart = Session["Cart"] as MyCart;
            cart.DeleteCart(id);
            return RedirectToAction("ViewCart", "MyCarts");
        }
        public PartialViewResult NumberCart()
        {
            int total_item = 0;

            MyCart cart = Session["Cart"] as MyCart;

            if (cart != null)
                total_item = cart.Total();
            ViewBag.TotalItem = total_item;

            return PartialView("NumberCart");
        }
        public ActionResult Checkout(FormCollection form)
        {
            try
            {
                MyCart cart = Session["Cart"] as MyCart;
                Order _order = new Order();
                _order.Order_Date = DateTime.Now;
                _order.Username = form["Username"];
                _order.Address_Delivery = form["Address_Delivery"];
                _order.Phone_Delivery = Convert.ToInt32(form["Phone_Delivery"]);
                _order.Total = Convert.ToInt32(form["Total"]);
                _db.Orders.Add(_order);

                foreach (var item in cart.Items)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.Order_ID = _order.Order_ID;
                    orderDetail.Book_ID = item._cart_book.Book_ID;
                    orderDetail.Quantity = item._cart_quantity;
                    orderDetail.Amount = item._cart_book.Price * item._cart_quantity;

                    var book = _db.Books.SingleOrDefault(s => s.Book_ID == orderDetail.Book_ID);

                    book.Quantity -= orderDetail.Quantity;
                    _db.Books.Attach(book);
                    _db.Entry(book).Property(a => a.Quantity).IsModified = true;

                    _db.OrderDetails.Add(orderDetail);
                }

                _db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("Checkout_Success", "MyCarts", new { id = _order.Order_ID });
            }
            catch
            {
                return Content("Error checkout, Check information again");
            }
        }
        public ActionResult Checkout_Success(int? id)
        {
            if (Session["Username"] != null)
            {
                var order = _db.Orders.Find(id);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return View("ErrorCart");
        }

        public ActionResult OrderHistory(string id)
        {
            if (Session["Username"] != null)
            {
                var orderHis = _db.Orders.ToList().Where(s => s.Username == id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (orderHis == null)
                {
                    return HttpNotFound();
                }
                return View(orderHis);
            }
            return View("ErrorCart");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPTBook.DB;
using FPTBook.Models;

namespace FPTBook.Controllers
{
    public class OrderDetailsController : Controller
    {
        private MyApplicationDbContext db = new MyApplicationDbContext();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Book).Include(o => o.Order);
            return View(orderDetails.ToList());
        }

    }
}

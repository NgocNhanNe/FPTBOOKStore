using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [Required(ErrorMessage = "Enter Order ID,please!")]
        public int Order_ID { get; set; }
        [Required(ErrorMessage = "Enter choose Order Date,please!")]
        //[DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime Order_Date { get; set; }
        public int totalPrice { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter address to delivery,please!")]
        public string Address_Delivery { get; set; }
        [Required (ErrorMessage = "Enter telephone to delivery,please!")]
        public int Phone_Delivery { get; set; }
        public virtual User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
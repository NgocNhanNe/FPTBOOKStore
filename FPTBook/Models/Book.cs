using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class Book
    {
        public Book()
        {
                OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int Book_ID { get; set; }
        [Required(ErrorMessage = "Enter Book Name,please!")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Enter Quantity ,please!")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Enter Price,please!")]
        public int Price { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public int Cat_ID { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
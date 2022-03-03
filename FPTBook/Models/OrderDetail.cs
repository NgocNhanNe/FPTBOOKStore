using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order=0)]
        public int Book_ID { get; set; }
        [Key]
        [Column(Order = 1)]
        public int Order_ID { get; set; }

        [Required(ErrorMessage = "Enter Quantity,please!")]
        [Range(0, 500, ErrorMessage = "Please in input positive number")]
        public int Quantity { get; set; }
        public int Amount { get; set; }
        public int  Price {get;set;}
        
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }

    }
}
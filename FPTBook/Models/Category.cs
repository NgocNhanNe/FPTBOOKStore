using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        [Key]
        [Required(ErrorMessage = "Enter Category ID,please!")]
        public int Cat_ID { get; set; }
        [Required(ErrorMessage = "Enter Category Name,please!")]
        public string CatName { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
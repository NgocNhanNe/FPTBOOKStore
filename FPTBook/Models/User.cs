using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FPTBook.Models
{
    
    public class User
    {
        public User()
        {            
            Orders = new HashSet<Order>();
        }
        [Key]
        [Required(ErrorMessage ="Enter Username,please!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password,please!")]
        [MinLength(8)]
        public string Password { get; set; }
        [MinLength(8)]
        [Required(ErrorMessage = "Enter Password again,please!")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Enter FullName,please!")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Enter Email,please!")]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Address,please!")]
        [StringLength(200)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter Telephone,please!")]
        public int Telephone { get; set; }
        [Required(ErrorMessage = "Enter choose your birthday,please!")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Enter choose Gender,please!")]
        public string Gender { get; set; }
        public int state { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
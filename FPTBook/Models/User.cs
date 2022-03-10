using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
      

        [MinLength(8)]
        public string Password { get; set; }


        [MinLength(8)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Enter FullName,please!")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Enter Email,please!")]
        [EmailAddress]
        [Display(Name ="Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email")]
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

        [NotMapped]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Enter Current Password again,please!")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Enter New Password again,please!")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [NotMapped]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Enter Confirm Password again,please!")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation new password do not match.")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }


        public int state { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
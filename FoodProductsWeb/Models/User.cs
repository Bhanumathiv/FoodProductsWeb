using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodProductsWeb.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is required and must be properly formatted.")]
        public string Email { get; set; }
    }
}

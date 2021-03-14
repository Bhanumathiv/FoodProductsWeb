using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodProductsWeb.Models
{
    public class CurrentProduct
    {
        [Key]
        public int PId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Should be greater than or equal to 1")]
        public decimal Price { get; set; }
        [Required]
        public int availability { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodProductsWeb.Models;

namespace FoodProductsWeb.Data
{
    public class FoodProductsWebContext : DbContext
    {
        public FoodProductsWebContext (DbContextOptions<FoodProductsWebContext> options)
            : base(options)
        {
        }

        public DbSet<FoodProductsWeb.Models.CurrentProduct> CurrentProduct { get; set; }

        public DbSet<FoodProductsWeb.Models.NewProduct> NewProduct { get; set; }

        public DbSet<FoodProductsWeb.Models.User> User { get; set; }
    }
}

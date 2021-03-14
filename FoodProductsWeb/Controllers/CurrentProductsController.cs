using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodProductsWeb.Data;
using FoodProductsWeb.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FoodProductsWeb.Controllers
{
    public class CurrentProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public CurrentProductsController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
                
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("Viewallproduct", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return View(dt);
        }
        
      
        // GET: CurrentProducts/AddorEdit/
        public IActionResult AddorEdit(int? id)
        {
            CurrentProduct cp = new CurrentProduct();
            if(id>0)
            {
                cp = fetchproductbyid(id);
            }
            return View(cp);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult AddorEdit(int id, [Bind("PId,ProductName,Price,availability")] CurrentProduct currentProduct)
        {

            Console.WriteLine(currentProduct.PId);
            if (ModelState.IsValid)
            {
               using(SqlConnection con=new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    con.Open();
                    SqlCommand sqlcmd = new SqlCommand("AddEditProducts", con);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("PId", currentProduct.PId);
                    sqlcmd.Parameters.AddWithValue("ProductName", currentProduct.ProductName);
                    sqlcmd.Parameters.AddWithValue("Price", currentProduct.Price);
                    sqlcmd.Parameters.AddWithValue("availability", currentProduct.availability);
                    sqlcmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(currentProduct);
        }

        // GET: CurrentProducts/Delete/5
        public  IActionResult Delete(int? id)
        {
            CurrentProduct cp1 = fetchproductbyid(id);


            return View(cp1);
        }

        // POST: CurrentProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                con.Open();
                SqlCommand sqlcmd = new SqlCommand("DeleteProductbyID", con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("PId",id);

                sqlcmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public CurrentProduct fetchproductbyid(int?id)
        {
            CurrentProduct currentproduct = new CurrentProduct();
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    DataTable dtbl = new DataTable();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("ProductviewbyID", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("PId", id);
                   
                    da.Fill(dtbl);
                    if(dtbl.Rows.Count==1)
                    {
                        currentproduct.PId =Convert.ToInt32( dtbl.Rows[0]["PId"].ToString());
                        currentproduct.ProductName = dtbl.Rows[0]["ProductName"].ToString();
                        currentproduct.Price = Convert.ToInt32(dtbl.Rows[0]["Price"].ToString());
                    }
                    return currentproduct;
                }
            }

        }
    }
}

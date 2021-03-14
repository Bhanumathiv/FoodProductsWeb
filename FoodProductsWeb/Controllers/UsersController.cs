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
    public class UsersController : Controller
    {
                private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("ViewAllUser", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return View(dt);

        }




        public IActionResult AddorEdit(int? id)
        {
            User user = new User();
            if (id > 0)
            {
                user = fetchproductbyid(id);
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddorEdit(int id, [Bind("UserId,UserName,Email")] User user)
        {

            Console.WriteLine(user.UserId);
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    con.Open();
                    SqlCommand sqlcmd = new SqlCommand("AddEditUsers", con);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("UserId", user.UserId);
                    sqlcmd.Parameters.AddWithValue("UserName", user.UserName);
                    sqlcmd.Parameters.AddWithValue("Email", user.Email);
                    sqlcmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        // GET: CurrentProducts/Delete/5
        public IActionResult Delete(int? id)
        {
            User u = fetchproductbyid(id);


            return View(u);
        }

        // POST: CurrentProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                con.Open();
                SqlCommand sqlcmd = new SqlCommand("DeleteUserByID", con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("UserId", id);

                sqlcmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public User fetchproductbyid(int? id)
        {
            User user = new User();
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    DataTable dtbl = new DataTable();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("UserByID", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("UserId", id);

                    da.Fill(dtbl);
                    if (dtbl.Rows.Count == 1)
                    {
                        user.UserId = Convert.ToInt32(dtbl.Rows[0]["UserId"].ToString());
                        user.UserName = dtbl.Rows[0]["UserName"].ToString();
                        user.Email = dtbl.Rows[0]["Email"].ToString();
                    }
                    return user;
                }
            }

        }

    }
}

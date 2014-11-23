using MvcLogin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLogin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (UserDBEntities dc = new UserDBEntities())
                {
                    if (ValidateUser(u.Username, u.Password))
                    {
                        Session["LogedUsername"] = u.Username.ToString();
                        return RedirectToAction("AfterLogin", "Page");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username or Password!");
                    }
                }
            }
            return View(u);
        }


        public bool ValidateUser(string username, string password)
        {
            string query = string.Format("SELECT * FROM Users WHERE Username = '{0}' AND Password = '{1}'", username, password);
            string connectionString = @"Data Source=tcp:rlju0ybi7n.database.windows.net,1433;Initial Catalog=UserDB;User Id=deepan@rlju0ybi7n;Password=Dee43211234"; 
            SqlConnection connection = new SqlConnection(connectionString); 
            SqlCommand cmd = new SqlCommand(query, connection); 
            bool isValid = false;
            try
            { 
                connection.Open(); 
                SqlDataReader dataReader = cmd.ExecuteReader(); 
                isValid = dataReader.HasRows; 
                connection.Close(); 
            }
            catch (Exception e) 
            { 
                Console.WriteLine(e.Message); 
            }
            return isValid;
        }
   


    }
}

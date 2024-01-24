using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Stock_Invoice.Models;

namespace Stock_Invoice.Controllers
{
    public class EntryController : Controller
    {
        public IConfiguration Configuration { get; }
        public EntryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserDetail userDetail)
        {


            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (var con = new SqlConnection(connectionString))
            {

                //     string sql = $"Insert Into NewTable (acc_no, pwd) Values ('{inventory.AccNo}', '{inventory.Pwd}')";

                using (var cmd = new SqlCommand("select dbo.userlogincheck(@username,@userid)", con))
                {
                    cmd.Parameters.AddWithValue("@username", userDetail.Username);
                    cmd.Parameters.AddWithValue("@userid", userDetail.Pwd);


                    con.Open();
                    int c = Convert.ToInt32(cmd.ExecuteScalar());

                    if (c == 1)
                    {

                        Console.WriteLine("welcome User");
                        return RedirectToAction("Index", "Stock_Invoice");
                        

                    }
                    else
                    {
                        Console.WriteLine("Account No or Password is wrong!!!");


                    }
                }

            }
            return View(userDetail);
        }
    }
}

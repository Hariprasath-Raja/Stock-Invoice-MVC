using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Stock_Invoice.Models;
using System.Data;

namespace Stock_Invoice.Controllers
{
    public class Stock_Invoice : Controller
    {
        public IConfiguration Configuration { get; }
        public Stock_Invoice(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ProductItem> ProductItemList = new List<ProductItem>();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("fetchDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductItem productItem = new ProductItem();

                            productItem.PId = Convert.ToInt32(reader["p_id"]);
                            productItem.PName = Convert.ToString(reader["p_name"]);
                            productItem.Cgst = Convert.ToDecimal(reader["cgst"]);
                            productItem.Sgst = Convert.ToDecimal(reader["sgst"]);
                            productItem.PPrice = Convert.ToInt32(reader["p_price"]);
                            productItem.PQuantity = Convert.ToInt32(reader["p_quantity"]);
                            productItem.ExpDate = Convert.ToDateTime(reader["Exp_Date"]);

                            ProductItemList.Add(productItem);


                        }

                    }

                    con.Close();
                }


            }


            return View(ProductItemList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductItem productItem)
        {

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("insert_product", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_name", productItem.PName);
                    cmd.Parameters.AddWithValue("@cgst", productItem.Cgst);
                    cmd.Parameters.AddWithValue("@sgst", productItem.Sgst);
                    cmd.Parameters.AddWithValue("@p_price", productItem.PPrice);
                    cmd.Parameters.AddWithValue("@p_quantity", productItem.PQuantity);
                    cmd.Parameters.AddWithValue("@Exp_Date", productItem.ExpDate);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    con.Close();




                }
            }
            ViewBag.Result = "Success";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ProductItem productItem = new ProductItem();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from productItems where p_id='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        productItem.PName = Convert.ToString(reader["p_name"]);
                        productItem.Cgst = Convert.ToDecimal(reader["cgst"]);
                        productItem.Sgst = Convert.ToDecimal(reader["sgst"]);
                        productItem.PPrice = Convert.ToInt32(reader["p_price"]);
                        productItem.PQuantity = Convert.ToInt32(reader["p_quantity"]);
                        productItem.ExpDate = Convert.ToDateTime(reader["Exp_Date"]);
                    }
                    connection.Close();
                }

            }
            return View(productItem);
        }


        [HttpPost]
        public IActionResult Delete(ProductItem productItem, int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete from productItems where p_id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ProductItem productItem = new ProductItem();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from productItems where p_id='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        productItem.PName = Convert.ToString(reader["p_name"]);
                        productItem.Cgst = Convert.ToDecimal(reader["cgst"]);
                        productItem.Sgst = Convert.ToDecimal(reader["sgst"]);
                        productItem.PPrice = Convert.ToInt32(reader["p_price"]);
                        productItem.PQuantity = Convert.ToInt32(reader["p_quantity"]);
                        productItem.ExpDate = Convert.ToDateTime(reader["Exp_Date"]);
                    }
                    connection.Close();
                }

            }
            return View(productItem);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProductItem productItem = new ProductItem();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from productItems where p_id='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string format = "MM/dd/yyyy";

                        productItem.PName = Convert.ToString(reader["p_name"]);
                        productItem.Cgst = Convert.ToDecimal(reader["cgst"]);
                        productItem.Sgst = Convert.ToDecimal(reader["sgst"]);
                        productItem.PPrice = Convert.ToInt32(reader["p_price"]);
                        productItem.PQuantity = Convert.ToInt32(reader["p_quantity"]);
                        productItem.ExpDate = Convert.ToDateTime(reader["Exp_Date"]);
                    }
                    connection.Close();
                }

            }
            return View(productItem);

        }
        [HttpPost]
        public IActionResult Edit(ProductItem productitem, int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"update productItems set p_name='{productitem.PName}',cgst='{productitem.Cgst}',sgst='{productitem.Sgst}',p_price='{productitem.PPrice}',p_quantity='{productitem.PQuantity}',Exp_Date='{productitem.ExpDate}.toDate('mm-dd-yyyy')'where p_id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }
    }

     
}

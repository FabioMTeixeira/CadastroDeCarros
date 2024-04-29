using CadastroDeCarros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.SqlClient;
using System.Drawing;
using System.Xml.Linq;

namespace CadastroDeCarros.Controllers
{
    public class CarController : Controller
    {
        public ActionResult Index()
        {
            List<ColorViewModel> colors = new List<ColorViewModel>();
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                connection.Open();

                string sql = "SELECT * FROM Color";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ColorViewModel color = new ColorViewModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Descricao = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                            };

                            colors.Add(color);
                        }
                    }
                }
            }

            return View(colors);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {

            List<ColorViewModel> colors = new List<ColorViewModel>();

            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql;

                if (string.IsNullOrEmpty(search))
                {
                    sql = "SELECT * FROM Color";
                }
                else if (search.ToLower() == "ativo" || search.ToLower() == "inativo")
                {
                    sql = "SELECT * FROM Color WHERE Status = @Search";
                }
                else
                {
                    sql = "SELECT * FROM Color WHERE Description LIKE @Search";
                    search = "%" + search + "%";
                }

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        command.Parameters.AddWithValue("@Search", search);
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ColorViewModel color = new ColorViewModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Descricao = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                            };

                            colors.Add(color);
                        }
                    }
                }
            }

            return View(colors);
        }

        public ActionResult Create()
        {
            ViewBag.Colors = TakeColors().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Descricao,
            })
            .ToList();
            ViewBag.Fuels = TakeFuels().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Descricao,
            })
            .ToList();
            ViewBag.StatusList = new SelectList(new List<string>() { "Ativo", "Inativo" });
            return View();
        }

        [HttpPost]
        public ActionResult Create(CarViewModel model)
        {
            if (ModelState.IsValid)
            {
                InsertCar(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }



        private List<ColorViewModel> TakeColors()
        {
            List<ColorViewModel> colors = new List<ColorViewModel>();
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                connection.Open();

                string sql = "SELECT * FROM Color";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ColorViewModel color = new ColorViewModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Descricao = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                            };

                            colors.Add(color);
                        }
                    }
                }
            }

            return colors;
        }

        private List<FuelViewModel> TakeFuels()
        {
            List<FuelViewModel> fuels = new List<FuelViewModel>();
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))

            {
                connection.Open();

                string sql = "SELECT * FROM Fuel";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FuelViewModel fuel = new FuelViewModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Descricao = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                            };

                            fuels.Add(fuel);
                        }
                    }
                }
            }

            return fuels;
        }

        private static void InsertCar(CarViewModel model)
        {
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Car (Plate, Renavam,ChassisNumber,EngineNumber,brand,Model,FuelId,ColorId,YearFactory,Status) VALUES (@Plate,@Renavam,@ChassisNumber,@EngineNumber,@brand,@Model, @FuelId,@ColorId,@YearFactory,@Status)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Plate", model.Plate);
                    command.Parameters.AddWithValue("@Renavam", model.Renavam);
                    command.Parameters.AddWithValue("@ChassisNumber", model.ChassisNumber);
                    command.Parameters.AddWithValue("@EngineNumber", model.EngineNumber);
                    command.Parameters.AddWithValue("@brand", model.Brand);
                    command.Parameters.AddWithValue("@Model", model.Model);
                    command.Parameters.AddWithValue("@ColorId", model.ColorId);
                    command.Parameters.AddWithValue("@FuelId", model.FuelId);
                    command.Parameters.AddWithValue("@YearFactory", model.YearFactory);
                    command.Parameters.AddWithValue("@Status", model.Status);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}


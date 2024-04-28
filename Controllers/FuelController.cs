using CadastroDeCarros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.SqlClient;

namespace CadastroDeCarros.Controllers
{
    public class FuelController : Controller
    {
        public ActionResult Index()
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
                                Descricao = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                            };

                            fuels.Add(fuel);
                        }
                    }
                }
            }

            return View(fuels);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {
            List<FuelViewModel> fuels = new List<FuelViewModel>();

            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql;

                if (string.IsNullOrEmpty(search))
                {
                    sql = "SELECT * FROM Fuel";
                }
                else if (search.ToLower() == "ativo" || search.ToLower() == "inativo")
                {
                    sql = "SELECT * FROM Fuel WHERE Status = @Search";
                }
                else
                {
                    sql = "SELECT * FROM Fuel WHERE Description LIKE @Search";
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
                            FuelViewModel fuel = new()
                            {
                                Descricao = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                            };

                            fuels.Add(fuel);
                        }
                    }
                }
            }

            return View(fuels);
        }



        public ActionResult Create()
        {
            ViewBag.StatusList = new SelectList(new List<string>() { "Ativo", "Inativo" });
            return View();
        }

        [HttpPost]
        public ActionResult Create(FuelViewModel model)
        {
            if (model.CheckIfExists(model.Descricao))
            {
                ModelState.AddModelError("Descricao", "A descrição já foi usada");
                ViewBag.StatusList = new SelectList(new List<string>() { "Ativo", "Inativo" });
                return View(model);
            }

            if (ModelState.IsValid)
            {
                string connectionString ;
                SqlConnection conn;
                connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
                conn = new SqlConnection(connectionString);
                {
                    string sql = "INSERT INTO Fuel (Description, Status) VALUES (@Description, @Status)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Description", model.Descricao);
                        cmd.Parameters.AddWithValue("@Status", model.Status);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}

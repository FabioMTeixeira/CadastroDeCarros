using CadastroDeCarros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.SqlClient;

namespace CadastroDeCarros.Controllers
{
    public class ColorController : Controller
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
            ViewBag.StatusList = new SelectList(new List<string>() { "Ativo", "Inativo" });
            return View();
        }

        [HttpPost]
        public ActionResult Create(ColorViewModel model)
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
                    string sql = "INSERT INTO Color (Description, Status) VALUES (@Description, @Status)";
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
        public ActionResult Edit(int id)
        {
            ColorViewModel item = null;
            ViewBag.StatusList = new SelectList(new List<string>() { "Ativo", "Inativo" });
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
        
                string sql = "SELECT * FROM Color WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new ColorViewModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Descricao = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                            };
                        }
                    }
                }
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(ColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE Color SET Description = @Description, Status = @Status WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Description", model.Descricao);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@Id", model.Id);

                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            ColorViewModel item = null;
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Color WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            item = new ColorViewModel()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Descricao = reader["Description"].ToString(),
                                Status = reader["Status"].ToString(),
                            };
                        }
                    }
                }
            }
            return View(item);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM Color WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }

}


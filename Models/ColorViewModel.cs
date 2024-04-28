
using System.Data.SqlClient;

namespace CadastroDeCarros.Models
{
    public class ColorViewModel
    {
        public int Id { get; set; }
        public required string Descricao { get; set; }
        public required string Status { get; set; }

        public bool CheckIfExists(string descricao)
        {
            bool exists = false;
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT COUNT(*) FROM Color WHERE Description = @Description";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Description", descricao);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        exists = true;
                    }
                }
            }

            return exists;
        }
    }
}

using System.Data.SqlClient;

namespace CadastroDeCarros.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public required string Plate { get; set; }
        public required string Renavam { get; set; }
        public required string ChassisNumber { get; set; }
        public required string EngineNumber { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required int FuelId { get; set; }
        public required int ColorId { get; set; }
        public required int YearFactory { get; set; }
        public required string Status { get; set; }


        public bool CheckingIfExist(string plate, string renavam, string chassisNumber, string engineNumber)
        {
            bool exist = false;
            string connectionString = @"Data Source=PCFABIO\MSSQLSERVER01;Initial Catalog=ShopCar;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Car WHERE Plate = @Plate OR Renavam = @Renavam OR ChassisNumber = @ChassisNumber OR EngineNumber = @EngineNumber";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Plate", plate);
                    command.Parameters.AddWithValue("@Renavam", renavam);
                    command.Parameters.AddWithValue("@ChassisNumber", chassisNumber);
                    command.Parameters.AddWithValue("@EngineNumber", engineNumber);

                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        exist = true;
                    }
                }
            }

            return exist;
        }
    }


}
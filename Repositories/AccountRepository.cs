using Files.Data;
using Files.Models.Entity;
using Files.Models.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Files.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account Login(string username, string password)
        {
            using SqlConnection conn = dbConnect.GetConnection();
            using SqlCommand cmd = new SqlCommand("sp_Login", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@Password", password);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Account
                {
                    ID = reader.GetInt32("ID"),
                    Code = reader.GetString("Code"),
                    UserName = reader.GetString("UserName"),
                    Role = reader.GetString("Role")
                };
            }

            return null!;
        }
    }
}

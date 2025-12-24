using Files.Helpers;
using Microsoft.Data.SqlClient;

namespace Files.Data
{
    public static class dbConnect
    {
        private static string? _connectionString;

        public static bool Initialize(IConfiguration configuration)
        {
            try
            {
                var dataConfig = configuration.GetSection("DataConfig");

                string? server = dataConfig["datalocal"];
                string? database = dataConfig["dataname"];
                string? user = dataConfig["datauser"];
                string? pass = dataConfig["datapass"];

                if (string.IsNullOrEmpty(server) ||
                    string.IsNullOrEmpty(database) ||
                    string.IsNullOrEmpty(user) ||
                    string.IsNullOrEmpty(pass))
                {
                    return false;
                }

                _connectionString =
                    $"Server={server};Database={database};User Id={user};Password={pass};TrustServerCertificate=True;";

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DB INIT ERROR: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Lấy SqlConnection mới (chưa Open)
        /// </summary>
        public static SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                Console.Write(Environment.NewLine);
            }

            return new SqlConnection(_connectionString);
        }
    }
}

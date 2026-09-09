using Microsoft.Data.SqlClient;
using System.Data;

namespace CNMSDataAPI.Services
{
    public class DatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Dictionary<string, object>>> GetCNMSDataAsync(string bizSrc)
        {
            var result = new List<Dictionary<string, object>>();

            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");

            await using SqlConnection connection = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand(
                "Prc_getCNMSData",
                connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@BizSrc", SqlDbType.VarChar).Value = bizSrc;

            await connection.OpenAsync();

            await using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] =
                        reader.IsDBNull(i) ? null! : reader.GetValue(i);
                }

                result.Add(row);
            }

            return result;
        }
    }
}
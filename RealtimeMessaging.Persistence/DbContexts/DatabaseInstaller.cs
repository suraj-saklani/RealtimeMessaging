using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeMessaging.Persistence.DbContexts
{
    internal sealed class DatabaseInstaller
    {
        private readonly string _connectionString;

        public DatabaseInstaller(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InstallAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = assembly
                .GetManifestResourceNames()
                .Single(x => x.EndsWith("Install.sql"));

            using var stream = assembly.GetManifestResourceStream(resourceName)!;

            using var reader = new StreamReader(stream);

            var sql = await reader.ReadToEndAsync();

            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            using var command = new SqlCommand(sql, connection);

            await command.ExecuteNonQueryAsync();
        }
    }
}

using System.Data.Entity.Core.EntityClient;
using MySql.Data.MySqlClient;

namespace Fate.DB
{
    public partial class frsDatabase
    {
        private static EntityConnectionStringBuilder _entityBuilder;

        private frsDatabase(string connectionString)
        : base(connectionString)
        {
        }

        /// <summary>
        /// Initializers EF database connection from given parameters
        /// </summary>
        public static void InitDatabaseConnection(string server, uint port, string userId, string password,
                                                  string databaseName)
        {
            // Initialize the connection string builder for the
            // underlying provider.
            var sqlBuilder =
                new MySqlConnectionStringBuilder
                {
                    Server = server,
                    Port = port,
                    UserID = userId,
                    Password = password,
                    PersistSecurityInfo = true,
                    Database = databaseName
                };
            // Set the properties for the data source.
            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();
            // Initialize the EntityConnectionStringBuilder.
            _entityBuilder = new EntityConnectionStringBuilder
            {
                ProviderConnectionString = providerString,
                Provider = "MySql.Data.MySqlClient",
                Metadata = @"res://*/frsdb.csdl|res://*/frsdb.ssdl|res://*/frsdb.msl",
            };
        }

        /// <summary>
        /// Create a new EF6 dynamic data context using the specified provider connection string.
        /// </summary>
        /// <returns></returns>
        public static frsDatabase Create()
        {
            return new frsDatabase(_entityBuilder.ConnectionString);
        }
    }
}

using System.Data.Entity.Core.EntityClient;
using Fate.Common.Data.GHost;
using MySql.Data.MySqlClient;

namespace Fate.DB
{
    public partial class ghostEntities
    {
        private static EntityConnectionStringBuilder _entityBuilder;

        private ghostEntities(string connectionString)
        : base(connectionString)
        {
        }

        /// <summary>
        /// Initializers EF database connection from given parameters
        /// </summary>
        public static void InitDatabaseConnection(GHostDatabaseInfo databaseInfo)
        {
            // Initialize the connection string builder for the
            // underlying provider.
            var sqlBuilder =
                new MySqlConnectionStringBuilder
                {
                    Server = databaseInfo.DatabaseServer,
                    Port = databaseInfo.DatabasePort,
                    UserID = databaseInfo.DatabaseUserName,
                    Password = databaseInfo.DatabasePassword,
                    PersistSecurityInfo = true,
                    Database = databaseInfo.DatabaseName
                };
            // Set the properties for the data source.
            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();
            // Initialize the EntityConnectionStringBuilder.
            _entityBuilder = new EntityConnectionStringBuilder
            {
                ProviderConnectionString = providerString,
                Provider = "MySql.Data.MySqlClient",
                Metadata = @"res://*/ghostAsia.csdl|res://*/ghostAsia.ssdl|res://*/ghostAsia.msl",
            };
        }

        /// <summary>
        /// Create a new EF6 dynamic data context using the specified provider connection string.
        /// </summary>
        /// <returns></returns>
        public static ghostEntities Create()
        {
            return new ghostEntities(_entityBuilder.ConnectionString);
        }
    }
}

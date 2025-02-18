using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace eCommerceInfrastucture.DbContext;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;
    public DapperDbContext(IConfiguration Confuguration)
    {
        _configuration = Confuguration;
        string connectionString = _configuration.GetConnectionString("PostgresConnection");

        // create new npgsql connection with the new received connection string
      _connection =  new NpgsqlConnection(connectionString);
    }

    public IDbConnection DbConnection => _connection;
}


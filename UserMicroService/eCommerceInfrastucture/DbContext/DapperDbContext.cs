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
        //_configuration = Confuguration;
        //string connectionString = _configuration.GetConnectionString("PostgresConnection");

        // create new npgsql connection with the new received connection string

        _configuration = Confuguration;
        string connectionStringTemplate = _configuration.GetConnectionString("PostgresConnection")!;
        string connectionString = connectionStringTemplate
          .Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST"))
          .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));
        _connection =  new NpgsqlConnection(connectionString);
    }

    public IDbConnection DbConnection => _connection;
}


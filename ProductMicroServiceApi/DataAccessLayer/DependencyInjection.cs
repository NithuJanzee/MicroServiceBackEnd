using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repository;
using DataAccessLayer.RepositoryContracts;
using eCommerce.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //  string connectionStringTemplate = configuration.GetConnectionString("DefaultConncetion")!;
            //string conencitonstring =  connectionStringTemplate.Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
            //      .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));

            //  services.AddScoped<IproductRepository, ProductRepository>();
            //  services.AddDbContext<ApplicationDbContext>(options =>
            //  options.UseMySQL(conencitonstring));

            string connectionStringTemplate = configuration.GetConnectionString("DefaultConnection")!;

            // Ensure environment variables are not null
            string mysqlHost = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? "localhost";
            string mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "261412";

            // Replace placeholders with environment variables
            string connectionString = connectionStringTemplate
                .Replace("$MYSQL_HOST", mysqlHost)
                .Replace("$MYSQL_PASSWORD", mysqlPassword);

            services.AddScoped<IproductRepository, ProductRepository>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(connectionString));
            return services;
        }
    }
}

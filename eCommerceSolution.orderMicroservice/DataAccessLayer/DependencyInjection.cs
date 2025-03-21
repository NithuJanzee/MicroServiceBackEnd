using eCommerce.DataAccessLayer.DbContext1;
using eCommerce.OrderMicroservice.DataAccessLayer.Repository;
using eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.OrderMicroservice.DataAccessLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {

        // Add DbContext
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        // Add repositories
        services.AddScoped<IorderRepository, OrderRepository>();

        return services;
    }
}

using eCommerce.Core.RepositoryContracts;
using eCommerceInfrastucture.DbContext;
using eCommerceInfrastucture.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerceInfrastucture
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Extenction methord to add infrastructure service to dependency injection container
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //add the service of the Ioc container infrastructure service often include the data access
            //caching and other low level components

            services.AddTransient<IusersRepository, UserRepository>();
            services.AddSingleton<DapperDbContext>();
            return services;
        }
    }
}

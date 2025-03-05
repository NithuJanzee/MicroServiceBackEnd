using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using eCommerce.OrderMicroservice.Businesslogiclayer.Validators;
using eCommerce.OrderMicroservice.Businesslogiclayer.Mappers;

namespace eCommerce.OrderMicroservice.BusinessLogicLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidators>();
        services.AddAutoMapper(typeof(Mappers).Assembly);
        return services;
    }
}

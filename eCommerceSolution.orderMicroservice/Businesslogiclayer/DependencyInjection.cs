using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using eCommerce.OrderMicroservice.Businesslogiclayer.Validators;
using eCommerce.OrderMicroservice.Businesslogiclayer.Mappers;
using eCommerce.OrderMicroservice.Businesslogiclayer.ServiceContact;
using eCommerce.OrderMicroservice.Businesslogiclayer.Services;

namespace eCommerce.OrderMicroservice.BusinessLogicLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidators>();
        services.AddAutoMapper(typeof(Mappers).Assembly);
        services.AddScoped<IorderService, OrderService>();
        return services;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBussnessLogicLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidators>();
            return services;
        }
    }
}

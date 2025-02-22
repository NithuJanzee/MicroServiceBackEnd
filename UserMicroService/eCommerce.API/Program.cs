using eCommerceInfrastucture;
using eCommerce.Core;
using eCommerce.API.MiddleWares;
using System.Text.Json.Serialization;
using eCommerce.Core.Mapper;
using FluentValidation.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

//before build add infrastucture service 
builder.Services.AddInfrastructure();
builder.Services.AddCore();

//add the controller to the service collection
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);
//Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
//add api explore services
builder.Services.AddEndpointsApiExplorer();
//add swagger genreration services
builder.Services.AddSwaggerGen();
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    });
var app = builder.Build();
app.UseExeptionHandlingMiddleWare();

//Routing 
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//controller route
app.MapControllers();

app.Run();

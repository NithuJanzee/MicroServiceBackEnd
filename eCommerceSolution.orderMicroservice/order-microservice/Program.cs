using eCommerce.OrderMicroservice.BusinessLogicLayer;
using eCommerce.OrderMicroservice.DataAccessLayer;
using eCommerce.OrderMicroservice.Middleware;
using FluentValidation.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer(builder.Configuration);

builder.Services.AddControllers();

//fluent validation
builder.Services.AddFluentValidationAutoValidation();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

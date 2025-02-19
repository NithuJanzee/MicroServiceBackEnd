using DataAccessLayer;
using BusinessLogicLayer;
using FluentValidation.AspNetCore;
using ProductMicroServiceApi.MiddleWare;
var builder = WebApplication.CreateBuilder(args);

//Add Dal And bil Services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBussnessLogicLayer();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();
app.UseMiddleware<ExeptionHandlingMiddleWare>();
app.MapControllers();

app.Run();

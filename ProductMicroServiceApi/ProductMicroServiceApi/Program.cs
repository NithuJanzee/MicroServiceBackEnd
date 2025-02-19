using DataAccessLayer;
using BusinessLogicLayer;
using FluentValidation.AspNetCore;
using ProductMicroServiceApi.MiddleWare;
using BusinessLogicLayer.Mapper;

var builder = WebApplication.CreateBuilder(args);

//Add Dal And bil Services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBussnessLogicLayer();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();
app.UseMiddleware<ExeptionHandlingMiddleWare>();
app.MapControllers();

app.Run();

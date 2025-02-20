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
// add api explorer service
builder.Services.AddEndpointsApiExplorer();
//add swagger service
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseMiddleware<ExeptionHandlingMiddleWare>();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

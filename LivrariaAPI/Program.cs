using LivrariaAPI.Endpoints;
using LivrariaAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();

var app = builder.Build();

app.MapProductsEndpoints();

var enviroment = app.Environment;
app.UseExceptionHandling(enviroment).UseAppCors().UseSwaggerMiddleware();

app.Run();


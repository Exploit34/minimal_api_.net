using ProductCatalog.Api.Endpoints;
using ProductCatalog.Api.Interface;
using ProductCatalog.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

app.MapProductEndpoints();

app.Run();

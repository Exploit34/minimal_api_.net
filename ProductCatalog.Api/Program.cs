using ProductCatalog.Api.Endpoints;
using ProductCatalog.Api.Interface;
using ProductCatalog.Api.Services;
using ProductCatalog.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddOpenApi();
builder.Services.AddValidation();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDatabase(builder.Configuration, builder.Environment);

app.MapProductEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

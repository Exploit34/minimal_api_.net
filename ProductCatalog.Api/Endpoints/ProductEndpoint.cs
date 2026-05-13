using Microsoft.AspNetCore.Http.HttpResults;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Services;
using ProductCatalog.Api.Interface;

namespace ProductCatalog.Api.Endpoints;

public static class ProductEndpoint
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products")
            .WithTags("Products");

        group.MapGet("/GetAll", (IProductService service) => 
            TypedResults.Ok(service.GetAll()))
            .WithName("GetAllProducts");

        group.MapGet("/{id:int}", Results<Ok<Product>, NotFound> (int id, IProductService service) =>
            service.GetById(id) is { } product is true
                ? TypedResults.Ok(product)
                : TypedResults.NotFound())
            .WithName("GetProductById");
        
        group.MapPost("/Add", (CreateProductRequest request, IProductService service) =>
        {
            var product = service.Create(request);
            return TypedResults.Created($"/products/{product.ProductId}", product);
        })
        .WithName("CreateProduct");

        group.MapPut("/{id:int}", Results<NoContent, NotFound> (int id, UpdateProductRequest request, IProductService service) =>
            service.Update(id, request) is true
                ? TypedResults.NoContent()
                : TypedResults.NotFound())
            .WithName("UpdateProduct");

        group.MapDelete("/{id:int}", Results<NoContent, NotFound> (int id, IProductService service) =>
            service.Delete(id) is true
                ? TypedResults.NoContent()
                : TypedResults.NotFound())
            .WithName("DeleteProduct");

        return group;
    }
}
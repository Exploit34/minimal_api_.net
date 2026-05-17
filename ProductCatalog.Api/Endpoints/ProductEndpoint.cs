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

        group.MapGet("/", (IProductService service) =>
            TypedResults.Ok(service.GetAll()))
            .WithName("GetAllProducts");

        group.MapGet("/{id:int}", Results<Ok<Productdb>, NotFound> (int id, IProductService service) =>
            service.GetById(id) is { } product
                ? TypedResults.Ok(product)
                : TypedResults.NotFound())
            .WithName("GetProductById");

        group.MapPost("/", Results<Created<Productdb>, Conflict<string>> (CreateProductRequest request, IProductService service) =>
        {
            var product = service.Create(request);
            
            if (product is null)
                return TypedResults.Conflict("Warning: Ya existe un producto con ese nombre.");

            return TypedResults.Created($"/products/{product.ProductId}", product);
        })
        .WithName("CreateProduct");

        group.MapPut("/{id:int}", Results<NoContent, NotFound> (int id, UpdateProductRequest request, IProductService service) =>
            service.Update(id, request)
                ? TypedResults.NoContent()
                : TypedResults.NotFound())
            .WithName("UpdateProduct");

        group.MapDelete("/{id:int}", Results<NoContent, NotFound> (int id, IProductService service) =>
            service.Delete(id)
                ? TypedResults.NoContent()
                : TypedResults.NotFound())
            .WithName("DeleteProduct");

        return group;
    }
}
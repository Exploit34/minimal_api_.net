using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Data;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Services;
using Xunit;

namespace ProductCatalog.Api.Tests;

public class ProductServiceTests
{
    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    private static Productdb SeedProduct(
        ApplicationDbContext context,
        string name  = "Phone",
        decimal price = 100m,
        int stock = 5)
    {
        var product = new Productdb { ProductName = name, Price = price, StockQuantity = stock };
        context.Products.Add(product);
        context.SaveChanges();
        return product;
    }

    // -------------------------------------------------------------------------
    // Create
    // -------------------------------------------------------------------------

    [Fact]
    public void Create_ShouldAddProduct_WhenRequestIsValid()
    {
        // Arrange
        using var context = CreateContext();
        var service = new ProductService(context);
        var request = new CreateProductRequest("Phone", 100m, 5);

        // Act
        var result = service.Create(request);

        // Assert
        Assert.NotNull(result);
        Assert.True(result!.ProductId > 0);
        Assert.Equal("Phone", result.ProductName);
        Assert.Equal(100m,    result.Price);
        Assert.Equal(5,       result.StockQuantity);
    }

    [Fact]
    public void Create_ShouldReturnNull_WhenProductNameAlreadyExists()
    {
        // Arrange
        using var context = CreateContext();
        SeedProduct(context, name: "Phone");

        var service = new ProductService(context);
        var request = new CreateProductRequest("Phone", 200m, 10);

        // Act
        var result = service.Create(request);

        // Assert
        Assert.Null(result);
        Assert.Equal(1, context.Products.Count());
    }

    // -------------------------------------------------------------------------
    // GetById
    // -------------------------------------------------------------------------

    [Fact]
    public void GetById_ShouldReturnProduct_WhenExists()
    {
        // Arrange
        using var context = CreateContext();
        var seeded = SeedProduct(context, name: "Laptop", price: 1500m, stock: 3);
        var service = new ProductService(context);

        // Act
        var result = service.GetById(seeded.ProductId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(seeded.ProductId, result!.ProductId);
        Assert.Equal("Laptop",         result.ProductName);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        using var context = CreateContext();
        var service = new ProductService(context);

        // Act
        var result = service.GetById(999);

        // Assert
        Assert.Null(result);
    }

    // -------------------------------------------------------------------------
    // Update
    // -------------------------------------------------------------------------

    [Fact]
    public void Update_ShouldModifyProduct_WhenExists()
    {
        // Arrange
        using var context = CreateContext();
        var seeded = SeedProduct(context, name: "Tablet", price: 500m, stock: 2);
        var service = new ProductService(context);
        var request = new UpdateProductRequest("Tablet Pro", 600m, 4);

        // Act
        var result = service.Update(seeded.ProductId, request);

        // Assert
        Assert.True(result);

        var updated = context.Products.Find(seeded.ProductId);
        Assert.NotNull(updated);
        Assert.Equal("Tablet Pro", updated!.ProductName);
        Assert.Equal(600m,         updated.Price);
        Assert.Equal(4,            updated.StockQuantity);
    }

    [Fact]
    public void Update_ShouldReturnFalse_WhenNotExists()
    {
        // Arrange
        using var context = CreateContext();
        var service = new ProductService(context);
        var request = new UpdateProductRequest("Ghost", 1m, 1);

        // Act
        var result = service.Update(999, request);

        // Assert
        Assert.False(result);
    }

    // -------------------------------------------------------------------------
    // Delete
    // -------------------------------------------------------------------------

    [Fact]
    public void Delete_ShouldRemoveProduct_WhenExists()
    {
        // Arrange
        using var context = CreateContext();
        var seeded = SeedProduct(context, name: "Watch", price: 200m, stock: 7);
        var service = new ProductService(context);

        // Act
        var result = service.Delete(seeded.ProductId);

        // Assert
        Assert.True(result);
        Assert.Null(context.Products.Find(seeded.ProductId));
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenNotExists()
    {
        // Arrange
        using var context = CreateContext();
        var service = new ProductService(context);

        // Act
        var result = service.Delete(999);

        // Assert
        Assert.False(result);
    }
}
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Interface;

namespace ProductCatalog.Api.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = new()
    {
        new Product(1, "Phone samsung", 2300.45m, 10),
        new Product(2, "Phone apple", 3000.90m, 10),
        new Product(3, "Laptop dell 15", 5045.15m, 10),
        new Product(4, "Reloj smart watch", 300.55m, 10)
    };

    private int _nextId = 4;

    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(int ProductId) =>
        _products.FirstOrDefault(p => p.ProductId == ProductId);

    public Product Create(CreateProductRequest request)
    {
        var product = new Product(_nextId++, request.ProductName, request.Price, request.StockQuantity);
        _products.Add(product);
        return product;
    }

    public bool Update(int ProductId, UpdateProductRequest request)
    {
        var index = _products.FindIndex(p => p.ProductId == ProductId);
        if (index == -1) return false;

        _products[index] = new Product(ProductId, request.ProductName, request.Price, request.StockQuantity);
        return true;
    }

    public bool Delete(int ProductId)
    {
        var index = _products.FindIndex(p => p.ProductId == ProductId);
        if (index == -1) return false;

        _products.RemoveAt(index);
        return true;
    }
}

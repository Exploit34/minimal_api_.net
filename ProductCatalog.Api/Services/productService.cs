using ProductCatalog.Api.Models;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Interface;
using ProductCatalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.Api.Services;

public class ProductService : IProductService
{

    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // private readonly List<Product> _products = new()
    // {
    //     new Product(1, "Phone samsung", 2300.45m, 10),
    //     new Product(2, "Phone apple", 3000.90m, 10),
    //     new Product(3, "Laptop dell 15", 5045.15m, 10),
    //     new Product(4, "Reloj smart watch", 300.55m, 10)
    // };

    // private int _nextId = 4;

    public IEnumerable<Productdb> GetAll() => _context.Products.AsNoTracking().ToList();

    public Productdb? GetById(int productId) =>
        _context.Products.AsNoTracking()
            .FirstOrDefault(p => p.ProductId == productId);

    public Productdb Create(CreateProductRequest request)
    {
        var exists = _context.Products.AsNoTracking()
            .Any(p => p.ProductName.ToLower() == request.ProductName.ToLower());

        if (exists) return null;

        var product = new Productdb
        {
            ProductName    = request.ProductName,
            Price          = request.Price,
            StockQuantity  = request.StockQuantity
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return product;
    }

    public bool Update(int productId, UpdateProductRequest request)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
        if (product == null) return false;

        product.ProductName   = request.ProductName;
        product.Price         = request.Price;
        product.StockQuantity = request.StockQuantity;

        _context.SaveChanges();
        return true;
    }

    public bool Delete(int productId)
    {
        var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
        if (product == null) return false;

        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }
}

using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Interface;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int ProductId);
    Product Create(CreateProductRequest request);

    bool Update(int ProductId, UpdateProductRequest request);
    bool Delete(int ProductId);
}
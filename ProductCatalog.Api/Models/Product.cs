namespace ProductCatalog.Api.Models;

public record Product(int ProductId, string ProductName, decimal Price, int StockQuantity);

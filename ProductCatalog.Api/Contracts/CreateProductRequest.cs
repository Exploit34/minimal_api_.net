using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Api.Contracts;

public record CreateProductRequest(
    [Required, StringLength(100, MinimumLength = 2)] string ProductName,
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0.")] decimal Price,
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be between 0 and 5000.")] int StockQuantity
);
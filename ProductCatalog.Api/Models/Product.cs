namespace ProductCatalog.Api.Models;
using System.ComponentModel.DataAnnotations;

// public record Product(int ProductId, string ProductName, decimal Price, int StockQuantity);

public class Productdb
{
    [Key]
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}
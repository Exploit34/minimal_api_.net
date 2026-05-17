using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : base(options)
    {
    }

    public DbSet<Productdb> Products => Set<Productdb>();
}
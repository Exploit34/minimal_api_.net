using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Data;
using ProductCatalog.Api.Validation;

namespace ProductCatalog.Api.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, 
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionString = ConnectionStringValidator
            .GetValidConnectionString(configuration, environment);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            ));

        return services;
    }
}
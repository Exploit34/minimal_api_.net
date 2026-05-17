namespace ProductCatalog.Api.Validation;

public static class ConnectionStringValidator
{
    public static string GetValidConnectionString(IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionString = environment.IsProduction()
            ? configuration.GetConnectionString("DefaultConnection")
            : configuration.GetConnectionString("MySqlConnection")
            ?? throw new InvalidOperationException("No se encontró ninguna conexión configurada.");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                environment.IsProduction()
                    ? "DefaultConnection no está configurada."
                    : "MySqlConnection no está configurada."
            );

        return connectionString;
    }
}
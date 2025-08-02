using Microsoft.EntityFrameworkCore;
using ShopControl.Infrastructure.Persistence.Context;

public static class ContextExtension
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
    {
        string mySqlConnection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

        return services;
    }
}
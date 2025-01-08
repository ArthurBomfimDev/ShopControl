using System.Text.Json.Serialization;

namespace ProjetoTeste.Api.Extensions;

public static class AddControllersAndConfigureJsonOptions
{
    public static IServiceCollection ConfigureJson(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        return services;
    }

}
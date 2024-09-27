using Microsoft.Extensions.DependencyInjection;
using Data;
using Bussiness.Services;

namespace Bussiness;
public static class ConfigureServices
{
    public static IServiceCollection ConfigureBussinessLayer (this IServiceCollection services)
    {
        services.ConfigureDataLayer();
        services.AddScoped<ProductoBussiness>();
        services.AddScoped<UsuarioBussiness>();
        services.AddScoped<ProductoVendidoBussiness>();
        services.AddScoped<VentaBussiness>();

        return services;
    }
}

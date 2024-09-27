using Data.Context;
using Data.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Data;
public static class ConfigureServices
{
    public static IServiceCollection ConfigureDataLayer (this IServiceCollection services)
    {
        services.AddDbContext<SistemaGestionContext>();
        services.AddScoped<ProductosDataAccess>();
        services.AddScoped<UsuariosDataAccess>();
        services.AddScoped<ProductosVendidosDataAccess>();
        services.AddScoped<VentasDataAccess>();

        return services;
    }
}

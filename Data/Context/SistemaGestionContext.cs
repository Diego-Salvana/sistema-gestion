using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;
public class SistemaGestionContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<ProductoVendido> ProductosVendidos { get; set; }
    public DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SistemaGestionDS;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}

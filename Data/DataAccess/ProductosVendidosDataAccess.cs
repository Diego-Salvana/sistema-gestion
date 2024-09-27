using Data.Context;
using Entities;

namespace Data.DataAccess;
public class ProductosVendidosDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public ProductosVendidosDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public List<ProductoVendido> ListarProductosVendidos ()
    {
        return _sistemaGestionContext.ProductosVendidos.ToList();
    }

    public ProductoVendido ObtenerProductoVendido (int id)
    {
        ProductoVendido? productoVendido =
            _sistemaGestionContext.ProductosVendidos.FirstOrDefault(p => p.Id == id);

        if (productoVendido is null)
        {
            throw new ArgumentException("No existe el ProductoVendido");
        }

        return productoVendido;
    }

    public void CrearProductoVendido (ProductoVendido productoVendido)
    {
        _sistemaGestionContext.ProductosVendidos.Add(productoVendido);
        _sistemaGestionContext.SaveChanges();
    }

    public void ModificarProductoVendido (int id, ProductoVendido productoAcutalizado)
    {
        ProductoVendido productoVendido = ObtenerProductoVendido(id);

        productoVendido.IdProducto = productoAcutalizado.IdProducto;
        productoVendido.Stock = productoAcutalizado.Stock;
        productoVendido.IdVenta = productoAcutalizado.IdVenta;

        _sistemaGestionContext.SaveChanges();
    }

    public void EliminarProductoVendido (int id)
    {
        _sistemaGestionContext.ProductosVendidos.Remove(ObtenerProductoVendido(id));
        _sistemaGestionContext.SaveChanges();
    }
}

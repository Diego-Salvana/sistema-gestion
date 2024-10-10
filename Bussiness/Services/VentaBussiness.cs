using Bussiness.Utils;
using Data.DataAccess;
using Entities;

namespace Bussiness.Services;
public class VentaBussiness
{
    private readonly VentasDataAccess _ventasDataAccess;
    private readonly UsuariosDataAccess _usuariosDataAccess;
    private readonly ProductosDataAccess _productosDataAccess;
    private readonly ProductosVendidosDataAccess _productosVendidosDataAccess;

    public VentaBussiness (
        VentasDataAccess ventasDataAccess,
        UsuariosDataAccess usuariosDataAccess,
        ProductosDataAccess productosDataAccess,
        ProductosVendidosDataAccess productosVendidosDataAccess)
    {
        _ventasDataAccess = ventasDataAccess;
        _usuariosDataAccess = usuariosDataAccess;
        _productosDataAccess = productosDataAccess;
        _productosVendidosDataAccess = productosVendidosDataAccess;
    }

    public async Task<List<Venta>> ListarVentasAsync ()
    {
        try
        {
            return await _ventasDataAccess.ListarVentasAsync();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener Ventas");
        }
    }

    public async Task<Venta> ObtenerVentaAsync (int id)
    {
        Venta? venta;

        try
        {
            venta = await _ventasDataAccess.ObtenerVentaAsync(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener la Venta");
        }

        return venta;
    }

    public async Task CrearVentaAsync (Venta venta)
    {
        List<Producto> productos = [];

        try
        {
            await _usuariosDataAccess.ObtenerUsuarioAsync(venta.Usuario.Id);

            //foreach (int productoId in venta.Productos)
            //{
            //    Producto producto = await _productosDataAccess.ObtenerProductoAsync(productoId);

            //    if (producto.Stock < 1)
            //        throw new Exception($"Sin stock para el producto: {producto.Descripcion}");

            //    productos.Add(producto);
            //}

            Venta nuevaVenta = await _ventasDataAccess.CrearVentaAsync(venta);

            foreach (Producto producto in productos)
            {
                producto.Stock -= 1;
                await _productosDataAccess.ModificarProductoAsync(producto.Id, producto);

                ProductoVendido? productoVendido =
                    await _productosVendidosDataAccess.ProductoVendidoByProductoId(producto.Id);

                if (productoVendido is null)
                {
                    ProductoVendido nuevoProdVendido = new()
                    {
                        Producto = producto,
                        Stock = producto.Stock,
                        Ventas = [nuevaVenta]
                    };

                    await _productosVendidosDataAccess.CrearProductoVendidoAsync(nuevoProdVendido);
                }
                else
                {
                    productoVendido.Stock = producto.Stock;
                    productoVendido.Ventas.Add(nuevaVenta);

                    await _productosVendidosDataAccess
                    .ModificarProductoVendidoAsync(productoVendido.Id, productoVendido);
                }
            }
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear la Venta");
        }
    }

    public async Task ModificarVentaAsync (int id, Venta ventaAcutalizada)
    {
        try
        {
            await _ventasDataAccess.ModificarVentaAsync(id, ventaAcutalizada);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar la Venta");
        }
    }

    public async Task EliminarVentaAsync (int id)
    {
        try
        {
            await _ventasDataAccess.EliminarVentaAsync(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar la Venta");
        }
    }
}

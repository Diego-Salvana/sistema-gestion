using Bussiness.Utils;
using Data.Context;
using Data.DataAccess;
using Entities;
using Entities.DTOs;

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

    public async Task CrearVentaAsync (VentaDTO ventaDTO)
    {
        List<int> idsProductos = ventaDTO.ProductosDetalle.Select(pd => pd.Id).ToList();

        try
        {
            Usuario usuario = await _usuariosDataAccess.ObtenerUsuarioAsync(ventaDTO.UsuarioId);

            List<Producto> productos = await _productosDataAccess.ObtenerProductosAsync(idsProductos);

            foreach (var idProducto in ventaDTO.ProductosDetalle)
            {
                Producto producto = productos.FirstOrDefault(p => p.Id == idProducto.Id) ??
                     throw new Exception($"Producto con Id {idProducto.Id} no encontrado.");

                if (producto.Stock < idProducto.Cantidad || producto.Stock < 1)
                {
                    throw new Exception($"Sin stock para el producto: {producto.Descripcion}");
                }
            }

            Venta ventaObj = new()
            {
                ProductosVendidos = [],
                Usuario = usuario,
                Comentario = ventaDTO.Comentario
            };

            Venta nuevaVenta = await _ventasDataAccess.CrearVentaAsync(ventaObj);

            foreach (Producto producto in productos)
            {
                int cantidad = ventaDTO.ProductosDetalle
                                    .FirstOrDefault(pd => pd.Id == producto.Id)?
                                    .Cantidad ?? 0;

                producto.Stock -= cantidad;

                ProductoVendido productoVendido = new()
                {
                    Producto = producto,
                    Stock = cantidad,
                    Venta = nuevaVenta
                };

                await _productosVendidosDataAccess.CrearProductoVendidoAsync(productoVendido);

                nuevaVenta.ProductosVendidos.Add(productoVendido);
            }

            await _productosDataAccess.ModificarProductosAsync();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, ex.Message);
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

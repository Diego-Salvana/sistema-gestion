using Bussiness.Utils;
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

    public async Task<List<VentaDTORespuesta>> ListarVentasAsync ()
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

    public async Task<List<VentaDTORespuesta>> ListarVentasAsync (int usuarioId)
    {
        try
        {
            return await _ventasDataAccess.ListarVentasAsync(usuarioId);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener Ventas");
        }
    }

    public async Task<VentaDTORespuesta> ObtenerVentaAsync (int ventaId)
    {
        try
        {
            Venta venta = await _ventasDataAccess.ObtenerVentaAsync(ventaId);

            return new VentaDTORespuesta(venta);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener la Venta");
        }
    }

    public async Task<Venta> CrearVentaAsync (int usuarioId, VentaDTO ventaDTO)
    {
        List<int> idsProductos = ventaDTO.ProductosDetalle.Select(pd => pd.Id).ToList();

        try
        {
            Usuario usuario = await _usuariosDataAccess.ObtenerUsuarioAsync(usuarioId);

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

            return nuevaVenta;
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, ex.Message);
        }
    }

    public async Task ModificarVentaAsync (
        int ventaId,
        int usuarioId,
        VentaDTO.ComentarioTxt ventaDTO)
    {
        try
        {
            await _ventasDataAccess.ModificarVentaAsync(ventaId, usuarioId, ventaDTO);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar la Venta");
        }
    }

    public async Task AgregarProductoAsync (
        int ventaId,
        int usuarioId,
        VentaDTO.DetalleProducto detalleProducto)
    {
        try
        {
            Producto producto =
                await _productosDataAccess.ObtenerProductoAsync(detalleProducto.Id) ??
                throw new Exception($"Producto con Id {detalleProducto.Id} no encontrado.");

            if (producto.Stock < detalleProducto.Cantidad || producto.Stock < 1)
            {
                throw new Exception($"Sin stock para el producto: {producto.Descripcion}");
            }

            int cantidad = detalleProducto?.Cantidad ?? 0;

            ProductoVendido productoVendido = new()
            {
                Producto = producto,
                Stock = cantidad,
            };

            await _ventasDataAccess.AgregarProducto(ventaId, usuarioId, productoVendido);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar la Venta");
        }
    }

    public async Task QuitarProductoAsync (
        int ventaId,
        int usuarioId,
        int productoVendidoId)
    {
        try
        {
            ProductoVendido pv =
                await _productosVendidosDataAccess.ObtenerProductoVendidoAsync(productoVendidoId);

            await _ventasDataAccess.QuitarProducto(ventaId, usuarioId, pv);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar la Venta");
        }
    }

    public async Task EliminarVentaAsync (int ventaId, int usuarioId)
    {
        try
        {
            await _ventasDataAccess.EliminarVentaAsync(ventaId, usuarioId);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar la Venta");
        }
    }
}

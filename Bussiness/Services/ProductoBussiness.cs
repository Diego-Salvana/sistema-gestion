using Bussiness.Utils;
using Data.DataAccess;
using Entities;
using Entities.DTOs;

namespace Bussiness.Services;
public class ProductoBussiness
{
    private readonly ProductosDataAccess _productosDataAccess;
    private readonly UsuariosDataAccess _usuariosDataAccess;

    public ProductoBussiness (ProductosDataAccess dataAccess, UsuariosDataAccess usuariosDataAccess)
    {
        _productosDataAccess = dataAccess;
        _usuariosDataAccess = usuariosDataAccess;
    }

    public async Task<List<Producto>> ListarProductosAsync ()
    {
        try
        {
            return await _productosDataAccess.ListarProductosAsync();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió error al obtener Productos");
        }
    }

    public async Task<Producto> ObtenerProductoAsync (int id)
    {
        try
        {
            return await _productosDataAccess.ObtenerProductoAsync(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener el Producto");
        }
    }

    public async Task CrearProductoAsync (ProductoDTO productoDTO)
    {
        try
        {
            Usuario usuario = await _usuariosDataAccess.ObtenerUsuarioAsync(productoDTO.UsuarioId);

            Producto nuevoProducto = new()
            {
                Descripcion = productoDTO.Descripcion,
                Costo = productoDTO.Costo,
                PrecioVenta = productoDTO.PrecioVenta,
                Stock = productoDTO.Stock,
                Usuario = usuario
            };

            await _productosDataAccess.CrearProductoAsync(nuevoProducto);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear el Producto");
        }
    }

    public async Task ModificarProductoAsync (int id, Producto productoAcutalizado)
    {
        try
        {
            await _productosDataAccess.ModificarProductoAsync(id, productoAcutalizado);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar el Producto");
        }
    }

    public async Task EliminarProductoAsync (int id)
    {
        try
        {
            await _productosDataAccess.EliminarProductoAsync(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar el Producto");
        }
    }
}

using Bussiness.Utils;
using Data.DataAccess;
using Entities;
using Entities.DTOs;

namespace Bussiness.Services;
public class ProductoVendidoBussiness
{
    private readonly ProductosVendidosDataAccess _productosVendidosDataAcces;

    public ProductoVendidoBussiness (ProductosVendidosDataAccess dataAccess)
    {
        _productosVendidosDataAcces = dataAccess;
    }

    public async Task<List<ProductoVendidoDTO>> ListarProductosVendidosAsync ()
    {
        try
        {
            return await _productosVendidosDataAcces.ListarProductosVendidosAsync();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener ProductosVendidos");
        }
    }

    public async Task<List<ProductoVendidoDTO>> ListarProductosVendidosAsync (int usuarioId)
    {
        try
        {
            return await _productosVendidosDataAcces.ListarProductosVendidosAsync(usuarioId);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener ProductosVendidos");
        }
    }

    public async Task<ProductoVendidoDTO> ObtenerProductoVendidoAsync (int id)
    {
        try
        {
            ProductoVendido pv = await _productosVendidosDataAcces.ObtenerProductoVendidoAsync(id);

            return new ProductoVendidoDTO(pv);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener el ProductoVendido");
        }
    }
}

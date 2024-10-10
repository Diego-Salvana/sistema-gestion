using Entities;

namespace UI.ClientServices;
public class ProductosVendidosService
{
    private readonly HttpClient _httpClient;

    public ProductosVendidosService (HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductoVendido>?> ListarProductosVendidosAsync ()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductoVendido>>("");
    }

    public async Task<ProductoVendido?> ObtenerProductoVendidoAsync (int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductoVendido>($"{id}");
    }

    public async Task CrearProductoVendidoAsync (ProductoVendido productoVendido)
    {
        await _httpClient.PostAsJsonAsync("", productoVendido);
    }

    public async Task ModificarProductoVendidoAsync (int id, ProductoVendido productoVendidoAcutalizado)
    {
        await _httpClient.PutAsJsonAsync($"{id}", productoVendidoAcutalizado);
    }

    public async Task EliminarProductoVendidoAsync (int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }
}

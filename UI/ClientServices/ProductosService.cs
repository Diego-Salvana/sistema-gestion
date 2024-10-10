using Entities;

namespace UI.ClientServices;
public class ProductosService
{
    private readonly HttpClient _httpClient;

    public ProductosService (HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Producto>?> ListarProductosAsync ()
    {
        return await _httpClient.GetFromJsonAsync<List<Producto>>("");
    }

    public async Task<Producto?> ObtenerProductoAsync (int id)
    {
        return await _httpClient.GetFromJsonAsync<Producto>($"{id}");
    }

    public async Task CrearProductoAsync (Producto producto)
    {
        await _httpClient.PostAsJsonAsync("", producto);
    }

    public async Task ModificarProductoAsync (int id, Producto productoAcutalizado)
    {
        await _httpClient.PutAsJsonAsync($"{id}", productoAcutalizado);
    }

    public async Task EliminarProductoAsync (int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }
}

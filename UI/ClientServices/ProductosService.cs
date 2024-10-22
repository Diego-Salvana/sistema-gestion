using Entities;

namespace UI.ClientServices;
public class ProductosService
{
    private readonly HttpClient _httpClient;
    private readonly Usuario? _usuario;

    public ProductosService (HttpClient httpClient, InicioRegistro inicioRegistro)
    {
        _httpClient = httpClient;
        _usuario = inicioRegistro.UsuarioActual;

        _httpClient.DefaultRequestHeaders.Add("UsuarioId", _usuario?.Id.ToString());
    }

    public async Task<List<Producto>?> ListarProductosAsync ()
    {
        return await _httpClient.GetFromJsonAsync<List<Producto>>("");
    }

    public async Task<List<Producto>?> ListarProductosAsync (int usuarioId)
    {
        return await _httpClient.GetFromJsonAsync<List<Producto>>($"usuario/{usuarioId}");
    }

    public async Task<Producto?> ObtenerProductoAsync (int id)
    {
        return await _httpClient.GetFromJsonAsync<Producto>($"{id}");
    }

    public async Task CrearProductoAsync (Producto producto)
    {
        await _httpClient.PostAsJsonAsync("", producto);
    }

    public async Task ModificarProductoAsync (int productoId, Producto productoAcutalizado)
    {
        await _httpClient.PutAsJsonAsync($"{productoId}", productoAcutalizado);
    }

    public async Task EliminarProductoAsync (int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }
}

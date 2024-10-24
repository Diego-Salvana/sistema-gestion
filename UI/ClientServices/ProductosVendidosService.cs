using Entities;
using Entities.DTOs;

namespace UI.ClientServices;
public class ProductosVendidosService
{
    private readonly HttpClient _httpClient;
    private readonly Usuario? _usuario;

    public ProductosVendidosService (HttpClient httpClient, InicioRegistro inicioRegistro)
    {
        _httpClient = httpClient;
        _usuario = inicioRegistro.UsuarioActual;

        _httpClient.DefaultRequestHeaders.Add("UsuarioId", _usuario?.Id.ToString());
    }

    public async Task<List<ProductoVendidoDTO>?> ListarProductosVendidosAsync ()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductoVendidoDTO>>("");
    }

    public async Task<List<ProductoVendidoDTO>?> ListarProductosVendidosAsync (int usuarioId)
    {
        return await _httpClient.GetFromJsonAsync<List<ProductoVendidoDTO>>($"usuario/{usuarioId}");
    }
}

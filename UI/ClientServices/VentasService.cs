using Entities;

namespace UI.ClientServices;
public class VentasService
{
    private readonly HttpClient _httpClient;

    public VentasService (HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Venta>?> ListarVentasAsync ()
    {
        return await _httpClient.GetFromJsonAsync<List<Venta>>("");
    }

    public async Task<Venta?> ObtenerVentaAsync (int id)
    {
        return await _httpClient.GetFromJsonAsync<Venta>($"{id}");
    }

    public async Task CrearVentaAsync (Venta venta)
    {
        await _httpClient.PostAsJsonAsync("", venta);
    }

    public async Task ModificarVentaAsync (int id, Venta ventaAcutalizado)
    {
        await _httpClient.PutAsJsonAsync($"{id}", ventaAcutalizado);
    }

    public async Task EliminarVentaAsync (int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }
}

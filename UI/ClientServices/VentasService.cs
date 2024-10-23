using Entities;
using Entities.DTOs;

namespace UI.ClientServices;
public class VentasService
{
    private readonly HttpClient _httpClient;
    private readonly Usuario? _usuario;

    public VentasService (HttpClient httpClient, InicioRegistro inicioRegistro)
    {
        _httpClient = httpClient;
        _usuario = inicioRegistro.UsuarioActual;

        _httpClient.DefaultRequestHeaders.Add("UsuarioId", _usuario?.Id.ToString());
    }

    public async Task<List<VentaDTORespuesta>?> ListarVentasAsync ()
    {
        return await _httpClient.GetFromJsonAsync<List<VentaDTORespuesta>>("");
    }

    public async Task<List<VentaDTORespuesta>?> ListarVentasAsync (int usuarioId)
    {
        return await _httpClient.GetFromJsonAsync<List<VentaDTORespuesta>>($"usuario/{usuarioId}");
    }

    public async Task<VentaDTORespuesta?> ObtenerVentaAsync (int id)
    {
        return await _httpClient.GetFromJsonAsync<VentaDTORespuesta>($"{id}");
    }

    public async Task CrearVentaAsync (VentaDTO ventaDTO)
    {
        var respuesta = await _httpClient.PostAsJsonAsync("", ventaDTO);

        if (!respuesta.IsSuccessStatusCode)
        {
            var errorMessage = await respuesta.Content.ReadAsStringAsync();
            throw new Exception($"Error al crear venta: {errorMessage}");
        }
    }

    public async Task ModificarVentaAsync (int ventaId, VentaDTO.ComentarioTxt ventaDTO)
    {
        var respuesta = await _httpClient.PutAsJsonAsync($"{ventaId}/comentario", ventaDTO);

        if (!respuesta.IsSuccessStatusCode)
        {
            var errorMessage = await respuesta.Content.ReadAsStringAsync();
            throw new Exception($"Error al modificar venta: {errorMessage}");
        }
    }

    public async Task<Producto> AgregarProductoAsync (int ventaId, VentaDTO.DetalleProducto detalleProducto)
    {
        var respuesta = await _httpClient.PutAsJsonAsync($"{ventaId}/agregarProducto", detalleProducto);

        if (respuesta.IsSuccessStatusCode)
        {
            Producto? producto = await respuesta.Content.ReadFromJsonAsync<Producto>();

            return producto!;
        }
        else
        {
            var errorMessage = await respuesta.Content.ReadAsStringAsync();
            throw new Exception($"Error agregar producto vendido: {errorMessage}");
        }
    }

    public async Task QuitarProductoAsync (int ventaId, int productoVendidoId)
    {
        var respuesta = await _httpClient.DeleteAsync($"{ventaId}/quitarProducto/{productoVendidoId}");

        if (!respuesta.IsSuccessStatusCode)
        {
            var errorMessage = await respuesta.Content.ReadAsStringAsync();
            throw new Exception($"Error quitar producto vendido: {errorMessage}");
        }
    }

    public async Task EliminarVentaAsync (int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }
}

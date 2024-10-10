using Entities;

namespace UI.ClientServices;
public class UsuariosService
{
    private readonly HttpClient _httpClient;

    public UsuariosService (HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Usuario>?> ListarUsuariosAsync ()
    {
        return await _httpClient.GetFromJsonAsync<List<Usuario>>("");
    }

    public async Task<Usuario?> ObtenerUsuarioAsync (int id)
    {
        return await _httpClient.GetFromJsonAsync<Usuario>($"{id}");
    }

    public async Task CrearUsuarioAsync (Usuario usuario)
    {
        await _httpClient.PostAsJsonAsync("", usuario);
    }

    public async Task ModificarUsuarioAsync (int id, Usuario usuarioAcutalizado)
    {
        await _httpClient.PutAsJsonAsync($"{id}", usuarioAcutalizado);
    }

    public async Task EliminarUsuarioAsync (int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }
}

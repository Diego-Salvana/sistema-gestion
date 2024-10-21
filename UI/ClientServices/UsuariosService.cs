using Entities;
using Entities.DTOs;

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

    public async Task<Usuario> CrearUsuarioAsync (Usuario nuevoUsuario)
    {
        var respuesta = await _httpClient.PostAsJsonAsync("registrar", nuevoUsuario);

        if (respuesta.IsSuccessStatusCode)
        {
            Usuario? usuario = await respuesta.Content.ReadFromJsonAsync<Usuario>();

            return usuario!;
        }
        else
        {
            var errorMessage = await respuesta.Content.ReadAsStringAsync();
            throw new Exception($"Error al crear usuario: {errorMessage}");
        }
    }

    public async Task<Usuario> IniciarSesion (IngresoDTO usuarioData)
    {
        var respuesta = await _httpClient.PostAsJsonAsync("ingresar", usuarioData);

        if (respuesta.IsSuccessStatusCode)
        {
            Usuario? usuario = await respuesta.Content.ReadFromJsonAsync<Usuario>();

            return usuario!;
        }
        else
        {
            var errorMessage = await respuesta.Content.ReadAsStringAsync();
            throw new Exception($"Error al iniciar sesión: {errorMessage}");
        }
    }

    public async Task<Usuario> ModificarUsuarioAsync (int id, Usuario usuarioAcutalizado)
    {
        var respuesta = await _httpClient.PutAsJsonAsync($"{id}", usuarioAcutalizado);

        if (respuesta.IsSuccessStatusCode)
        {
            Usuario? usuario = await respuesta.Content.ReadFromJsonAsync<Usuario>();

            return usuario!;
        }
        else
        {
            var errorMessage = await respuesta.Content.ReadAsStringAsync();
            throw new Exception($"Error al modificar usuario: {errorMessage}");
        }
    }

    public async Task EliminarUsuarioAsync (int id)
    {
        await _httpClient.DeleteAsync($"{id}");
    }
}

using Entities;
using Entities.DTOs;
using UI.ClientServices;

namespace UI;
public class InicioRegistro
{
    private readonly UsuariosService _usuariosService;

    public Usuario? UsuarioActual { get; private set; } = new Usuario()
    {
        Id = 8,
        Nombre = "User",
        Apellido = "string",
        NombreUsuario = "string",
        Contraseña = "string",
        Mail = "user@example.com"
    };

    public event Action? RefrescarUsuario;

    public InicioRegistro (UsuariosService usuariosService)
    {
        _usuariosService = usuariosService;
    }

    private void ActualizarUsuario (Usuario? usuario)
    {
        UsuarioActual = usuario;
        NotificarCambios();
    }

    private void NotificarCambios () => RefrescarUsuario?.Invoke();

    public async Task RegistrarUsuarioAsync (Usuario nuevoUsuario)
    {
        Usuario usuario = await _usuariosService.CrearUsuarioAsync(nuevoUsuario);

        ActualizarUsuario(usuario);
    }

    public async Task IniciarSesionAsync (IngresoDTO usuarioDTO)
    {
        Usuario usuario = await _usuariosService.IniciarSesion(usuarioDTO);

        ActualizarUsuario(usuario);
    }

    public void CerrarSesión ()
    {
        ActualizarUsuario(null);
    }

    public async Task ModificarUsuarioAsync (int id, Usuario usuarioModificado)
    {
        Usuario usuario = await _usuariosService.ModificarUsuarioAsync(id, usuarioModificado);

        ActualizarUsuario(usuario);
    }

    public async Task EliminarUsuario (int id)
    {
        await _usuariosService.EliminarUsuarioAsync(id);

        ActualizarUsuario(null);
    }
}

using Bussiness.Utils;
using Data.DataAccess;
using Entities;

namespace Bussiness.Services;
public class UsuarioBussiness
{
    private readonly UsuariosDataAccess _usuariosDataAccess;

    public UsuarioBussiness (UsuariosDataAccess dataAccess)
    {
        _usuariosDataAccess = dataAccess;
    }

    public async Task<List<Usuario>> ListarUsuariosAsync ()
    {
        try
        {
            return await _usuariosDataAccess.ListarUsuariosAsync();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió error al obtener usuarios");
        }
    }

    public async Task<Usuario> ObtenerUsuarioAsync (int id)
    {
        try
        {
            return await _usuariosDataAccess.ObtenerUsuarioAsync(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió error al obtener el usuario");
        }
    }

    public async Task CrearUsuarioAsync (Usuario usuario)
    {
        try
        {
            await _usuariosDataAccess.CrearUsuarioAsync(usuario);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear el usuario");
        }
    }

    public async Task ModificarUsuarioAsync (int id, Usuario usuarioAcutalizado)
    {
        try
        {
            await _usuariosDataAccess.ModificarUsuarioAsync(id, usuarioAcutalizado);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar el usuario");
        }
    }

    public async Task EliminarUsuarioAsync (int id)
    {
        try
        {
            await _usuariosDataAccess.EliminarUsuarioAsync(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar el usuario");
        }
    }
}

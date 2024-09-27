using Bussiness.Utils;
using Data.DataAccess;
using Entities;

namespace Bussiness.Services;
public class UsuarioBussiness
{
    private readonly UsuariosDataAccess _usuarioDataAccess;

    public UsuarioBussiness (UsuariosDataAccess dataAccess)
    {
        _usuarioDataAccess = dataAccess;
    }

    public List<Usuario> ListarUsuarios ()
    {
        try
        {
            return _usuarioDataAccess.ListarUsuarios();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió error al obtener usuarios");
        }
    }

    public Usuario ObtenerUsuario (int id)
    {
        Usuario? usuario;

        try
        {
            usuario = _usuarioDataAccess.ObtenerUsuario(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener el usuario");
        }

        return usuario;
    }

    public void CrearUsuario (Usuario usuario)
    {
        try
        {
            _usuarioDataAccess.CrearUsuario(usuario);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear el usuario");
        }
    }

    public void ModificarUsuario (int id, Usuario usuarioAcutalizado)
    {
        try
        {
            _usuarioDataAccess.ModificarUsuario(id, usuarioAcutalizado);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar el usuario");
        }
    }

    public void EliminarUsuario (int id)
    {
        try
        {
            _usuarioDataAccess.EliminarUsuario(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar el usuario");
        }
    }
}

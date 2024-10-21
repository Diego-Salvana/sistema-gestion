using Bussiness.Utils;
using Data.DataAccess;
using Entities;
using Entities.DTOs;
using Microsoft.IdentityModel.Tokens;

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
            Usuario usuario = await _usuariosDataAccess.ObtenerUsuarioAsync(id);

            return usuario;
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió error al obtener el usuario");
        }
    }

    public async Task<Usuario> CrearUsuarioAsync (Usuario usuario)
    {
        try
        {
            List<Usuario> usuariosExistentes =
                await _usuariosDataAccess.ObtenerUsuarioByNombreUsuarioOrEmail(usuario);

            if (!usuariosExistentes.IsNullOrEmpty())
            {
                Usuario? usuarioByNombreUsuario =
                    usuariosExistentes.FirstOrDefault(u => u.NombreUsuario == usuario.NombreUsuario);

                if (usuarioByNombreUsuario is not null)
                {
                    throw new ArgumentException("El nombre de usuario ya está en uso");
                }

                Usuario? usuarioByEmail = usuariosExistentes.FirstOrDefault(u => u.Mail == usuario.Mail);

                if (usuarioByEmail is not null)
                {
                    throw new ArgumentException("El email ya está en uso");
                }
            }

            Usuario nuevoUsuario = await _usuariosDataAccess.CrearUsuarioAsync(usuario);

            nuevoUsuario.Contraseña = string.Empty;

            return nuevoUsuario;
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear el usuario");
        }
    }

    public async Task<Usuario> Ingresar (IngresoDTO usuario)
    {
        try
        {
            Usuario usuarioExistente =
                await _usuariosDataAccess.ObtenerUsuarioByNombreUsuario(usuario.NombreUsuario) ??
                throw new ArgumentException(
                    $"No se encontró el usuario {usuario.NombreUsuario}");

            if (usuario.Contraseña != usuarioExistente.Contraseña)
            {
                throw new ArgumentException("Contraseña incorrecta");
            }

            usuarioExistente.Contraseña = string.Empty;

            return usuarioExistente;
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar el usuario");
        }
    }

    public async Task<Usuario> ModificarUsuarioAsync (int id, Usuario usuarioAcutalizado)
    {
        try
        {
            Usuario usuario = await _usuariosDataAccess.ModificarUsuarioAsync(id, usuarioAcutalizado);

            usuario.Contraseña = string.Empty;

            return usuario;
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

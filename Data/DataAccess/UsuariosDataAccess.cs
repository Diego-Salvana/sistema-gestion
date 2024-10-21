using Data.Context;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess;
public class UsuariosDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public UsuariosDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public async Task<List<Usuario>> ListarUsuariosAsync ()
    {
        return await _sistemaGestionContext.Usuarios
                            .Select(u => new Usuario()
                            {
                                Id = u.Id,
                                Nombre = u.Nombre,
                                Apellido = u.Apellido,
                                NombreUsuario = u.NombreUsuario,
                                Contraseña = string.Empty,
                                Mail = u.Mail,
                            })
                            .ToListAsync();
    }

    public async Task<Usuario> ObtenerUsuarioAsync (int id)
    {
        Usuario? usuario = await _sistemaGestionContext.Usuarios
                                        .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null)
        {
            throw new ArgumentException("No existe el usuario");
        }

        return usuario;
    }

    public async Task<Usuario?> ObtenerUsuarioByNombreUsuario (string nombreUsuario)
    {
        return await _sistemaGestionContext.Usuarios
                            .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
    }

    public async Task<List<Usuario>> ObtenerUsuarioByNombreUsuarioOrEmail (Usuario usuario)
    {
        return await _sistemaGestionContext.Usuarios
                            .Where(u => u.NombreUsuario == usuario.NombreUsuario || u.Mail == usuario.Mail)
                            .ToListAsync();
    }

    public async Task<Usuario> CrearUsuarioAsync (Usuario usuario)
    {
        await _sistemaGestionContext.Usuarios.AddAsync(usuario);
        await _sistemaGestionContext.SaveChangesAsync();

        return usuario;
    }

    public async Task<Usuario> ModificarUsuarioAsync (int id, Usuario usuarioAcutalizado)
    {
        Usuario usuario = await ObtenerUsuarioAsync(id);

        usuario.Nombre = usuarioAcutalizado.Nombre;
        usuario.Apellido = usuarioAcutalizado.Apellido;
        usuario.NombreUsuario = usuarioAcutalizado.NombreUsuario;
        usuario.Contraseña = usuarioAcutalizado.Contraseña;
        usuario.Mail = usuarioAcutalizado.Mail;

        await _sistemaGestionContext.SaveChangesAsync();

        return usuario;
    }

    public async Task EliminarUsuarioAsync (int id)
    {
        Usuario usuario = await ObtenerUsuarioAsync(id);

        _sistemaGestionContext.Usuarios.Remove(usuario);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}

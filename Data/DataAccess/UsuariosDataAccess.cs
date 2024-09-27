using Data.Context;
using Entities;

namespace Data.DataAccess;
public class UsuariosDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public UsuariosDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public List<Usuario> ListarUsuarios ()
    {
        return _sistemaGestionContext.Usuarios.ToList();
    }

    public Usuario ObtenerUsuario (int id)
    {
        Usuario? usuario = _sistemaGestionContext.Usuarios.FirstOrDefault(u => u.Id == id);

        if (usuario is null)
        {
            throw new ArgumentException("No existe el usuario");
        }

        return usuario;
    }

    public void CrearUsuario (Usuario usuario)
    {
        _sistemaGestionContext.Usuarios.Add(usuario);
        _sistemaGestionContext.SaveChanges();
    }

    public void ModificarUsuario (int id, Usuario usuarioAcutalizado)
    {
        Usuario usuario = ObtenerUsuario(id);

        usuario.Nombre = usuarioAcutalizado.Nombre;
        usuario.Apellido = usuarioAcutalizado.Apellido;
        usuario.NombreUsuario = usuarioAcutalizado.NombreUsuario;
        usuario.Contraseña = usuarioAcutalizado.Contraseña;
        usuario.Mail = usuarioAcutalizado.Mail;

        _sistemaGestionContext.SaveChanges();
    }

    public void EliminarUsuario (int id)
    {
        _sistemaGestionContext.Usuarios.Remove(ObtenerUsuario(id));
        _sistemaGestionContext.SaveChanges();
    }
}

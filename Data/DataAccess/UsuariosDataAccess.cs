﻿using Data.Context;
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
        Usuario? usuario = await _sistemaGestionContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null)
        {
            throw new ArgumentException("No existe el usuario");
        }

        return usuario;
    }

    public async Task<Usuario> CrearUsuarioAsync (Usuario usuario)
    {
        await _sistemaGestionContext.Usuarios.AddAsync(usuario);
        await _sistemaGestionContext.SaveChangesAsync();

        return usuario;
    }

    public async Task ModificarUsuarioAsync (int id, Usuario usuarioAcutalizado)
    {
        Usuario usuario = await ObtenerUsuarioAsync(id);

        usuario.Nombre = usuarioAcutalizado.Nombre;
        usuario.Apellido = usuarioAcutalizado.Apellido;
        usuario.NombreUsuario = usuarioAcutalizado.NombreUsuario;
        usuario.Contraseña = usuarioAcutalizado.Contraseña;
        usuario.Mail = usuarioAcutalizado.Mail;

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarUsuarioAsync (int id)
    {
        _sistemaGestionContext.Usuarios.Remove(await ObtenerUsuarioAsync(id));
        await _sistemaGestionContext.SaveChangesAsync();
    }
}
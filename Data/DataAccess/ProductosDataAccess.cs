﻿using Data.Context;
using Entities;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess;
public class ProductosDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public ProductosDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public async Task<List<Producto>> ListarProductosAsync ()
    {
        return await _sistemaGestionContext.Productos
                            .Include(p => p.Usuario)
                            .Select(p => ProductoDTORespuesta.Crear(p))
                            .ToListAsync();
    }

    public async Task<List<Producto>> ListarProductosAsync (int usuarioId)
    {
        return await _sistemaGestionContext.Productos
                            .Include(p => p.Usuario)
                            .Where(p => p.Usuario.Id == usuarioId)
                            .Select(p => ProductoDTORespuesta.Crear(p))
                            .ToListAsync();
    }

    public async Task<Producto> ObtenerProductoAsync (int id)
    {
        Producto? producto = await _sistemaGestionContext.Productos
                                        .Include(p => p.Usuario)
                                        .FirstOrDefaultAsync(p => p.Id == id);

        if (producto is null)
        {
            throw new ArgumentException($"No existe el producto con id {id}");
        }

        Producto productoDTO = ProductoDTORespuesta.Crear(producto);

        return productoDTO;
    }

    public async Task<List<Producto>> ObtenerProductosAsync (List<int> ids)
    {
        List<Producto> productos = await _sistemaGestionContext.Productos
                                                .Include(p => p.Usuario)
                                                .Where(p => ids.Contains(p.Id))
                                                .ToListAsync();

        return productos;
    }

    public async Task CrearProductoAsync (Producto producto)
    {
        await _sistemaGestionContext.Productos.AddAsync(producto);
        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task ModificarProductoAsync (
        int productoId,
        int usuarioId,
        Producto productoActualizado)
    {
        Producto producto = await ObtenerProductoAsync(productoId);

        if (producto.Usuario.Id != usuarioId)
        {
            throw new ArgumentException($"El producto no corresponde al usuario con id {usuarioId}");
        }

        producto.Descripcion = productoActualizado.Descripcion;
        producto.Costo = productoActualizado.Costo;
        producto.PrecioVenta = productoActualizado.PrecioVenta;
        producto.Stock = productoActualizado.Stock;

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task ModificarProductosAsync ()
    {
        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarProductoAsync (int productoId, int usuarioId)
    {
        Producto producto = await ObtenerProductoAsync(productoId);

        if (producto.Usuario.Id != usuarioId)
        {
            throw new ArgumentException($"El producto no corresponde al usuario con id {usuarioId}");
        }

        _sistemaGestionContext.Productos.Remove(producto);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}

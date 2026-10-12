using Microsoft.EntityFrameworkCore;
using MiTiendaPOS.Data;
using MiTiendaPOS.Dtos;
using MiTiendaPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiTiendaPOS.Services
{
    public class VentaService
    {
        
        public List<Cliente> ListarClientes()
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Clientes.AsNoTracking().OrderBy(c => c.Nombre).ToList();
        }

        public List<Usuario> ListarUsuarios()
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Usuarios.AsNoTracking().OrderBy(u => u.NombreUsuario).ToList();
        }

        public List<Producto> ListarProductosActivos()
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Productos.AsNoTracking()
                      .Where(p => p.Activo)
                      .OrderBy(p => p.Nombre)
                      .ToList();
        }

        
        public List<Venta> ListarVentas()
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Ventas.AsNoTracking()
                      .Include(v => v.Cliente)         
                      .OrderByDescending(v => v.Fecha)
                      .ToList();
        }

        
        public Venta? ObtenerConDetalle(int id)
        {
            using var ctx = new MiTiendaPOSContext();
            return ctx.Ventas.AsNoTracking()
                      .Include(v => v.Detalles)         
                          .ThenInclude(d => d.Producto) 
                      .FirstOrDefault(v => v.Id == id);
        }

        
        public int Registrar(int clienteId, int usuarioId,
                             IEnumerable<LineaVenta> lineas)
        {
            var lista = lineas.ToList();
            if (lista.Count == 0)
                throw new InvalidOperationException("La venta no tiene productos.");

            using var ctx = new MiTiendaPOSContext();
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                ClienteId = clienteId,     
                UsuarioId = usuarioId      
            };

            venta.Detalles.AddRange(CrearDetalles(ctx, lista));
            venta.Total = venta.Detalles.Sum(d => d.Subtotal);

            ctx.Ventas.Add(venta);   
            ctx.SaveChanges();       
            return venta.Id;         
        }

        
        public void Actualizar(int ventaId, int clienteId, int usuarioId,
                               IEnumerable<LineaVenta> lineas)
        {
            var lista = lineas.ToList();
            if (lista.Count == 0)
                throw new InvalidOperationException("La venta no tiene productos.");

            using var ctx = new MiTiendaPOSContext();
            Venta venta = ctx.Ventas.Include(v => v.Detalles)
                                    .FirstOrDefault(v => v.Id == ventaId)
                ?? throw new InvalidOperationException("La venta ya no existe.");

            
            var anteriores = venta.Detalles.ToList();
            foreach (var d in anteriores)
            {
                Producto producto = ctx.Productos.Find(d.ProductoId)!;
                producto.Stock += d.Cantidad;
            }
            ctx.DetallesVenta.RemoveRange(anteriores);

            
            var nuevos = CrearDetalles(ctx, lista);
            foreach (var d in nuevos) d.VentaId = venta.Id;
            ctx.DetallesVenta.AddRange(nuevos);

          
            venta.ClienteId = clienteId;
            venta.UsuarioId = usuarioId;
            venta.Total = nuevos.Sum(d => d.Subtotal);

            ctx.SaveChanges(); 
        }

        
        public void Eliminar(int ventaId)
        {
            using var ctx = new MiTiendaPOSContext();
            Venta venta = ctx.Ventas.Include(v => v.Detalles)
                                    .FirstOrDefault(v => v.Id == ventaId)
                ?? throw new InvalidOperationException("La venta ya no existe.");

            foreach (var d in venta.Detalles)
            {
                Producto? producto = ctx.Productos.Find(d.ProductoId);
                if (producto != null) producto.Stock += d.Cantidad;
            }

            ctx.Ventas.Remove(venta);   
            ctx.SaveChanges();
        }

       
        private static List<DetalleVenta> CrearDetalles(MiTiendaPOSContext ctx,
                                                        List<LineaVenta> lineas)
        {
            var detalles = new List<DetalleVenta>();
            foreach (var linea in lineas)
            {
                Producto producto = ctx.Productos.Find(linea.ProductoId)
                    ?? throw new InvalidOperationException("Producto no encontrado.");

                if (producto.Stock < linea.Cantidad)
                    throw new InvalidOperationException(
                        $"Stock insuficiente de '{producto.Nombre}'. " +
                        $"Disponible: {producto.Stock}.");

                producto.Stock -= linea.Cantidad;   

                detalles.Add(new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Cantidad = linea.Cantidad,
                    PrecioUnitario = producto.PrecioUnitario,   
                    Subtotal = linea.Cantidad * producto.PrecioUnitario
                });
            }
            return detalles;
        }
    }
}

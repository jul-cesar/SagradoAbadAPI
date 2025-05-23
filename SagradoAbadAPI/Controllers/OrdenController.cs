using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SagradoAbadAPI.Contexto;
using SagradoAbadAPI.DTOs.Ordenes;
using SagradoAbadAPI.DTOs.Productos;
using SagradoAbadAPI.Modelos;

namespace SagradoAbadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController(ContextoDb db) : ControllerBase
    {

        [HttpGet("{userId}")]

        public async Task<ActionResult<List<OrdenDTO>>> ObtenerOrdenes(string userId)
        {
            try
            {
                var ordenes = await db.OrdenesCompra
                    .Where(o => o.UsuarioId == userId)
                    .Include(o => o.DetalleOrdenes)
                        .ThenInclude(od => od.Producto)
                    .Select(o => new OrdenDTO
                    {
                        Id = o.IdOrden,
                        UsuarioId = o.UsuarioId,
                        FechaOrden = o.FechaOrden,
                        Total = o.Total,
                        MetodoPago = o.MetodoPago,
                        EstadoEnvio = o.EstadoEnvio,
                        Detalles= o.DetalleOrdenes.Select(od => new DetalleOrdenDTO
                        {
                            Id = od.Id,
                          
                            Cantidad = od.Cantidad,
                            PrecioUnitario = od.PrecioUnitario,
                           Producto = new ProductoDTO
                           {
                               Id = od.Producto.Id,
                               NombreProducto = od.Producto.Nombre,
                               Precio = od.Producto.Precio,
                               Descripcion = od.Producto.Descripcion,
                               ImagenPrincipal = od.Producto.ImagenPrincipal,
                               Categoria = od.Producto.Categoria
                               
                           }
                        }).ToList()

                    })
                    .ToListAsync();
                if (ordenes == null || !ordenes.Any())
                    return NotFound(new { mensaje = "No se encontraron órdenes para este usuario." });
                return Ok(ordenes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrdenDTO>> CrearOrden(CrearOrdenDTO ordenData)
        {
            try
            {
               
                var carrito = await db.Carritos
                    .Include(c => c.Detalles)
                        .ThenInclude(cd => cd.Producto)
                    .FirstOrDefaultAsync(c => c.UsuarioId == ordenData.UsuarioId);

                if (carrito == null || !carrito.Detalles.Any())
                    return BadRequest(new { mensaje = "El carrito está vacío o no existe." });

            
                var orden = new OrdenCompra
                {
                    EstadoEnvio = ordenData.EstadoEnvio,
                    MetodoPago = ordenData.MetodoPago,
                    UsuarioId = ordenData.UsuarioId,
                    FechaOrden = DateTime.Now,
                    DetalleOrdenes = new List<OrdenDetalle>()
                };

                decimal total = 0;

       
                foreach (var item in carrito.Detalles)
                {
                    var detalle = new OrdenDetalle
                    {
                        ProductoId = item.ProductoId,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.Producto.Precio
                    };

                    total += detalle.Cantidad * detalle.PrecioUnitario;
                    orden.DetalleOrdenes.Add(detalle);
                }

                orden.Total = total;

               
                await db.OrdenesCompra.AddAsync(orden);

                db.CarritoDetalles.RemoveRange(carrito.Detalles);

                await db.SaveChangesAsync();

                var ordenResponse = new OrdenDTO
                {
                    Id = orden.IdOrden,
                    UsuarioId = orden.UsuarioId,
                    FechaOrden = orden.FechaOrden,
                    Total = orden.Total,
                    MetodoPago = orden.MetodoPago,
                    EstadoEnvio = orden.EstadoEnvio
                };

                return Ok(ordenResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}

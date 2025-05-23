using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SagradoAbadAPI.Contexto;
using SagradoAbadAPI.DTOs.Ordenes;
using SagradoAbadAPI.Modelos;

namespace SagradoAbadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController(ContextoDb db) : ControllerBase
    {
     
        [HttpPost]
        public async Task<ActionResult<OrdenDTO>> CrearOrden(CrearOrdenDTO ordenData)
        {
            try
            {
                // 1. Obtener el carrito con productos del usuario
                var carrito = await db.Carritos
                    .Include(c => c.Detalles)
                        .ThenInclude(cd => cd.Producto)
                    .FirstOrDefaultAsync(c => c.UsuarioId == ordenData.UsuarioId);

                if (carrito == null || !carrito.Detalles.Any())
                    return BadRequest(new { mensaje = "El carrito está vacío o no existe." });

                // 2. Crear la orden
                var orden = new OrdenCompra
                {
                    EstadoEnvio = ordenData.EstadoEnvio,
                    MetodoPago = ordenData.MetodoPago,
                    UsuarioId = ordenData.UsuarioId,
                    FechaOrden = DateTime.Now,
                    DetalleOrdenes = new List<OrdenDetalle>()
                };

                decimal total = 0;

                // 3. Crear detalles de la orden basados en productos del carrito
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

                // 4. Agregar orden y guardar
                await db.OrdenesCompra.AddAsync(orden);

                // 5. Vaciar carrito (eliminar detalles)
                db.CarritoDetalles.RemoveRange(carrito.Detalles);

                await db.SaveChangesAsync();

                // 6. Preparar respuesta DTO (puedes adaptarlo a tu DTO)
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SagradoAbadAPI.Contexto;
using SagradoAbadAPI.DTOs.Carritos;
using SagradoAbadAPI.DTOs.Productos;
using SagradoAbadAPI.Modelos;

namespace SagradoAbadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController(ContextoDb db) : ControllerBase
    {
        [HttpDelete("{detalleId}")]
        public async Task<ActionResult<string>>  EliminarCarritoDetalle(string detalleId)
        {
            try
            {
                var detalle = await db.CarritoDetalles.FirstOrDefaultAsync(d => d.IdDetalle == detalleId);
                if (detalle == null)
                {
                    return NotFound("Detalle no encontrado");
                }
                db.CarritoDetalles.Remove(detalle);
                await db.SaveChangesAsync();
                return Ok("Producto eliminado del carrito");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addProducto")]
        public async Task<ActionResult<string>> AddProductoToCarrito(NuevoCarritoDetalleDTO carritoDetalleData)
        {
            try
            {
                var carritoExist = await db.Carritos.FirstOrDefaultAsync(c => c.Id == carritoDetalleData.CarritoId);
                if (carritoExist == null)
                {
                    return BadRequest("El carrito no existe");
                }
                var productoExist = await db.Productos.FirstOrDefaultAsync(p => p.Id == carritoDetalleData.ProductoId);
                if (productoExist == null)
                {
                    return BadRequest("El producto no existe");
                }
                carritoDetalleData.Cantidad = carritoDetalleData.Cantidad <= 0 ? 1 : carritoDetalleData.Cantidad;
                var carritoDetalle = new CarritoDetalle
                {
                    CarritoId = carritoDetalleData.CarritoId,
                    ProductoId = carritoDetalleData.ProductoId,
                    Cantidad = carritoDetalleData.Cantidad
                };
                await db.CarritoDetalles.AddAsync(carritoDetalle);
                await db.SaveChangesAsync();
                return Ok("Producto agregado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{detalleId}")]
        public async Task<ActionResult<string>> ActualizarCarritoDetalleCantidad(string detalleId, [FromBody] int nuevaCantidad)
        {
            try
            {
                var detalle = await db.CarritoDetalles.FirstOrDefaultAsync(d => d.IdDetalle == detalleId);
                if (detalle == null)
                {
                    return NotFound("Detalle no encontrado");
                }

                detalle.Cantidad = nuevaCantidad;
                await db.SaveChangesAsync();

                return Ok("Cantidad actualizada");
            }
            catch (Exception)
            {
                return BadRequest("Hubo un error al actualizar el detalle del carrito");
            }
        }

       


        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarrito(string userId)
        {
            try
            {
                var userExist = await db.Usuarios.FirstOrDefaultAsync(u => u.Id == userId);
                if (userExist == null)
                {
                    return NotFound("User no encontrado");
                }
                var carritoDto = await db.Carritos
                 .Where(c => c.UsuarioId == userId)
                 .Select(c => new CarritoDto
                 {
                     Id = c.Id,
                     UsuarioId = c.UsuarioId,
                     Detalles = c.Detalles.Select(d => new CarritoDetalleDto
                     {
                         IdDetalle = d.IdDetalle,
                         Cantidad = d.Cantidad,
                         Producto = new ProductoDTO
                         {
                             Id = d.Producto.Id,
                             NombreProducto = d.Producto.Nombre,
                             Precio = d.Producto.Precio,
                             Descripcion = d.Producto.Descripcion,
                             ImagenPrincipal = d.Producto.ImagenPrincipal,
                             Categoria = d.Producto.Categoria
                         }
                     }).ToList()
                 })
     .FirstOrDefaultAsync();

                return Ok(carritoDto);
            }
            catch (Exception e)
            {
                return BadRequest("Hubo un error obteniendo el carrito");
            }
        }

        [HttpPost("createCarrito")]
        public async Task<ActionResult<CarritoDto>> createCarrito(NuevoCarritoDTO carritoData)
        {
            try
            {
                var carritoExist = await db.Carritos.FirstOrDefaultAsync(c => c.UsuarioId == carritoData.UsuarioId);
                if (carritoExist != null)
                {
                    return BadRequest("Ya existe un carrito para este usuario");
                }
                var newCarrito = new Carrito
                {
                    UsuarioId = carritoData.UsuarioId,
                };
                await db.Carritos.AddAsync(newCarrito);
                await db.SaveChangesAsync();
                return Ok(newCarrito);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
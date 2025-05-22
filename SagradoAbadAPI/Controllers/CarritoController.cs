using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SagradoAbadAPI.Contexto;
using SagradoAbadAPI.DTOs.Carritos;

using SagradoAbadAPI.Modelos;

namespace SagradoAbadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController(ContextoDb db) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<CarritoDTO>> createCarrito(NuevoCarritoDTO carritoData)
        {
            try
            {
                var carritoExist = await db.Carritos.FirstOrDefaultAsync(c => c.UsuarioId == carritoData.UsuarioId);
                if(carritoExist != null) {
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

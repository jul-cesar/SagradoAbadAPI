using Microsoft.AspNetCore.Mvc;
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
                var orden = new OrdenCompra
                {
                    EstadoEnvio = ordenData.EstadoEnvio,
                    MetodoPago = ordenData.MetodoPago,
                    Total = ordenData.Total,
                    UsuarioId = ordenData.UsuarioId,
                    
                };
                var ordenResponse = new OrdenDTO
                {
                    Id = orden.IdOrden,
                    UsuarioId = orden.UsuarioId,
                    FechaOrden = orden.FechaOrden,
                    Total = orden.Total,
                    MetodoPago = orden.MetodoPago,
                    EstadoEnvio = orden.EstadoEnvio
                };
                await db.OrdenesCompra.AddAsync(orden);
                await db.SaveChangesAsync();

                return Ok(orden);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
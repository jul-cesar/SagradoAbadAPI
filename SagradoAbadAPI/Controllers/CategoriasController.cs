
using Microsoft.AspNetCore.Mvc;
using SagradoAbadAPI.Contexto;
using SagradoAbadAPI.DTOs.Categorias;
using SagradoAbadAPI.Modelos;

namespace SagradoAbadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController(ContextoDb db) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Categoria>> CrearCategoria(CrearCategoriaDTO categoria)
        {
            var nuevaCategoria = new Categoria
            {
                NombreCategoria = categoria.NombreCategoria
            };
            await db.Categoria.AddAsync(nuevaCategoria);
            await db.SaveChangesAsync();
            return Ok(nuevaCategoria);
        }


    }
}
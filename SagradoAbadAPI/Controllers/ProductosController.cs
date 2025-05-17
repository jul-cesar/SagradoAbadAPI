
using Microsoft.AspNetCore.Mvc;
using SagradoAbadAPI.Contexto;
using SagradoAbadAPI.DTOs.Productos;
using SagradoAbadAPI.Modelos;

namespace SagradoAbadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController(ContextoDb db) : ControllerBase
    {

        [HttpPost]
        
        public async Task<ActionResult> CrearProducto(CrearProductoDTO producto)
        {
            var NuevoProducto = new Producto
            {
                Nombre = producto.NombreProducto,
                CategoriaId = producto.CategoriaId,
                Descripcion = producto.Descripcion,
                ImagenPrincipal = producto.ImagenPrincipal,
                Precio = producto.Precio,
            };
            var ProductoResponse = new ProductoDTO
            {
                Id = NuevoProducto.Id,
                NombreProducto = NuevoProducto.Nombre,
                Descripcion = NuevoProducto.Descripcion,
                ImagenPrincipal = NuevoProducto.ImagenPrincipal,
                Precio = NuevoProducto.Precio,
                CategoriaId = NuevoProducto.CategoriaId,
            };
            await db.Productos.AddAsync(NuevoProducto);
            await db.SaveChangesAsync();
            return Ok(ProductoResponse);   
        }
    }
}

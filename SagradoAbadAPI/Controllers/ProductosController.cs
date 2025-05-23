using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            try
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
                    Categoria = NuevoProducto.Categoria,
                };
                await db.Productos.AddAsync(NuevoProducto);
                await db.SaveChangesAsync();
                return Ok(ProductoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al crear el producto", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> ObtenerProductos()
        {
            try
            {
                var productos = await db.Productos.Include(p => p.Categoria).ToListAsync();
                var productosResponse = new List<ProductoDTO>();
                foreach (var producto in productos)
                {
                    var productoResponse = new ProductoDTO
                    {
                        Id = producto.Id,
                        NombreProducto = producto.Nombre,
                        Descripcion = producto.Descripcion,
                        ImagenPrincipal = producto.ImagenPrincipal,
                        Precio = producto.Precio,
                     
                        Categoria = producto.Categoria, 
                    };
                    productosResponse.Add(productoResponse);
                }
                return Ok(productosResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener los productos", error = ex.Message });
            }
        }
    }
}
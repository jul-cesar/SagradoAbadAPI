using SagradoAbadAPI.Modelos;

namespace SagradoAbadAPI.DTOs.Productos
{
    public class ProductoDTO
    {
        public string Id { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
     
        public Categoria Categoria  { get; set; }  
        public string ImagenPrincipal { get; set; }
    }
}
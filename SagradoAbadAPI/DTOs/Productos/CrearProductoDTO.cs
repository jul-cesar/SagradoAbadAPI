namespace SagradoAbadAPI.DTOs.Productos
{
    public class CrearProductoDTO
    {
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string CategoriaId { get; set; }
        public string ImagenPrincipal { get; set; }
    }
}

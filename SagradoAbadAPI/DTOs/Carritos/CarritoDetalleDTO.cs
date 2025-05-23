using SagradoAbadAPI.DTOs.Productos;

namespace SagradoAbadAPI.DTOs.Carritos
{
    public class CarritoDetalleDto
    {
        public string IdDetalle { get; set; }
        public int Cantidad { get; set; }
        public ProductoDTO Producto { get; set; }
    }
}

using SagradoAbadAPI.Modelos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SagradoAbadAPI.DTOs.Productos;

namespace SagradoAbadAPI.DTOs.Ordenes
{
    public class DetalleOrdenDTO
    {
      
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("OrdenCompra")]

        [JsonIgnore]
        public OrdenCompra OrdenCompra { get; set; }

        

        public ProductoDTO Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}

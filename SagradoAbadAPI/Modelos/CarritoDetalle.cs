using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SagradoAbadAPI.Modelos
{
    public class CarritoDetalle
    {
        [Key]
        public string IdDetalle { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Carrito")]
        public string CarritoId { get; set; }
        [JsonIgnore]
        public Carrito Carrito { get; set; }

        [ForeignKey("Producto")]
        public string ProductoId { get; set; }
        
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
    }
}
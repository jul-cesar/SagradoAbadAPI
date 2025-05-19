using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SagradoAbadAPI.Modelos
{
    public class CarritoDetalle
    {
        [Key]
        public string IdDetalle { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Carrito")]
        public string CarritoId { get; set; }

        public Carrito Carrito { get; set; }

        [ForeignKey("Producto")]
        public string ProductoId { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SagradoAbadAPI.Modelos
{
    public class OrdenDetalle
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("OrdenCompra")]
        public string OrdenId { get; set; }
        public OrdenCompra OrdenCompra { get; set; }

        [ForeignKey("Producto")]
        public string ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SagradoAbadAPI.Modelos
{
    public class OrdenCompra
    {
        [Key]
        public string IdOrden { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime FechaOrden { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(10,3)")]
        public decimal Total { get; set; }

        public string MetodoPago { get; set; }

        public string EstadoEnvio { get; set; }

        public ICollection<OrdenDetalle> DetalleOrdenes { get; set; }
    }
}
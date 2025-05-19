using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SagradoAbadAPI.Modelos
{
    public class Producto
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(150)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal Precio { get; set; }

        [ForeignKey("Categoria")]
        public string? CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public string ImagenPrincipal { get; set; }

        public ICollection<ImagenProducto> Imagenes { get; set; }

        [JsonIgnore]
        public ICollection<CarritoDetalle> CarritoDetalles { get; set; }

        [JsonIgnore]
        public ICollection<OrdenDetalle> DetalleOrdenes { get; set; }
    }
}
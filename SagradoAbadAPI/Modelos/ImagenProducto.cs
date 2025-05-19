using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SagradoAbadAPI.Modelos
{
    public class ImagenProducto
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Producto")]
        public string ProductoId { get; set; }

        public string UrlImagen { get; set; }
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

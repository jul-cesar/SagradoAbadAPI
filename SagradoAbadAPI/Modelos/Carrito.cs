using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SagradoAbadAPI.Modelos
{
    public class Carrito
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario Usuario { get; set; }

        public ICollection<CarritoDetalle> Detalles { get; set; }
    }
}
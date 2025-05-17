using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SagradoAbadAPI.Modelos
{
    public class Categoria
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(100)]
        public string NombreCategoria { get; set; }
        [JsonIgnore]
        public ICollection<Producto> Productos { get; set; }
    }
}

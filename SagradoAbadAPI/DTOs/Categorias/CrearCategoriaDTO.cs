using System.ComponentModel.DataAnnotations;

namespace SagradoAbadAPI.DTOs.Categorias
{
    public class CrearCategoriaDTO
    {
        [Required]
        public string NombreCategoria { get; set; }
    }
}

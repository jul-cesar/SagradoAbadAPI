using SagradoAbadAPI.Modelos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SagradoAbadAPI.DTOs.Carritos
{
    public class CarritoDto
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public List<CarritoDetalleDto> Detalles { get; set; }
    }
}

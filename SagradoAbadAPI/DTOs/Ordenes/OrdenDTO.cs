using System.ComponentModel.DataAnnotations.Schema;

namespace SagradoAbadAPI.DTOs.Ordenes
{
    public class OrdenDTO
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }

        public DateTime FechaOrden { get; set; } = DateTime.Now;

       
        public decimal Total { get; set; }

        public string MetodoPago { get; set; }

        public string EstadoEnvio { get; set; }
    }
}
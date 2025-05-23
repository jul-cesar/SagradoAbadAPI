using System.ComponentModel.DataAnnotations.Schema;

namespace SagradoAbadAPI.DTOs.Ordenes
{
    public class CrearOrdenDTO
    {
        public string UsuarioId { get; set; }

      

    

        public string MetodoPago { get; set; }

        public string EstadoEnvio { get; set; }
    }
}
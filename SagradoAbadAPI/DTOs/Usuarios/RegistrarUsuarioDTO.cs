using SagradoAbadAPI.Modelos;
using System.ComponentModel.DataAnnotations;

namespace SagradoAbadAPI.DTOs.Usuarios
{
    public class RegistrarUsuarioDTO
    {
      
        public string Nombre { get; set; }

       
        public string CorreoElectronico { get; set; }


        public string Password { get; set; }

      
    }
}

using SagradoAbadAPI.Modelos;
using static SagradoAbadAPI.Modelos.Usuario;
using System.ComponentModel.DataAnnotations;

namespace SagradoAbadAPI.DTOs.Usuarios
{
    public class UsuarioDTO

    {
        public string Id { get; set; }


        public string Nombre { get; set; }


        public string CorreoElectronico { get; set; }


        public string Password { get; set; }

        public DateTime FechaRegistro { get; set; }



        public string Rol { get; set; }

    }
}

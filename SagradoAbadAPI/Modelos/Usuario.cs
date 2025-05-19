using System.ComponentModel.DataAnnotations;

namespace SagradoAbadAPI.Modelos
{
    public class Usuario
    { 


        public enum RolUsuario
    {
        Administrador,
        Cliente
    }

    [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required, MaxLength(100)]
        public string CorreoElectronico { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public Carrito Carrito { get; set; }

        public RolUsuario Rol { get; set; } = RolUsuario.Cliente;

        public ICollection<OrdenCompra> Ordenes { get; set; }
    }
}
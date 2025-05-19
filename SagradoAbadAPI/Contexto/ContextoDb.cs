using Microsoft.EntityFrameworkCore;

namespace SagradoAbadAPI.Contexto
{
    public class ContextoDb : DbContext
    {
        public ContextoDb(DbContextOptions<ContextoDb> options) : base(options)
        {
        }

        public DbSet<Modelos.Usuario> Usuarios { get; set; }
        public DbSet<Modelos.Producto> Productos { get; set; }
        public DbSet<Modelos.OrdenCompra> OrdenesCompra { get; set; }
        public DbSet<Modelos.OrdenDetalle> OrdenDetalles { get; set; }
        public DbSet<Modelos.Carrito> Carritos { get; set; }
        public DbSet<Modelos.CarritoDetalle> CarritoDetalles { get; set; }

        public DbSet<Modelos.ImagenProducto> ImagenesProductos
        {
            get; set;
        }

        public DbSet<SagradoAbadAPI.Modelos.Categoria> Categoria { get; set; } = default!;
    }
}
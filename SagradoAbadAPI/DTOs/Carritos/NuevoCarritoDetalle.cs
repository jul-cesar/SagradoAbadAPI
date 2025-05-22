using SagradoAbadAPI.Modelos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SagradoAbadAPI.DTOs.Carritos
{
    public class NuevoCarritoDetalle
    {
      
      
        public string CarritoId { get; set; }

    
        public string ProductoId { get; set; }

       

        public int Cantidad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.api.CarritoCompras.Modelo
{
    public class CarritoDetalle
    {
        public int CarritoDetalleId { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }
        
        public string ProductoId { get; set; }
    }
}

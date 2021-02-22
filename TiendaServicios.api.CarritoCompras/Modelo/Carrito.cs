using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.api.CarritoCompras.Modelo
{
    public class Carrito
    {
        public int CarritoId { get; set; }
        public DateTime FechaCreacion { get; set; }

        public ICollection<CarritoDetalle> ListaDetalle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.api.CarritoCompras.Aplicacion
{
    public class CarritoDTO
    {
        public int CarritoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<CarritoDetalleDTO> ListaProductos { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.CarritoCompras.Modelo;

namespace TiendaServicios.api.CarritoCompras.Persistencia
{
    public class ContextoCarrito: DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> options): base(options)
        {

        }

        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<CarritoDetalle> CarritoDetalle { get; set; }
    }
}

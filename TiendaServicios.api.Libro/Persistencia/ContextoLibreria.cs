using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.Libro.Modelo;

namespace TiendaServicios.api.Libro.Persistencia
{
    public class ContextoLibreria: DbContext
    {
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options):base(options)
        {

        }

        public DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.Gateway.LibroRemote;

namespace TiendaServicios.api.Gateway
{
    public interface ILibroService
    {
        Task<(bool resultado, AutorRemoteModel autor)> GetAutor(Guid AutorId);
    }
}

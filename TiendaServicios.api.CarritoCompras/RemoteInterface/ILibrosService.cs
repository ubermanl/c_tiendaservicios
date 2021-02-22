using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.CarritoCompras.RemoteModel;

namespace TiendaServicios.api.CarritoCompras.RemoteInterface
{
    public interface ILibrosService
    {
        Task<(bool resultado, LibroRemote libro, string errorMessage)> GetLibro(Guid libroId);
    }
}

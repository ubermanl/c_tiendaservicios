using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.Gateway.LibroRemote;

namespace TiendaServicios.api.Gateway.InterfaceRemote
{
    public interface IAutorRemote
    {
        Task<(bool resultado, AutorRemoteModel autor, string message)> GetAutor(Guid autorId);
    }
}

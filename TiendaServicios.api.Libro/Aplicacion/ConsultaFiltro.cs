using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.Libro.Modelo;
using TiendaServicios.api.Libro.Persistencia;

namespace TiendaServicios.api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico: IRequest<LibroDTO>
        {
            public  Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroDTO>
        {
            private IMapper _mapper;
            private ContextoLibreria _contexto;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _mapper = mapper;
                _contexto = contexto;
            }

            public async Task<LibroDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
               
                if (libro == null)
                    throw new System.Exception("Libro no encontrado");

                var libroDTO = _mapper.Map<LibreriaMaterial, LibroDTO>(libro);
                return libroDTO;
            }
        }
    }
}

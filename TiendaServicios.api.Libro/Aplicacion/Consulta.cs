using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.Libro.Modelo;
using TiendaServicios.api.Libro.Persistencia;

namespace TiendaServicios.api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<List<LibroDTO>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroDTO>>
        {
            private IMapper _mapper;
            private ContextoLibreria _contexto;

            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _mapper = mapper;
                _contexto = contexto;
            }

            public async Task<List<LibroDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.ToListAsync();
                var librosDTO = _mapper.Map<List<LibreriaMaterial>, List<LibroDTO>>(libros);
                return librosDTO;
            }
        }
    }
}

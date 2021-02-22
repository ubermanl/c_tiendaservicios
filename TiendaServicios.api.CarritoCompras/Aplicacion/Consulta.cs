using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.CarritoCompras.Persistencia;
using TiendaServicios.api.CarritoCompras.RemoteInterface;

namespace TiendaServicios.api.CarritoCompras.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDTO>
        {
            public int CarritoId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly ContextoCarrito _contexto;
            private ILibrosService _librosService;
            

            public Manejador(ContextoCarrito contexto, ILibrosService librosService)
            {
                _contexto = contexto;
                _librosService = librosService;

            }

            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carrito = await _contexto.Carrito.FirstOrDefaultAsync(x => x.CarritoId == request.CarritoId);
                var carritoDetalle = await _contexto.CarritoDetalle.Where(x => x.CarritoId == request.CarritoId).ToListAsync();

                var listaCarrito = new List<CarritoDetalleDTO>();

                foreach (var libro in carritoDetalle)
                {
                    var response = await _librosService.GetLibro(new Guid(libro.ProductoId));
                    if (response.resultado)
                    {
                        var objetoLibro = response.libro;
                        var carritoDetalleDto = new CarritoDetalleDTO()
                        {
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };
                        listaCarrito.Add(carritoDetalleDto);
                    }
                }

                var carritoDto = new CarritoDTO()
                {
                    CarritoId = carrito.CarritoId,
                    FechaCreacion = carrito.FechaCreacion,
                    ListaProductos = listaCarrito
                };

                return carritoDto;
            }
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.CarritoCompras.Modelo;
using TiendaServicios.api.CarritoCompras.Persistencia;

namespace TiendaServicios.api.CarritoCompras.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {
            public DateTime FechaCreacion { get; set; }
            public List<string> ProductoLista { get; set; }
        }


        public class Manejador: IRequestHandler<Ejecuta>
        {
            private readonly ContextoCarrito _contexto;

            public Manejador(ContextoCarrito contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new Carrito
                {
                    FechaCreacion = request.FechaCreacion
                };

                _contexto.Carrito.Add(carritoSesion);
                var value = await _contexto.SaveChangesAsync();
                if (value == 0)
                    throw new Exception("Error al guardar carrito");

                int id = carritoSesion.CarritoId;

                foreach (var item in request.ProductoLista)
                {
                    var detalle = new CarritoDetalle { CarritoId = id, ProductoId = item, FechaCreacion = DateTime.Now };

                    _contexto.CarritoDetalle.Add(detalle);
                }
                var result = await _contexto.SaveChangesAsync();

                if (value == 0)
                    throw new Exception("Error al guardar items del carrito");

                return Unit.Value;
            }
        }
    }
}

using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.Libro.Modelo;
using TiendaServicios.api.Libro.Persistencia;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IRabbitEventBus _eventBus;

            public Manejador(ContextoLibreria contexto, IRabbitEventBus eventBus)
            {
                _contexto = contexto;
                _eventBus = eventBus;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial { AutorLibro = request.AutorLibro, FechaPublicacion = request.FechaPublicacion, Titulo = request.Titulo };
                _contexto.LibreriaMaterial.Add(libro);
                var result = await _contexto.SaveChangesAsync();

                _eventBus.Publish(new EmailEventoQueue("pepe@localhost.com", request.Titulo, "Este contenido es un ejemplo"));

                if (result > 0)
                    return Unit.Value;
                else
                    throw new Exception("No se pudo guardar el libro");

                

            }
        }
    }
}

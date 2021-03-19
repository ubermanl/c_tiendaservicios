using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        private readonly ILogger<EmailEventoManejador> _logger;

        public EmailEventoManejador()
        {

        }

        public EmailEventoManejador(ILogger<EmailEventoManejador> logger)
        {
            _logger = logger;
        }

        public Task Hande(EmailEventoQueue @event)
        {
            _logger.LogInformation($"Este el valor consumido desde RBMQ {@event.Titulo}");
            return Task.CompletedTask;
        }
    }
}

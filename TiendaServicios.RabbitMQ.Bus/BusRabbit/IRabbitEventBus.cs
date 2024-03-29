﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.RabbitMQ.Bus.Comandos;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {
        Task EnviarComando<T>(T comando) where T:Comando;
        void Publish<T>(T evento) where T : Evento;
        void Subscribe<T, Th>() where T : Evento
            where Th : IEventoManejador<T>;

    }
}

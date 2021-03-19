using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace TiendaServicios.RabbitMQ.Bus.Eventos
{
    public abstract class Message: IRequest<bool>
    {
        public string MessageType { get; protected set; }
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}

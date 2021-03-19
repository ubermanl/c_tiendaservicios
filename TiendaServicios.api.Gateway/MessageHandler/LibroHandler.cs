using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.api.Gateway.InterfaceRemote;
using TiendaServicios.api.Gateway.LibroRemote;

namespace TiendaServicios.api.Gateway.MessageHandler
{
    public class LibroHandler: DelegatingHandler
    {
        private readonly ILogger<LibroHandler> _logger;
        private readonly IAutorRemote _autorRemote;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation)
        {
            var tiempo = Stopwatch.StartNew();
            _logger.LogInformation("Inicia el request");
            var response = await base.SendAsync(request, cancellation);
            _logger.LogInformation($"Este proceso se hizo en {tiempo.ElapsedMilliseconds} ms");

            if (response.IsSuccessStatusCode)
            {
                var contenido = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<LibroRemoteModel>(contenido);
                var responseAutor = await _autorRemote.GetAutor(resultado.AutorLibro ?? Guid.Empty);
                if (responseAutor.resultado)
                {
                    var objetoAutor = responseAutor.autor;
                    resultado.AutorData = objetoAutor;
                    var jsonString = JsonSerializer.Serialize(resultado);
                    response.Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
                }
            }

            return response;
        }

        public LibroHandler(ILogger<LibroHandler> logger, IAutorRemote autorRemote)
        {
            _logger = logger;
            _autorRemote = autorRemote;
        }
    }
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.api.Gateway.InterfaceRemote;
using TiendaServicios.api.Gateway.LibroRemote;

namespace TiendaServicios.api.Gateway.ImplementRemote
{
    public class AutorRemote : IAutorRemote
    {

        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<AutorRemote> _logger;

        public async Task<(bool resultado, LibroRemote.AutorRemoteModel autor, string message)> GetAutor(Guid autorId)
        {
            try
            {
                var client = _httpClient.CreateClient("AutorService");
                var response = await client.GetAsync($"Autor/{autorId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<AutorRemoteModel>(contenido,options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public AutorRemote(IHttpClientFactory httpClient, ILogger<AutorRemote> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
    }
}

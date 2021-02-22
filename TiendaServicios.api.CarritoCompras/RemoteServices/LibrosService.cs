using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.CarritoCompras.RemoteInterface;
using TiendaServicios.api.CarritoCompras.RemoteModel;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TiendaServicios.api.CarritoCompras.RemoteServices
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibrosService> _logger;

        public LibrosService(IHttpClientFactory httpClient, ILogger<LibrosService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, LibroRemote libro, string errorMessage)> GetLibro(Guid libroId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/Libro/{libroId}");
                if(response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, string.Empty);
                }
                else
                {
                    return (false, null, response.ReasonPhrase);
                }
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}

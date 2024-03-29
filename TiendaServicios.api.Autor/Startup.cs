using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TiendaServicios.api.Autor.Aplicacion;
using TiendaServicios.api.Autor.ManejadorRabbit;
using TiendaServicios.api.Autor.Persistencia;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Eventos;
using TiendaServicios.RabbitMQ.Bus.Implement;

namespace TiendaServicios.api.Autor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddSingleton<IRabbitEventBus, RabbitEventBus>( sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitEventBus(sp.GetService<IMediator>(), scopeFactory);
            });

            services.AddTransient<EmailEventoManejador>();

            services.AddTransient<IEventoManejador<EmailEventoQueue>, EmailEventoManejador>();
            

            services.AddDbContext<ContextoAutor>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("ConnectionDatabase"));
            });

            services.AddMediatR(typeof(Aplicacion.Nuevo.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var eventBus = app.ApplicationServices.GetRequiredService<IRabbitEventBus>();
            eventBus.Subscribe<EmailEventoQueue, EmailEventoManejador>();
        }
    }
}

using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.api.CarritoCompras.Persistencia;
using Pomelo.EntityFrameworkCore.MySql;
using TiendaServicios.api.CarritoCompras.Aplicacion;
using TiendaServicios.api.CarritoCompras.RemoteInterface;
using TiendaServicios.api.CarritoCompras.RemoteServices;

namespace TiendaServicios.api.CarritoCompras
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
            services.AddControllers(); //.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

            services.AddDbContext<ContextoCarrito>(options => {
                options.UseMySql(Configuration.GetConnectionString("ConexionDB"));
            });

            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

            services.AddHttpClient("Libros", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Libros"]);
            });

            services.AddScoped<ILibrosService,LibrosService>();
            //services.AddAutoMapper(typeof(Consulta.Manejador).Assembly);
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
        }
    }
}

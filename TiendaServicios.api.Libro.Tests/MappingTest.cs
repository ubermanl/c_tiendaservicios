using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaServicios.api.Libro.Aplicacion;
using TiendaServicios.api.Libro.Modelo;

namespace TiendaServicios.api.Libro.Tests
{
    public class MappingTest: Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroDTO>();
        }
    }
}

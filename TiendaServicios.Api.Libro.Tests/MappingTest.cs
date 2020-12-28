using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Tests
{
    class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<Modelo.Libro, LibroDto>();
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibro.Where(p => p.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if(autor == null)
                {
                    throw new Exception("No se encontró el autor");
                }

                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);

                return autorDto;
            }
        }
    }
}

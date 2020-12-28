using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibroDto>
        {
            public Guid LibroGuid { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroDto>
        {
            private readonly ContextoLibro _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibro context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LibroDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _context.Libro.Where(p => p.LibroId == request.LibroGuid).FirstOrDefaultAsync();
                if (libro == null)
                {
                    throw new Exception("No se encontró el libro");
                }

                var libroDto = _mapper.Map<Modelo.Libro, LibroDto>(libro);

                return libroDto;
            }
        }
    }
}

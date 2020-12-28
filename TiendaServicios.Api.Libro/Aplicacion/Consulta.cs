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
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroDto>> { }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroDto>>
        {
            private readonly ContextoLibro _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibro context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<LibroDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _context.Libro.ToListAsync();
                var libroDto = _mapper.Map<List<Modelo.Libro>, List<LibroDto>>(libros);
                return libroDto;
            }
        }
    }
}

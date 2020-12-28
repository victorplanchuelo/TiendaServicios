using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto> {
            public int CarritoSessionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly ContextoCarrito _context;
            private readonly ILibrosService _librosService;

            public Manejador(ContextoCarrito context, ILibrosService librosService)
            {
                _context = context;
                _librosService = librosService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _context.CarritoCompra.FirstOrDefaultAsync(x => x.CarritoCompraId == request.CarritoSessionId);
                var carritoSesionDetalle = await _context.CarritoCompraDetalle.Where(X => X.CarritoCompraId == request.CarritoSessionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDto>();
                
                foreach (var libro in carritoSesionDetalle)
                {
                    var resp = await _librosService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if(resp.resultado)
                    {
                        var objLibro = resp.Libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = objLibro.Titulo,
                            FechaPublicacionLibro = objLibro.FechaPublicacion,
                            LibroId = objLibro.LibroId
                        };

                        listaCarritoDto.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoCompraId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarritoDto
                };

                return carritoSesionDto;
            }
        }
    }
}

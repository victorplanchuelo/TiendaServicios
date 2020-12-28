using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime? FechaCreacion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoCarrito _context;

            public Manejador(ContextoCarrito context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoCompra = new Modelo.CarritoCompra
                {
                    FechaCreacion = request.FechaCreacion
                };

                _context.CarritoCompra.Add(carritoCompra);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("No se pudo insertar los datos en el Carrito de compra");
                }

                int id = carritoCompra.CarritoCompraId;

                foreach (var item in request.ProductoLista)
                {
                    var detalleCompra = new CarritoCompraDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoCompraId = id,
                        ProductoSeleccionado = item
                    };

                    _context.CarritoCompraDetalle.Add(detalleCompra);
                }

                result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar los datos del detalle del Carrito de compra");

            }
        }
    }
}

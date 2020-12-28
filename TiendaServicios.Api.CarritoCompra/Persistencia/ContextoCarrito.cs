using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;

namespace TiendaServicios.Api.CarritoCompra.Persistencia
{
    public class ContextoCarrito : DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> options) : base(options) { }

        public DbSet<Modelo.CarritoCompra> CarritoCompra { get; set; }
        public DbSet<CarritoCompraDetalle> CarritoCompraDetalle { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibro : DbContext
    {
        public ContextoLibro() { }

        public ContextoLibro(DbContextOptions<ContextoLibro> options) : base(options) { }

        public virtual DbSet<Modelo.Libro> Libro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoCompra
    {
        public int CarritoCompraId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public ICollection<CarritoCompraDetalle> ListaDetalle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoCompraDetalle
    {
        public int CarritoCompraDetalleId { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public string ProductoSeleccionado { get; set; }

        public int CarritoCompraId { get; set; }
        public CarritoCompra CarritoCompra { get; set; }
    }
}

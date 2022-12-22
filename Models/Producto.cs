using System;
using System.Collections.Generic;

namespace WebSocket.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleFacturas = new HashSet<DetalleFactura>();
        }

        public int Iidproducto { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Bhabilitado { get; set; }
        public int? Stock { get; set; }
        public byte[] Foto { get; set; }
        public string Nombrefoto { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}

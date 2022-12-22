using System;
using System.Collections.Generic;

namespace WebSocket.Models
{
    public partial class TipoAlergium
    {
        public TipoAlergium()
        {
            FichaMedicas = new HashSet<FichaMedica>();
        }

        public int Iidtipoalergia { get; set; }
        public string Nombretipoalergia { get; set; }
        public string Bhabilitado { get; set; }

        public virtual ICollection<FichaMedica> FichaMedicas { get; set; }
    }
}

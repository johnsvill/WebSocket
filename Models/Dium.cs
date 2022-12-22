using System;
using System.Collections.Generic;

namespace WebSocket.Models
{
    public partial class Dium
    {
        public Dium()
        {
            HoraDiaActividadPersonas = new HashSet<HoraDiaActividadPersona>();
        }

        public int Iiddia { get; set; }
        public string Nombredia { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual ICollection<HoraDiaActividadPersona> HoraDiaActividadPersonas { get; set; }
    }
}

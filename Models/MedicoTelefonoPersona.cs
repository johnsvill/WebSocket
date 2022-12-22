﻿using System;
using System.Collections.Generic;

namespace WebSocket.Models
{
    public partial class MedicoTelefonoPersona
    {
        public int Iidmedicotelefono { get; set; }
        public int? Iidpersona { get; set; }
        public string Numerotelefonicomedico { get; set; }
        public int? Bhabilitado { get; set; }

        public virtual Persona IidpersonaNavigation { get; set; }
    }
}

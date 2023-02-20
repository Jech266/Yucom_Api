using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yucom.Entity;

namespace Yucom.Entity
{
    public class EventoPresentador
    {
        public int EventoId { get; set; }
        public int PresentadorId { get; set; }
        public int Orden { get; set; }
        public Evento Evento {get; set; }
        public Presentador Presentador { get; set; }
    }
}
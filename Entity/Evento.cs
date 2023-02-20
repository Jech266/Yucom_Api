using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yucom.Validaciones;

namespace Yucom.Entity
{
    public class Evento
    {
        public int Id { get; set; }
        [PrimeraLetra]
        public string Nombre { get; set; }
        public string Descripcion {get; set; }
        public string Fotografia {get; set; }
        public DateTime Fecha { get; set; }
        public int Establecimientoid {get; set; }
        public Establecimiento establecimiento { get; set; }
        public List<EventoPresentador> EventoPresentadors { get; set;}

    }
}
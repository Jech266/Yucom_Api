using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yucom.Validaciones;

namespace Yucom.DTOs
{
    public class EventosCreationDTO
    {
        public int Id { get; set; }
        [PrimeraLetra]
        public string Nombre { get; set; }
        public string Descripcion {get; set; }
        public string Fotografia { get; set; }
        public DateTime Fecha { get; set; }
        public int Establecimientoid {get; set; }
        public List<int> PresentadoresIds { get; set; }
    }
}
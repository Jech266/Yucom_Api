using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yucom.Entity
{
    public class Asiento
    {
        public int Id { get; set; }
        [Required]
        public string Numero { get; set; }
        public string Descripcion {get; set;}
        public int EstablecimientoId {get; set; }
        public Establecimiento establecimiento {get; set; }
    }
}
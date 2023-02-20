using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yucom.Validaciones;

namespace Yucom.DTOs
{
    public class PresentadorDTO
    {
        public int Id {get; set;}
        [PrimeraLetra]
        public string Nombre { get; set; }
        [StringLength(maximumLength: 12)]
        public string RFC { get; set; }
        public string Fotografia {get; set; }
        public string Descripcion {get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yucom.Validaciones;

namespace Yucom.DTOs
{
    public class EstablecimientoCreationDTO
    {
        public int Id { get; set; }
        [PrimeraLetra]
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string Imagenes { get; set; }
        public string Descripcion { get; set; }

    }
}
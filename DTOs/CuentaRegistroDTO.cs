using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yucom.Validaciones;

namespace Yucom.DTOs
{
    public class CuentaRegistroDTO : CredencialesUsuario
    {
        [PrimeraLetra]
        public string Nombre { get; set; }
        [PrimeraLetra]
        public string ApellidoPaterno { get; set; }
        [PrimeraLetra]
        public string ApellidoMaterno {get; set;}
        [Required]
        public string Usuario { get; set; }
        public string Telefono { get; set; }
    }
}
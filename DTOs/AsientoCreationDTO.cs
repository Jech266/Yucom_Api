using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yucom.DTOs
{
    public class AsientoCreationDTO
    {
        public int Id {get; set; }
        [Required]
        public string Numero { get; set; }
        public string Descripcion { get; set; }
    }
}
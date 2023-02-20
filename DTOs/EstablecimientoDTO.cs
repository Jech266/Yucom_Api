using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yucom.DTOs
{
    public class EstablecimientoDTO : EstablecimientoCreationDTO
    {
        public List<AsientoCreationDTO> asientoCreationDTOs { get; set; }
    }
}
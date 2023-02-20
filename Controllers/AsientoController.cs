using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Yucom.Entity;
using Microsoft.AspNetCore.Mvc;
using Yucom.DTOs;

namespace Yucom.Controllers
{
    [ApiController]
    [Route("api/establecimiento/{establecimientoId:int}/asientos")]
    public class AsientoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AsientoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<AsientoCreationDTO>>> get(int establecimientoId)
        {
            var ExisteEstablecimiento = await context.Establecimientos.AnyAsync( DbEstablecimiento => DbEstablecimiento.Id == establecimientoId);
            if(!ExisteEstablecimiento)
            {
                return NotFound();
            }
            var asiento = await context.Asientos
                .Where(DbEstablecimiento => DbEstablecimiento.EstablecimientoId == establecimientoId).ToListAsync();
            
            
            return mapper.Map<List<AsientoCreationDTO>>(asiento);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post (int establecimientoId, AsientoCreationDTO asientoCreationDTO)
        {
            var ExisteEstablecimiento = await context.Establecimientos.AnyAsync( DbEstablecimiento => DbEstablecimiento.Id == establecimientoId);
            if(!ExisteEstablecimiento)
            {
                return NotFound();
            }
            
            if(ExisteEstablecimiento)
            {
            var ExisteAsiento = await context.Asientos.Where(y => y.EstablecimientoId == establecimientoId).AnyAsync(x => x.Numero == asientoCreationDTO.Numero);

                if (ExisteAsiento)
                {
                    return BadRequest($"El asiento: {asientoCreationDTO.Numero} ya Existe en la tabla del establecimiento {establecimientoId}");
                }
            }

            var asiento = mapper.Map<Asiento>(asientoCreationDTO);
            asiento.EstablecimientoId = establecimientoId;
            context.Add(asiento);
            await context.SaveChangesAsync();
            return Ok();
        }


        // [HttpPut("{id:int}")]
        // public async Task<ActionResult> Put (Asiento asiento, int id)
        // {
        //     if (asiento.Id != id)
        //     {
        //         return BadRequest("El Id no existe en el sistema");
        //     }

        //     var existe = await context.Asientos.AnyAsync(x => x.Id == id);

        //     if (!existe)
        //     {
        //         return NotFound();
        //     }
        //     context.Update(asiento);
        //     await context.SaveChangesAsync();
        //     return Ok();
        // }
        // [HttpDelete("{id:int}")]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     var existe = await context.Asientos.AnyAsync(x => x.Id == id);
        //     if (!existe)
        //     {
        //         return NotFound();
        //     }
        //     context.Remove(new Asiento(){Id = id});
        //     await context.SaveChangesAsync();
        //     return Ok();
        // }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yucom.DTOs;
using Yucom.Entity;
using Yucom.Utilidades;

namespace Yucom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstablecimientoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public readonly IMapper Mapper;

        public EstablecimientoController(ApplicationDbContext context, IMapper mapper)
        {
            this.Mapper = mapper;
            this.context = context;
        }

        [HttpGet("Paginación")]
        [AllowAnonymous]
        public async Task<ActionResult<List<EstablecimientoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Establecimientos
            .Include(establecimientoDb => establecimientoDb.Asientos)
            .AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var Establecimiento = await queryable.OrderBy(Establecimiento => Establecimiento.Nombre).Paginar(paginacionDTO).ToListAsync();
            return Mapper.Map<List<EstablecimientoDTO>>(Establecimiento);
        }

        [HttpGet("{Nombre}")]
        [AllowAnonymous]
        public async Task<ActionResult<EstablecimientoDTO>> Get (string Nombre)
        {
            var NombreEstablecimiento = await context.Establecimientos
                .Include(establecimientoDb => establecimientoDb.Asientos)
                .FirstOrDefaultAsync(x => x.Nombre.Contains(Nombre));
            
            if (NombreEstablecimiento == null)
            {
                return BadRequest($"El establecimento de nombre {Nombre} no se encuentra en los datos");
            }
             return Mapper.Map<EstablecimientoDTO>(NombreEstablecimiento);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]EstablecimientoCreationDTO establecimientoCreationDTO)
        {
            var existeEstablecimiento = await context.Establecimientos.AnyAsync(x => x.RFC == establecimientoCreationDTO.RFC);

            if (existeEstablecimiento)
            {
                return BadRequest($"El establecimiento: {establecimientoCreationDTO.RFC} ya EXISTE en el sistema");
            }
            var establecimiento = Mapper.Map<Establecimiento>(establecimientoCreationDTO);

            context.Add(establecimiento);
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(EstablecimientoCreationDTO establecimientoCreationDTO, int id)
        {
            if (establecimientoCreationDTO.Id != id)
            {
                return BadRequest("El Id no existe en el sistema");
            }

            var existe = await context.Establecimientos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            var establecimiento =Mapper.Map<Establecimiento>(establecimientoCreationDTO);

            context.Update(establecimiento);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Establecimientos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Establecimiento(){Id = id});
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
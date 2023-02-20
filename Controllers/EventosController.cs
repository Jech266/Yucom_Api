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
    [Route("api/eventos")]
    public class EventosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EventosController(ApplicationDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }
        [HttpGet("Paginación")]
        [AllowAnonymous]
        public async Task<ActionResult<List<EventoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Eventos
            .Include(eventoDb => eventoDb.EventoPresentadors)
            .ThenInclude(eventoPresentador => eventoPresentador.Presentador)
            .AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var Evento = await queryable.OrderBy(Evento => Evento.Nombre).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<EventoDTO>>(Evento);
        }

        [HttpGet("{nombre}")]
        [AllowAnonymous]
        public async Task<ActionResult<EventoDTO>> Get(string nombre)
        {
            var evento = await context.Eventos
                .Include(eventoDb => eventoDb.EventoPresentadors)
                .ThenInclude(eventoPresentador => eventoPresentador.Presentador)
                .FirstOrDefaultAsync(x => x.Nombre == nombre);
            
            return mapper.Map<EventoDTO>(evento);
       }

        [HttpPost("{RFC}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post (string RFC, EventosCreationDTO eventosCreationDTO)
        {
            var ExisteEstablecimiento = await context.Establecimientos.AnyAsync( DbEstablecimiento => DbEstablecimiento.RFC == RFC);
            if(!ExisteEstablecimiento)
            {
                return NotFound();
            }

            if(eventosCreationDTO.PresentadoresIds == null)
            {
                return BadRequest("No se puede agregar un evento sin presentador");
            }

            var autoresIds = await context.Presentadors.Where(PresentadorDb => eventosCreationDTO.PresentadoresIds.Contains(PresentadorDb.Id)).Select(x => x.Id).ToListAsync();

            if(eventosCreationDTO.PresentadoresIds.Count != autoresIds.Count)
            {
                return BadRequest("No existe algunos de los presentadores enviados");
            }

            var existeEstablecimiento = await context.Establecimientos.AnyAsync(x => x.Id == eventosCreationDTO.Id);

            if (!existeEstablecimiento)
            {
                return BadRequest($"No existe el establecimiento {eventosCreationDTO.Id} asignado al evento");
            }

            var existeEvento = await context.Eventos.AnyAsync(x => x.Nombre == eventosCreationDTO.Nombre);

            if(existeEvento)
            {
                return BadRequest("Ya existe el evento");
            }
            var eventos = mapper.Map<Evento>(eventosCreationDTO);

            if(eventos.EventoPresentadors != null)
            {
                for(int i = 0; i < eventos.EventoPresentadors.Count; i++)
                {
                    eventos.EventoPresentadors[i].Orden = i;
                }
            }
            context.Add(eventos);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
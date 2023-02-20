using AutoMapper;
using Yucom.DTOs;
using Yucom.Entity;

namespace Yucom.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AsientoCreationDTO, Asiento>();
            CreateMap<BoletoCreationDTO, Boleto>();
            CreateMap<ClienteCreationDTO, Cliente>();
            CreateMap<CostoCreationDTO, Costo>();
            CreateMap<EstablecimientoCreationDTO, Establecimiento>();
            CreateMap<EventosCreationDTO, Evento>()
                .ForMember(evento => evento.EventoPresentadors, opciones => opciones.MapFrom(MapEventoPresentadores));
            CreateMap<EventoDTO, Evento>();
            CreateMap<Evento, EventoDTO>()
                .ForMember(eventoDTO => eventoDTO.Presentadores, opciones => opciones.MapFrom(MapEventoDTOPresentador));
            CreateMap<PresentadorCreationDTO, Presentador>();
            CreateMap<Presentador, PresentadorDTO>();
            CreateMap<PresentadorDTO, Presentador>();
            CreateMap<ReservacionCreationDTO, Reservacion>();

            CreateMap<Asiento, AsientoCreationDTO>();
            CreateMap<Boleto, BoletoCreationDTO>();
            CreateMap<Cliente, ClienteCreationDTO>();
            CreateMap<Costo, CostoCreationDTO>();
            CreateMap<Establecimiento, EstablecimientoCreationDTO>();
            CreateMap<Establecimiento, EstablecimientoDTO>()
                .ForMember(establecimientoDb => establecimientoDb.asientoCreationDTOs, opciones => opciones.MapFrom(MapEstableAsientos));
            CreateMap<Evento, EventosCreationDTO>();            
            CreateMap<Presentador, PresentadorCreationDTO>();
            CreateMap<Reservacion, ReservacionCreationDTO>();
        }

        private List<AsientoCreationDTO>MapEstableAsientos(Establecimiento establecimiento, EstablecimientoCreationDTO establecimientoCreationDTO)
        {
            var resultado = new List<AsientoCreationDTO>();
            if(establecimiento.Asientos == null) { return resultado; }
            foreach(var estasbleAsiento in establecimiento.Asientos)
            {
                resultado.Add(new AsientoCreationDTO()
                {
                    Id = estasbleAsiento.Id,
                    Numero = estasbleAsiento.Numero,
                    Descripcion = estasbleAsiento.Descripcion
                });
            }
            return resultado;
        }
        private List<PresentadorDTO>MapEventoDTOPresentador(Evento evento, EventoDTO eventoDTO )
        {
            var resultado = new List<PresentadorDTO>();

            if(evento.EventoPresentadors == null) {return resultado;}

            foreach(var eventoPresentador in evento.EventoPresentadors)
            {
                resultado.Add(new PresentadorDTO()
                {
                    Id = eventoPresentador.PresentadorId,
                    Nombre = eventoPresentador.Presentador.Nombre,
                    Fotografia = eventoPresentador.Presentador.Fotografia,
                    RFC = eventoPresentador.Presentador.RFC,
                    Descripcion = eventoPresentador.Presentador.Descripcion
                });
            }
            return resultado;
        }

        private List<EventoPresentador> MapEventoPresentadores(EventosCreationDTO eventosCreationDTO, Evento evento)
        {
            var resultado = new List<EventoPresentador>();

            if(eventosCreationDTO.PresentadoresIds == null){ return resultado; }

            foreach(var presentadorId in eventosCreationDTO.PresentadoresIds)
            {
                resultado.Add(new EventoPresentador() {PresentadorId = presentadorId});
            }
            return resultado;
        }

    }
}
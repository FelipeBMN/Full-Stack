using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetWebApi.Application.Contratos;
using dotnetWebApi.Application.Dtos;
using dotnetWebApi.Domain;
using dotnetWebApi.Persistence.Contratos;

namespace dotnetWebApi.Application
{
    public class EventService : IEventService
    {
        private readonly IGeralPersist geralPersist;
        private readonly IEventPersist eventPersist;
        private readonly IMapper mapper;

        public EventService(IGeralPersist geralPersist,
                            IEventPersist eventPersist,
                            IMapper mapper)
        {
            this.eventPersist = eventPersist;
            this.mapper = mapper;
            this.geralPersist = geralPersist;
        }

        public async Task<EventoDto> AddEvento(EventoDto model)
        {
            try
            {
                var evento = this.mapper.Map<Event>(model);

                this.geralPersist.Add<Event>(evento);

                if (await this.geralPersist.SaveChangesAsync())
                {
                    // Caso queira indicar qual o evento adicionado
                    var retorno = await this.eventPersist.GetEventByIdAsync(evento.Id, false);

                    return this.mapper.Map<EventoDto>(retorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                
                var evento = await this.eventPersist.GetEventByIdAsync(eventoId, false);
                if (evento == null) return null;
                
                var eventoEvent = this.mapper.Map<Event>(model);

                eventoEvent.Id = evento.Id;

                this.geralPersist.Update<Event>(eventoEvent);

                if (await this.geralPersist.SaveChangesAsync())
                {
                    // Caso queira indicar qual o evento adicionado
                    var eventoRetorno = await this.eventPersist.GetEventByIdAsync(eventoEvent.Id, false);

                    return this.mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await this.eventPersist.GetEventByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para delete nâo encontrado.");

                this.geralPersist.Delete<Event>(evento);
                return await this.geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await this.eventPersist.GetAllEventAsync(includePalestrantes);
                if (eventos == null) return null;

                var resultados = this.mapper.Map<EventoDto[]>(eventos);

                return resultados;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventByIdAsync(int eventId, bool includePalestrantes)
        {
            try
            {
                var evento = await this.eventPersist.GetEventByIdAsync(eventId, includePalestrantes);
                if (evento == null) return null;

                var resultado = this.mapper.Map<EventoDto>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetEventByTemaAsync(string tema, bool includePalestrantes)
        {
            try
            {
                var eventos = await this.eventPersist.GetEventByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                
                var resultados = this.mapper.Map<EventoDto[]>(eventos);

                return resultados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
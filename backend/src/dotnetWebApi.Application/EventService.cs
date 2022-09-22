using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Application.Contratos;
using dotnetWebApi.Domain;
using dotnetWebApi.Persistence.Contratos;

namespace dotnetWebApi.Application
{
    public class EventService : IEventService
    {
        private readonly IGeralPersist geralPersist;
        private readonly IEventPersist eventPersist;
        public EventService(IGeralPersist geralPersist, IEventPersist eventPersist)
        {
            this.eventPersist = eventPersist;
            this.geralPersist = geralPersist;
        }

        public async Task<Event> AddEvento(Event model)
        {
            try
            {
                this.geralPersist.Add<Event>(model);
                if (await this.geralPersist.SaveChangesAsync())
                {
                    // Caso queira indicar qual o evento adicionado
                    return await this.eventPersist.GetEventByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event> UpdateEvento(int eventoId, Event model)
        {
            try
            {
                var evento = await this.eventPersist.GetEventByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                this.geralPersist.Update(model);
                if (await this.geralPersist.SaveChangesAsync())
                {
                    // Caso queira indicar qual o evento adicionado
                    return await this.eventPersist.GetEventByIdAsync(model.Id, false);
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

        public async Task<Event[]> GetAllEventAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await this.eventPersist.GetAllEventAsync(includePalestrantes);
                if (eventos == null) return null;
                
                return eventos;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includePalestrantes)
        {
            try
            {
                var eventos = await this.eventPersist.GetEventByIdAsync(eventId,includePalestrantes);
                if (eventos == null) return null;
                
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event[]> GetEventByTemaAsync(string tema, bool includePalestrantes)
        {
            try
            {
                var eventos = await this.eventPersist.GetEventByTemaAsync(tema,includePalestrantes);
                if (eventos == null) return null;
                
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
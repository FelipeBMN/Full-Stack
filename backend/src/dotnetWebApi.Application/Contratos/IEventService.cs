using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Domain;

namespace dotnetWebApi.Application.Contratos
{
    public interface IEventService
    {
        Task<Event> AddEvento(Event model);
        Task<Event> UpdateEvento(int eventoId,Event model);
        Task<bool> DeleteEvento(int eventoId);

        //Event
        Task<Event[]> GetAllEventAsync(bool includePalestrantes);
        Task<Event[]> GetEventByTemaAsync(string tema, bool includePalestrantes);
        Task<Event> GetEventByIdAsync(int eventId, bool includePalestrantes);
    }
}
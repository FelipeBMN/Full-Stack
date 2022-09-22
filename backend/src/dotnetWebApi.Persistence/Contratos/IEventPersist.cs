using System.Threading.Tasks;
using dotnetWebApi.Domain;

namespace dotnetWebApi.Persistence.Contratos
{
    public interface IEventPersist
    {
        //Event
        Task<Event[]> GetAllEventAsync(bool includePalestrantes);
        Task<Event[]> GetEventByTemaAsync(string tema, bool includePalestrantes);
        Task<Event> GetEventByIdAsync(int eventId, bool includePalestrantes);
    }
}
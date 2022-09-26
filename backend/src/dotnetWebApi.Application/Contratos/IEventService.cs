using System.Threading.Tasks;
using dotnetWebApi.Application.Dtos;

namespace dotnetWebApi.Application.Contratos
{
    public interface IEventService
    {
        Task<EventoDto> AddEvento(EventoDto model);
        Task<EventoDto> UpdateEvento(int eventoId,EventoDto model);
        Task<bool> DeleteEvento(int eventoId);

        //Event
        Task<EventoDto[]> GetAllEventAsync(bool includePalestrantes);
        Task<EventoDto[]> GetEventByTemaAsync(string tema, bool includePalestrantes);
        Task<EventoDto> GetEventByIdAsync(int eventId, bool includePalestrantes);
    }
}
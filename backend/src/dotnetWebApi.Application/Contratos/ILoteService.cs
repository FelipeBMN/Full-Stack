using System.Threading.Tasks;
using dotnetWebApi.Application.Dtos;

namespace dotnetWebApi.Application.Contratos
{
    public interface ILoteService
    {
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models);
        Task<bool> DeleteLote(int eventoId, int loteId);

        Task<LoteDto[]> GetAllLotesByEventIdAsync(int EventoId);
        Task<LoteDto> GetLoteByIdAsync(int envetoId, int loteId);
    }
}
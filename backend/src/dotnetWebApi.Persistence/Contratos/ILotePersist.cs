using System.Threading.Tasks;
using dotnetWebApi.Domain;

namespace dotnetWebApi.Persistence.Contratos
{
    public interface ILotePersist
    {
        /// <summary>
        ///     Retorna todos os lotes de um evento
        /// </summary>
        /// <param name="eventoId"></param>
        /// <returns>Lista de lotes</returns>
        Task<Lote[]> GetAllLotesByEventIdAsync(int eventoId);

        /// <summary>
        ///     Método que retorna apenas um lote
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="id"> Código Id do lote</param>
        /// <returns>Lote</returns>
        Task<Lote> GetLoteByIdAsync(int eventoId, int id);
    }
}
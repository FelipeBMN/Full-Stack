using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Domain;
using dotnetWebApi.Persistence.Contextos;
using dotnetWebApi.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApi.Persistence
{
    public class LotePersist : ILotePersist
    {
        private readonly dotnetWebAPIContext context;
        public LotePersist(dotnetWebAPIContext context)
        {
            this.context = context;
        }

        public async Task<Lote[]> GetAllLotesByEventIdAsync(int eventoId)
        {
            IQueryable<Lote> query = this.context.Lotes;
            query = query.AsNoTracking().Where(lote => lote.EventId == eventoId);

            return await query.ToArrayAsync();
        }

        public async Task<Lote> GetLoteByIdAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = this.context.Lotes;
            query = query.AsNoTracking().Where(lote => (lote.EventId == eventoId && lote.Id == id));

            return await query.FirstOrDefaultAsync();
        }
    }
}
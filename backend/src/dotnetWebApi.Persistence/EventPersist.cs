using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Domain;
using dotnetWebApi.Persistence.Contextos;
using dotnetWebApi.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApi.Persistence
{
    public class EventPersist : IEventPersist
    {
        private readonly dotnetWebAPIContext context;
        public EventPersist(dotnetWebAPIContext context)
        {
            this.context = context;
        }
 
        //Event
        public async Task<Event[]> GetAllEventAsync(bool includePalestrantes = false)
        {
            IQueryable<Event> query = this.context.Events.Include(e => e.Lotes).Include(e => e.RedesSociais);

            if (includePalestrantes){
                query = query.Include(e => e.PalestrantesEvents).ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetEventByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Event> query = this.context.Events.Include(e => e.Lotes).Include(e => e.RedesSociais);

            if (includePalestrantes){
                query = query.Include(e => e.PalestrantesEvents).ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
            .Where(e => e.Tema.ToLower()
            .Contains(tema.ToLower()));

            return await query.AsNoTracking().ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includePalestrantes = false)
        {
             IQueryable<Event> query = this.context.Events.Include(e => e.Lotes).Include(e => e.RedesSociais);

            if (includePalestrantes){
                query = query.Include(e => e.PalestrantesEvents).ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
            .Where(e => e.Id == eventId);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
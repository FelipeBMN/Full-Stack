using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Domain;
using dotnetWebApi.Persistence.Contextos;
using dotnetWebApi.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApi.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly dotnetWebAPIContext context;
        public PalestrantePersist(dotnetWebAPIContext context)
        {
            this.context = context;
        }

        //Palestrantes
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes.Include(p => p.RedesSociais);

            if (includeEventos){
                query = query.Include(p => p.PalestrantesEvents).ThenInclude(pe => pe.Event);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes.Include(p => p.RedesSociais);

            if (includeEventos){
                query = query.Include(p => p.PalestrantesEvents).ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
            .Where(p => p.User.FirstName.ToLower()
            .Contains(nome.ToLower()) || p.User.LastName.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes.Include(p => p.RedesSociais);

            if (includeEventos){
                query = query.Include(p => p.PalestrantesEvents).ThenInclude(pe => pe.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
            .Where(p => p.Id == palestranteId );

            return await query.FirstOrDefaultAsync();
        }

        

        
    }
}
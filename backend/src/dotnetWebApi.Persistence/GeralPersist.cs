using System.Threading.Tasks;
using dotnetWebApi.Persistence.Contextos;
using dotnetWebApi.Persistence.Contratos;

namespace dotnetWebApi.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly dotnetWebAPIContext context;
        public GeralPersist(dotnetWebAPIContext context)
        {
            this.context = context;
        }
        // Geral
        public void Add<T>(T entity) where T : class
        {
            this.context.Add(entity);
        }

         public void Update<T>(T entity) where T : class
        {
            this.context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            this.context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return( await this.context.SaveChangesAsync()) > 0;
        }  
    }
}
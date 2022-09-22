using dotnetWebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApi.Persistence.Contextos
{
    public class dotnetWebAPIContext : DbContext
    {
        public dotnetWebAPIContext(DbContextOptions<dotnetWebAPIContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvent> PalestranteEvents { get; set; }
        public DbSet<RedeSocial> RedeSocials { get; set; }

        protected  override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<PalestranteEvent>().HasKey(PE => new {PE.EventId, PE.PalestranteId});

            modelBuilder.Entity<Event>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Event)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
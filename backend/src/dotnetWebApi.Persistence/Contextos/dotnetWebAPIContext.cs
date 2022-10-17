using dotnetWebApi.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotnetWebApi.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace dotnetWebApi.Persistence.Contextos
{
    // USE INT FOR DEV BUT IN PRODUCTION USE Guid
    public class dotnetWebAPIContext : IdentityDbContext<User, Role, int,
                                                        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public dotnetWebAPIContext(DbContextOptions<dotnetWebAPIContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvent> PalestranteEvents { get; set; }
        public DbSet<RedeSocial> RedeSocials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento muito pra muitos

            // using modelBuilder
            // First mode
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles).HasForeignKey(ur => ur.UserId).IsRequired();
            });

            // Second mode
            modelBuilder.Entity<PalestranteEvent>().HasKey(PE => new { PE.EventId, PE.PalestranteId });
            modelBuilder.Entity<Event>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Event)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e .RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
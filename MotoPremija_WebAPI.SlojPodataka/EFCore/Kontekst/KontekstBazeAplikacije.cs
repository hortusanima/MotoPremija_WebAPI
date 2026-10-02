using Microsoft.EntityFrameworkCore;
using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPodataka.EFCore.Kontekst
{
    public class KontekstBazeAplikacije : DbContext
    {
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Motocikl> Motocikl { get; set; }
        public DbSet<Osiguranje> Osiguranje { get; set; }
        public DbSet<TipOsiguranja> TipOsiguranja { get; set; }

        public KontekstBazeAplikacije(DbContextOptions<KontekstBazeAplikacije> opcije)
            : base(opcije)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Motocikl>()
                .HasOne<Korisnik>()
                .WithOne()
                .HasForeignKey<Motocikl>(m => m.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Osiguranje>()
                .HasOne<Motocikl>()
                .WithMany()
                .HasForeignKey(o => o.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Osiguranje>()
                .HasOne<TipOsiguranja>()
                .WithMany()
                .HasForeignKey(o => o.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Korisnik>()
                .Property(k => k.Uloga)
                .HasConversion<string>();

            modelBuilder.Entity<Motocikl>()
                .Property(m => m.PrimarnaUpotreba)
                .HasConversion<string>();

            modelBuilder.Entity<TipOsiguranja>().HasData(
            new TipOsiguranja
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                NazivOsiguranja = "Osiguranje od odgovornosti",
                BaznaPremija = 100.00
            },
            new TipOsiguranja
            {
                Id = Guid.Parse("22222211-2222-2222-2222-222222222222"),
                NazivOsiguranja = "Kasko osiguranje",
                BaznaPremija = 250.00
            },
            new TipOsiguranja
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                NazivOsiguranja = "Osiguranje štete usled sudara",
                BaznaPremija = 150.00
            },
            new TipOsiguranja
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                NazivOsiguranja = "Pokriće neosiguranog vozila",
                BaznaPremija = 200.00
            }
        );
        }
    }
}

using Microsoft.EntityFrameworkCore;


namespace EF_2.Models
{
    public class DB:DbContext
    {       
        public DB(DbContextOptions<DB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        //lager alle fire tabellene/entitetene her.
        public virtual DbSet<Vare> Vare { get; set; }
        public virtual DbSet<Kunde> Kunde { get; set; }
        public virtual DbSet<Ordre> Ordre { get; set; }
        public virtual DbSet<OrdreLinje> OrdreLinjer { get; set; }

        //Og lager denne for å kunne bruke lazy-loading
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // må importere pakken Microsoft.EntityFrameworkCore.Proxies
            // og legge til"viritual" på de attriuttene som ønskes å lastes automatisk (LazyLoading)
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebapplikasjonerOppgave1.DAL;

namespace WebapplikasjonerOppgave1.Models
{
    [ExcludeFromCodeCoverage]
    public class NorwayContext : DbContext
    {

        public NorwayContext(DbContextOptions<NorwayContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Kunde> Kunder { get; set; }
        public virtual DbSet<Turer> Turer { get; set; }
        public virtual DbSet<Stasjon> Stasjoner { get; set; }
        public virtual DbSet<Bestilling> Bestillinger { get; set; }
        public virtual DbSet<Brukere> Brukere { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    } 
}

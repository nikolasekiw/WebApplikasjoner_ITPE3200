using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ITPE3200Mappe3.DAL
{
    public class KundeSpm
    {
        [Key]
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Epost { get; set; }
        public string NyttSporsmal { get; set; }
    }
    public class FAQ
    {
        [Key]
        public int id { get; set; }
        public string sporsmal { get; set; }
        public string svar { get; set; }
        public int tommelOpp { get; set; }
        public int tommelNed { get; set; }
        public string kategori { get; set; }
    }
    public class KundeserviceContext : DbContext
    {
        public KundeserviceContext(DbContextOptions<KundeserviceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<KundeSpm> KundeSpm { get; set; }
        public DbSet<FAQ> FAQ { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
     }
}
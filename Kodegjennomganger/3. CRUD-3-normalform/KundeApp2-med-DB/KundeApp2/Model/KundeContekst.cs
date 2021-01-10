using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KundeApp2.Model
{
    /**
     * Denne strukturen her med kunde og poststed må vi lage fordi vi skal ha dette på 3. normalform. 
     * Og det er disse to klassene (Kunder og Poststeder) som skal lage grunnlaget for databasen.
     * Kunne også ha laget disse klassene utenfor, som egne filer under Model, men det er vanlig å 
     * legge de sammen med databasecontexten. 
    **/
    public class Kunder
    {
        public int Id { get; set; }  
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }
        virtual public Poststeder Poststed { get; set; }
        //virtual = lazy loading = når vi spør etter en kunde så vil vi også få med automatisk poststedet. 
    }
    
    public class Poststeder
    {
        /**
         * Må også ha nøkkel på denne. Den starter ikke på ID, derfor blir det ikke generert primærnøkkel,
         * men det blir implementert en AUTO_INCREMENT på en nøkkel automatisk når vi setter på [Key] --> en dekoratør.
         * Så skal vi sette på en dekoratør til for å sikre at vi ikke får AUTO_INCREMENT på den, og den
         * heter DatabaseGenerated 
         *
        **/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Postnr { get; set; }
        public String Poststed { get; set; }
        virtual public List<Kunder> Kunder { get; set; }
        // denne listen ikke nødvendig med mindre man skal finne kundene på et gitt postnr (altså gå inn via Poststeder-collection)
    }

    /**
     * Vi må lage override på KundeContext for å introdusere lazy-loading/bruk av virtual.
     * Og det gjør vi på slutten av contexten.
    **/
    public class KundeContext : DbContext
    {
            public KundeContext (DbContextOptions<KundeContext> options)
                    : base(options)
            {
                // denne brukes for å opprette databasen fysisk dersom den ikke er opprettet
                // dette er uavhenig av initiering av databasen (seeding)
                // når man endrer på strukturen på KundeContxt her er det fornuftlig å slette denne fysisk før nye kjøringer
                Database.EnsureCreated();
        }

        public DbSet<Kunder> Kunder { get; set; }
        public DbSet<Poststeder> Poststeder { get; set; }

        /**
         * Her overrider vi contexten for å introdusere LazyLoading
        **/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // må importere pakken Microsoft.EntityFrameworkCore.Proxies
            // og legge til"viritual" på de attriuttene som ønskes å lastes automatisk (LazyLoading)
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}

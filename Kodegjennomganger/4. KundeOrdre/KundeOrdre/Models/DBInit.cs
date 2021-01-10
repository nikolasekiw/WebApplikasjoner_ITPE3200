using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_2.Models
{
    public class DBInit
    {
        public static void init(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<DB>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var nyKunde = new Kunde
                {
                    Navn = "Ole Hansen"
                };

                var nyOrdre = new Ordre
                {
                    Dato = "23.05.2017"
                };

                var nyVare1 = new Vare
                {
                    Pris = 2.34,
                    Navn = "Mutter 3mm"
                };
                var nyVare2 = new Vare
                {
                    Pris = 3.34,
                    Navn = "Mutter 4mm"
                };

                var nyOrdreLinje1 = new OrdreLinje
                {
                    Vare = nyVare1, //varen er lagt inn i OrdreLinje
                    Antall = 100
                };

                var nyOrdreLinje2 = new OrdreLinje
                {
                    Vare = nyVare2,
                    Antall = 50
                };

                //Så må jeg legge ordrelinjene inn i en liste av ordrelinjer som skal inn i ordren.

                // legg ordrelinjene inn i den nye ordren
                // det eksisterer ingen Liste av ordrelinjer i ordren fra før av så den må opprettes!
                var nyeOrdreLinjer = new List<OrdreLinje>();
                nyeOrdreLinjer.Add(nyOrdreLinje1); //legger de to ordrelinjene inn til den listen av ordrelinjer
                nyeOrdreLinjer.Add(nyOrdreLinje2);

                nyOrdre.OrdreLinjer = nyeOrdreLinjer; //legger disse ordrelinjene inn i nyOrdre sin ordrelinje

                // det eksisterer ingen Liste av ordre i kunden så den må opprettes først!
                var nyeOrdre = new List<Ordre>();
                nyeOrdre.Add(nyOrdre);
                nyKunde.Ordre = nyeOrdre;

                // legg hele kunden med alle dataene inn i databasen
                context.Kunde.Add(nyKunde); //vi må da bygge denne opp bakenifra på en måte, kommer til kunde til slutt.
                context.SaveChanges();
            }
        }
    }
}
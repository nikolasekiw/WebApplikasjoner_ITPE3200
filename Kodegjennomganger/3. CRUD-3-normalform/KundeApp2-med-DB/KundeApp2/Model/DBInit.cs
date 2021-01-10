using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KundeApp2.Model
{
    //Dette er slik vi "seeder"/så/initiere databasen når vi starter serveren med data. 
    public static class DBInit
    {
        /**
         * Denne metoden kan hete hva som helst. Den tar inn IApplicationBuilder og app
         * som variabel. 
        **/
        public static void Initialize(IApplicationBuilder app)
        {
            //Her må det importeres noen using namespaces. 
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                /**
                 * Vi lager en context, altså en knytning til databasen. 
                 * GetService..<KundeContext> osv. vil si den KundeXontext-en/klassen vår
                 * hvor kundeContext-en ligger, den som er implementert (DbContext), den skal
                 * da ta inn KundeContext som sin type (parameter?).
                 * 
                 * Using som er brukt over, betyr at det scopet eller denne contexten skal 
                 * bare vare inne i scopet. Det er en måte å åpne og lukke databasen på i 
                 * denne initialize metoden.
                 * 
                 * Variabelen context (under) kan hete DB eller hva som helst, men den benyttes
                 * for å lagre ting. 
                **/
                var context = serviceScope.ServiceProvider.GetService<KundeContext>();

                /**
                 * Må slette og opprette databasen hver gang når den skal initieres (seed`es).
                 * Hvis vi ikke gjør det så vil vi bare legge til nye hver gang vi 
                 * starter applikasjonen, så vil vi få flere og flere rader. 
                 * **/
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                /**
                 * Ettersom vi har kunder og poststeder, så skal vi ha poststeder
                 * inn i kundene. Så da lager vi to nye poststeder. 
                 * Kan gjøre det på nye linjer og da ha () bak new Poststeder og ha resten
                 * under, men det er smak og behag. Men når det gjelder seeding av
                 * nye data så er det vanlig å ha det ved siden av for å gjøre det mer kompakt. 
                 * 
                **/
                var poststed1 = new Poststeder {Postnr = "0270", Poststed = "Oslo"};
                var poststed2 = new Poststeder {Postnr = "1370", Poststed = "Asker"};

                var kunde1 = new Kunder { Fornavn = "Ole", Etternavn = "Hansen", Adresse = "Olsloveien 82", Poststed = poststed1};
                var kunde2 = new Kunder { Fornavn = "Line", Etternavn = "Jensen", Adresse = "Askerveien 72", Poststed = poststed2 };

                context.Kunder.Add(kunde1);
                context.Kunder.Add(kunde2);

                /**
                 * Må egentlig ha asynkrone kall til databasen, men her er det bare
                 * en savechanges som skal gjøres og det skal bare gjøres ved
                 * oppstart av server en gang, og derfor er det ikke nødvendig å bruke
                 * Tasks, async og await her. 
                **/
                context.SaveChanges();

                /**
                 * En annen ting som må også gjøres her er å få startet denne metoden, og
                 * det gjøres i startup.cs
                **/
            }
        }
    }
       
}

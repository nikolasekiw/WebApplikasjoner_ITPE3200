using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ITPE3200Mappe3.DAL
{
    public static class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
            var db = serviceScope.ServiceProvider.GetService<KundeserviceContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var kundeSpm = new KundeSpm
            {
                Fornavn = "Nikola",
                Etternavn = "Sekiw",
                Epost = "nikola@gmail.com",
                NyttSporsmal = "Hei, dette er et spørsmål"
            };

            var faq1 = new FAQ
            {
                sporsmal = "Hvordan endrer jeg på reisen min?",
                svar = "Du må ta kontakt med admin slik at admin da kan få endret på reisen. " +
                       "Da kan man få endret på både antall billetter, men også start- og endestasjonen",
                tommelOpp = 7,
                tommelNed = 1,
                kategori = "Billetter"
            };

            var faq2 = new FAQ
            {
                sporsmal = "Hvor finner jeg mine tidligere billetter",
                svar = "Ved å ta kontakt med kundeservice kan du få tilsendt en pdf-fil med alle tidligere reiser.",
                tommelOpp = 1,
                tommelNed = 1,
                kategori = "Billetter"
            };
            
            var faq3 = new FAQ
            {
                sporsmal = "Hvor ser jeg prisen på billetter til diverse reiser",
                svar = "Billettprisen kommer opp etter at en reise er valgt og antall voksne og barn har blitt satt." +
                       "Da ser man prisen både for barn og for voksen. Prisene varierer ut ifra hvor man skal reise" +
                       "fra og til",
                tommelOpp = 8,
                tommelNed = 1,
                kategori = "Billetter"
            };
            
            var faq4 = new FAQ
            {
                sporsmal = "Hvordan kan jeg avbestille billetten min?",
                svar = "Du kan ta kontakt med kundeservis og få kansellert reisen din. Da vil pengene komme tilbake" +
                       "på det registrere bankkortet i løpet av noen få dager. Reisen kan derimot ikke avbestilles" +
                       "dersom det er under en time til avreise. ",
                tommelOpp = 2,
                tommelNed = 0,
                kategori = "Billetter"
            };

            var faq5 = new FAQ
            {
                sporsmal = "Er det tillatt å ha med dyr?",
                svar = "Det er dessverre ikke tillatt på våre busser. Det er kun tillatt ved spesiell unntak, " +
                       "f.eks. hvis det er fører- tjeneste- eller servicehund.",
                tommelOpp = 4,
                tommelNed = 2,
                kategori = "Regler"
            };
            
            var faq6 = new FAQ
            {
                sporsmal = "Hvor mye bagasje kan jeg ta med meg?",
                svar = "Det er ikke en konkret regel på nøyaktig mengde, men en max. er 2 kofforter og en sekk/veske." +
                       "Dersom man har med seg mer enn det er det ikke garantert at det er plass til det. Om det skulle " +
                       "vært nødvendig kan man ta kontakt på telefon og bestille ekstra plass.",
                tommelOpp = 6,
                tommelNed = 0,
                kategori = "Regler"
            };
            
            var faq7 = new FAQ
            {
                sporsmal = "Er det greit å spis mat under turen?",
                svar = "Det er tillatt med mat under turen, men det er viktig at det ikke forstyrrer andre reisene. " +
                       "Dermed så anbefales det å ikke spise mat som lukter mye, nøtter eller annen mat som kan " +
                       "forstyrre andre.",
                tommelOpp = 2,
                tommelNed = 0,
                kategori = "Regler"
            };
            
            var faq8 = new FAQ
            {
                sporsmal = "Er det greit å spis mat under turen?",
                svar = "Det er tillatt med mat under turen, men det er viktig at det ikke forstyrrer andre reisene. " +
                       "Dermed så anbefales det å ikke spise mat som lukter mye, nøtter eller annen mat som kan " +
                       "forstyrre andre.",
                tommelOpp = 2,
                tommelNed = 0,
                kategori = "Regler"
            };
            
            var faq9 = new FAQ
            {
                sporsmal = "Er det greit å spis mat under turen?",
                svar = "Det er tillatt med mat under turen, men det er viktig at det ikke forstyrrer andre reisene. " +
                       "Dermed så anbefales det å ikke spise mat som lukter mye, nøtter eller annen mat som kan " +
                       "forstyrre andre.",
                tommelOpp = 2,
                tommelNed = 0,
                kategori = "Regler"
            };
            
            var faq13 = new FAQ
            {
                sporsmal = "Kan barn reise alene?",
                svar = "Det går helt fint at barn reiser alene. Da er det viktig å forberede" +
                       "barnet på hva som venter dem. Vi anbefaler at barnet er i skolepliktig alder og " +
                       "kan ta vare på seg selv under bussreisen. Barnet må følges til bussen av foresatt " +
                       "og må kunne fremvise informasjon med navn og nummer til en ansvarlig voksen som " +
                       "sjåføren kan ringe til om det skulle skje noe uforutsett.",
                tommelOpp = 2,
                tommelNed = 1,
                kategori = "Regler"
            };

            var faq10 = new FAQ
            {
                sporsmal = "Er det noe studentrabatt for reise over lang strekning",
                svar = "For øyeblikket tilbyr vi dessverre ikke noen studentrabatter for våre reiser dessverre." +
                       "Utelukker derimot ikke at dette kan bli mulig på et senere tidspunkt.",
                tommelOpp = 7,
                tommelNed = 2,
                kategori = "TurTilbud"
            };

            var faq11 = new FAQ
            {
                sporsmal = "Er det egne plasser til rullestol?",
                svar = "Det er plasser til rollestol ved alle dørene. Av hensyn til sikkerheten og plassen om " +
                       "bord i bussen kan ikke rullestolen overstige målene; lengde 120 cm, bredde 70 cm, " +
                       "høyde 109 cm. Det er også begrensninger på totalvekt av stol. Dette kan variere noe " +
                       "fra rute til rute, avhengig av sikring i buss.",
                tommelOpp = 2,
                tommelNed = 2,
                kategori = "TurTilbud"
            };
            
            var faq12 = new FAQ
            {
                sporsmal = "Er det internett og strømuttak på bussen?",
                svar = "De fleste bussene tilbyr gratis Wi-Fi og strømuttak under setene. Husk derimot å ta på" +
                       "headset dersom du skal se på noe, slik at det ikke forstyrrer alle reisende.",
                tommelOpp = 1,
                tommelNed = 0,
                kategori = "TurTilbud"
            };

            var faq14 = new FAQ
            {
                sporsmal = "Kan jeg ha med sykkel?",
                svar = "Det er greit å ha med sykkel, men dette må registreres under bestillingen av billetten. " +
                       "Det er egne plasser som er merkert som er tilpasset for plass til sykkel.",
                tommelOpp = 2,
                tommelNed = 0,
                kategori = "TurTilbud"
            };

            var faq15 = new FAQ
            {
                sporsmal = "Har dere en app?",
                svar = "Vi har for øyeblikket ikke et slik tilbud, men utelukker ikke at det er noe som kan komme " +
                       "i fremtiden. For øyeblikket er det kun mulig å bestille reise inne på våre nettsider.",
                tommelOpp = 2,
                tommelNed = 0,
                kategori = "TurTilbud"
            };

            db.FAQ.Add(faq1);
            db.FAQ.Add(faq2);
            db.FAQ.Add(faq3);
            db.FAQ.Add(faq4);
            db.FAQ.Add(faq5);
            db.FAQ.Add(faq6);
            db.FAQ.Add(faq7);
            db.FAQ.Add(faq8);
            db.FAQ.Add(faq9);
            db.FAQ.Add(faq10);
            db.FAQ.Add(faq11);
            db.FAQ.Add(faq12);
            db.FAQ.Add(faq13);
            db.FAQ.Add(faq14);
            db.FAQ.Add(faq15);
            db.KundeSpm.Add(kundeSpm);

            db.SaveChanges();
        }
    }
}
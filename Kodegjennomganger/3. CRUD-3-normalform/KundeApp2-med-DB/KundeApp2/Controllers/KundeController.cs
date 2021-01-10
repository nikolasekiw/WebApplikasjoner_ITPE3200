using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KundeApp2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KundeApp2.Controllers
{
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        private readonly KundeContext _db;

        public KundeController(KundeContext db)
        {
            _db = db;
        }

        /**
         * Lagre metoden blir mer komplisert når på 3. normalform fordi vi må forholde
         * oss til den kunden som kommer inn her (som er den flate kunden), den kunden som
         * ikke ligger sammen med context klassen, men den som ligger for seg selv.
         * Vi må omforme de attributtene som kommer inn i innKunde til den strukturen i 
         * tabellene i context klassen hvor de er hver for seg. 
        **/
        public async Task<bool> Lagre(Kunde innKunde)
        {
            try
            {
                var nyKundeRad = new Kunder(); //lager ny kunderad, må ta new på kunder, altså db-klassen
                nyKundeRad.Fornavn = innKunde.Fornavn; //må overføre attributtene som skal til inn i kunden fra innKunde
                nyKundeRad.Etternavn = innKunde.Etternavn; //tar ikke id når vi skal lagre, men begynner med fornavnet. 
                nyKundeRad.Adresse = innKunde.Adresse; //det er disse tre som skal inn i kunden

                /**
                 * Så må vi sjekke om postnr eksisterer i db fra før av. Hvis det ikke gjør det så opprettes det 
                 * et nytt poststedsobjekt og så legges det inn i kunden. Kunne ha lagt inn alle poststeder og nr fra 
                 * hele landet på forhånd og da ville ikke lagringen vært så komplisert, men dette
                 * gjøres for å illustrere. 
                 * 
                 * Sjekker først om poststedet finnes (bruker FindAsync). Og nøkkelen til denne
                 * er innKunde.Postnr. Den returnerer enten poststedet eller null. 
                **/
                var sjekkPoststed = await _db.Poststeder.FindAsync(innKunde.Postnr);
                if (sjekkPoststed == null) //hvis poststedet ikke fantes
                {
                    var nyPoststedsRad = new Poststeder(); //da oppretter vi en ny poststedsrad
                    nyPoststedsRad.Postnr = innKunde.Postnr; //så legger vi inn de to parameterne fra innKunde
                    nyPoststedsRad.Poststed = innKunde.Poststed;
                    nyKundeRad.Poststed = nyPoststedsRad; //legger inn poststedet (poststedsRad) inn i ny nyKundeRad. Da er poststedet lagt inn i kunden. 
                }
                else
                {
                    nyKundeRad.Poststed = sjekkPoststed; 
                }
                _db.Kunder.Add(nyKundeRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<List<Kunde>> HentAlle()  
        {
            try
            {
                List<Kunde> alleKunder = await _db.Kunder.Select(k => new Kunde
                {
                    /**
                     * Så mapper vi de kundene vi får fra Kunder som kommer inn i k i lambda, 
                     * inn i new Kunde og dette skjer automatisk for alle kundene.
                     * Skriver Id som er for denne kunden erLik k.id, så fortsetter med de andre også.
                    **/
                    Id = k.Id,
                    Fornavn = k.Fornavn,
                    Etternavn = k.Etternavn,
                    Adresse = k.Adresse,
                    Postnr = k.Poststed.Postnr, //legg merke til de to siste radene
                    Poststed = k.Poststed.Poststed
                }).ToListAsync();
                return alleKunder;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                //Her er det selve kunden som må endres. Den som går mot databasen og ikke modell-kunden.
                Kunder enKunde = await _db.Kunder.FindAsync(id);
                _db.Kunder.Remove(enKunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Kunde> HentEn(int id)
        {
            Kunder enKunde = await _db.Kunder.FindAsync(id); //henter kunde fra DB med den spesielle id-en
            var hentetKunde = new Kunde() //vår modell som skal overføres
            {
                /**
                 * Her setter vi id, fornavn osv. inn i hentetKunde (modell) og dataene
                 * får vi dra enKunde som er kunden fra databasen. Her mapper vi.
                **/
                Id = enKunde.Id,
                Fornavn = enKunde.Fornavn,
                Etternavn = enKunde.Etternavn,
                Adresse = enKunde.Adresse,
                Postnr = enKunde.Poststed.Postnr,
                Poststed = enKunde.Poststed.Poststed
            };
            return hentetKunde; //returnerer modellen
        }

        public async Task<bool> Endre(Kunde endreKunde)
        {
            try
            {
                /**
                 * Så må vi finne ut om postnr er endret, altså om innkunden er forskjellig
                 * fra kunden man har funnet. Hvis den er det så må man gjøre noe med 
                 * poststeds-tabellen
                **/
                Kunder enKunde = await _db.Kunder.FindAsync(endreKunde.Id);
                if (enKunde.Poststed.Postnr != endreKunde.Postnr)
                {
                    var sjekkPoststed = _db.Poststeder.Find(endreKunde.Postnr);
                    if (sjekkPoststed == null)
                    {
                        var nyPoststedsRad = new Poststeder();
                        nyPoststedsRad.Postnr = endreKunde.Postnr;
                        nyPoststedsRad.Poststed = endreKunde.Poststed;
                        enKunde.Poststed = nyPoststedsRad;
                    }
                    else
                    {
                        enKunde.Poststed = sjekkPoststed;
                    }
                }
                /**
                 * Hvis poststedet ikke er flyttet så skal vi flytte de andre
                 * attributtene over. 
                 * 
                 * Under oppdaterer vi enKunde i databasen til å få verdiene inn fra 
                 * endreKunde, altså modellen/det vi får inn fra klienten.
                 * Det var det som skulle til for å kjøre denne løsningen på server.
                **/
                enKunde.Fornavn = endreKunde.Fornavn; 
                enKunde.Etternavn = endreKunde.Etternavn;
                enKunde.Adresse = endreKunde.Adresse;
                await _db.SaveChangesAsync();
            }
            catch 
            {
                return false;
            }
            return true;
        }
    }
}

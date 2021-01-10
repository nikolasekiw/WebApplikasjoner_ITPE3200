using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using KundeApp2.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KundeApp2.DAL
{
    /**
     * Løsningen her inneholder en logg- inn funksjon for å kunne vise kundene. 
     * Løsningen lagrerpassordet kryptert (eller hash'et). Det blir brukt en innebygd hashingsfunksjon kalt Pbkdf2. 
     * Denne kan settes opp for å hash'e et antall ganger (f.eks 1000) og velge hvilken algoritme som skal 
     * brukes (her SHA512 - den mest robuste algoritmen). Man kunne vurdert å bruke f.eks BlowFish algoritme isteden, 
     * men da må det benyttes NuGet-pakker fra andre enn Microsoft. Dette kan innebære en risiko.
     * 
     * Oppdatering i Properties mappen i filen launchSetting.json er nødvendig for å starte løsningen fra logginn.html.
     * 
     * Løsningen er ikke komplett. Det betyr at sikkerhet for å forbigå innlogging ikke er implementert. 
     * Dette skjer i neste omgang ved innføring av sessions.
    **/
    public class KundeRepository : IKundeRepository
    {
        private KundeContext _db;

        private ILogger<KundeRepository> _log;

        public KundeRepository(KundeContext db, ILogger<KundeRepository> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<bool> Lagre(Kunde innKunde)
        {
            try
            {
                var nyKundeRad = new Kunder();
                nyKundeRad.Fornavn = innKunde.Fornavn;
                nyKundeRad.Etternavn = innKunde.Etternavn;
                nyKundeRad.Adresse = innKunde.Adresse;

                var sjekkPostnr = await _db.Poststeder.FindAsync(innKunde.Postnr);
                if (sjekkPostnr == null)
                {
                    var poststedsRad = new Poststeder();
                    poststedsRad.Postnr = innKunde.Postnr;
                    poststedsRad.Poststed = innKunde.Poststed;
                    nyKundeRad.Poststed = poststedsRad;
                }
                else
                {
                    nyKundeRad.Poststed = sjekkPostnr;
                }
                _db.Kunder.Add(nyKundeRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<List<Kunde>> HentAlle()
        {
            try
            {
                List<Kunde> alleKunder = await _db.Kunder.Select(k => new Kunde
                {
                    Id = k.Id,
                    Fornavn = k.Fornavn,
                    Etternavn = k.Etternavn,
                    Adresse = k.Adresse,
                    Postnr = k.Poststed.Postnr,
                    Poststed = k.Poststed.Poststed
                }).ToListAsync();
                return alleKunder;
            }
            catch(Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Kunder enDBKunde = await _db.Kunder.FindAsync(id);
                _db.Kunder.Remove(enDBKunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<Kunde> HentEn(int id)
        {
            try
            {
                Kunder enKunde = await _db.Kunder.FindAsync(id);
                var hentetKunde = new Kunde()
                {
                    Id = enKunde.Id,
                    Fornavn = enKunde.Fornavn,
                    Etternavn = enKunde.Etternavn,
                    Adresse = enKunde.Adresse,
                    Postnr = enKunde.Poststed.Postnr,
                    Poststed = enKunde.Poststed.Poststed
                };
                return hentetKunde;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<bool> Endre(Kunde endreKunde)
        {
            try
            {
                var endreObjekt = await _db.Kunder.FindAsync(endreKunde.Id);
                if (endreObjekt.Poststed.Postnr != endreKunde.Postnr)
                {
                    var sjekkPostnr = _db.Poststeder.Find(endreKunde.Postnr);
                    if (sjekkPostnr == null)
                    {
                        var poststedsRad = new Poststeder();
                        poststedsRad.Postnr = endreKunde.Postnr;
                        poststedsRad.Poststed = endreKunde.Poststed;
                        endreObjekt.Poststed = poststedsRad;
                    }
                    else
                    {
                        endreObjekt.Poststed.Postnr = endreKunde.Postnr;
                    }
                }
                endreObjekt.Fornavn = endreKunde.Fornavn;
                endreObjekt.Etternavn = endreKunde.Etternavn;
                endreObjekt.Adresse = endreKunde.Adresse;
                await _db.SaveChangesAsync();
            }
            catch(Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
            return true;
        }

        /**
         * Denne tar inn string passord
        **/
        public static byte[] LagHash(string passord, byte[] salt)
        {
            /**
             * Bruker hashfunksjonen Pbkdf2. Den tar passordet og saltet og en parameter prf. Den sier noe om hvilken 
             * krypteringsalgoritme skal brukes. Vi har valgt SHA512. Den skal kjøres 1000 ganger. Dvs. at først så genereres
             * det en hash, så hashes den hashen, så hashes den hashen... 1000 ganger. Det er for å gjøre det vanskeligere å finne
             * en hash bare ved å gjette. Jeg har også bestemt at selve hashen skal være 32 byte. 
            **/
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        /**
         * Det må lages og genereres et salt når vi legger inn en bruker. Da tar vi new på cryptoServiceProvider. Et byte array
         * på 24 bite, som er saltet. Så bruker vi den serviceProvideren, så GetBytes(salt), det er et randomisert salt, 24 tilfeldige tegn. 
         * Til slutt returneres saltet. 
         * 
         * Så se på hvordan vi har lagt inn brukeren (i DBInit)
        **/
        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                //Finner brukeren ved å lete etter brukernavnet, altså fra den brukeren som kommer inn og ser i brukernavnet i tabellen/DB
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);

                // sjekk passordet, at passordet som ligger i funnetBruker (hashen) er det samme som når vi hasher det passordet som kommer inn i bruker.
                //Så vi lager en ny hash. Med bruker (den som kommer inn) sitt passord og og funnetBruker.Salt. Det ligger en bruker allerede i DB
                //som heter admin, og det er generert en hash og et salt for denne brukeren allerede. Så vi må bruke det saltet som funnetBruker har,
                //samtidig med at man bruker passordet til det som er oppgitt. Lager den hashe, også tar vi hashen sin SequenceEqual, dvs. at den tar
                //byte for bite og sammenligner hashen og funnetBruker sitt passord. Hashen er hashet av den brukeren som kommer inn. Så vi skal
                //sammenligne den hashede brukeren som kommer inn med funnetBrukre sitt passord.
                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }
    }
}
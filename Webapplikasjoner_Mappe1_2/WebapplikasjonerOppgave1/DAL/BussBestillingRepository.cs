using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebapplikasjonerOppgave1.Models;
using WebapplikasjonerOppgave1.DAL;

using static WebapplikasjonerOppgave1.Models.NorwayContext;
using System.Diagnostics.CodeAnalysis;

namespace WebapplikasjonerOppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public class BussBestillingRepository : IBussBestillingRepository
    {
        private readonly NorwayContext _db;
        private ILogger<BussBestillingRepository> _log;

        public BussBestillingRepository(NorwayContext db, ILogger<BussBestillingRepository> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<List<Stasjon>> HentAlleStasjoner()
        {
            List<Stasjon> alleStasjoner = await _db.Stasjoner.ToListAsync();
            return alleStasjoner;
        }

        public async Task<List<Turer>> HentAlleTurer()
        {
            List<Turer> alleTurer = await _db.Turer.ToListAsync();
            return alleTurer;
        }

        public async Task<List<Stasjon>> HentEndeStasjoner(string startStasjonsNavn)
        {
            List<Turer> alleTurer = await _db.Turer.ToListAsync();
            var endeStasjon = new List<Stasjon>();

            foreach (var turen in alleTurer)
            {
                if (startStasjonsNavn.Equals(turen.StartStasjon.StasjonsNavn))
                {
                    endeStasjon.Add(turen.EndeStasjon);
                }
            }
            return endeStasjon;
        }

        public async Task<bool> Lagre(BussBestilling innBussBestilling)
        {
            int turID = 0;
            List<Turer> alleTurer = await _db.Turer.ToListAsync();
            foreach (var turen in alleTurer)
            {
                if (innBussBestilling.StartStasjon.Equals(turen.StartStasjon.StasjonsNavn) &&
                    innBussBestilling.EndeStasjon.Equals(turen.EndeStasjon.StasjonsNavn) &&
                    innBussBestilling.Tid.Equals(turen.Tid) && innBussBestilling.Dato.Equals(turen.Dato))
                {
                    turID = turen.TurId;
                }
            }
            Turer funnetTur = _db.Turer.Find(turID);

            double totalpris = (innBussBestilling.AntallBarn * funnetTur.BarnePris) + (innBussBestilling.AntallVoksne * funnetTur.VoksenPris);


            int kundeID = 0;
            List<Kunde> alleKunder = await _db.Kunder.ToListAsync();

            foreach (var kunde in alleKunder)
            {
                if (innBussBestilling.Fornavn.Equals(kunde.Fornavn) &&
                    innBussBestilling.Etternavn.Equals(kunde.Etternavn) &&
                    innBussBestilling.Epost.Equals(kunde.Epost) &&
                    innBussBestilling.Kortnummer.Equals(kunde.Kortnummer))
                {
                    kundeID = kunde.KId;
                }
            }
            try
            {
                var nyBestillingRad = new Bestilling();
                nyBestillingRad.AntallBarn = innBussBestilling.AntallBarn;
                nyBestillingRad.AntallVoksne = innBussBestilling.AntallVoksne;
                nyBestillingRad.TotalPris = totalpris;
                nyBestillingRad.Tur = funnetTur;


                Kunde funnetKunde = await _db.Kunder.FindAsync(kundeID);

                if (funnetKunde == null)
                {
                    var kundeRad = new Kunde();
                    kundeRad.Fornavn = innBussBestilling.Fornavn;
                    kundeRad.Etternavn = innBussBestilling.Etternavn;
                    kundeRad.Telefonnummer = innBussBestilling.Telefonnummer;
                    kundeRad.Epost = innBussBestilling.Epost;
                    kundeRad.Kortnummer = innBussBestilling.Kortnummer;
                    _db.Kunder.Add(kundeRad);
                    await _db.SaveChangesAsync();
                    nyBestillingRad.kunde = kundeRad;

                }
                else
                {
                    nyBestillingRad.kunde = funnetKunde;
                }
                _db.Bestillinger.Add(nyBestillingRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                    password: passord,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 1000,
                    numBytesRequested: 32);
        }

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
                Brukere funnetBruker = await _db.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
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
        public async Task<bool> OpprettTur(Tur innTur)
        {
            try
            {
                var nyTurRad = new Turer();
                nyTurRad.Dato = innTur.Dato;
                nyTurRad.Tid = innTur.Tid;
                nyTurRad.BarnePris = innTur.BarnePris;
                nyTurRad.VoksenPris = innTur.VoksenPris;

                bool startStasjonFunnet = false;
                List<Stasjon> alleStasjoner = await _db.Stasjoner.ToListAsync();
                foreach (var stasjon in alleStasjoner)
                {
                    if (innTur.StartStasjon.Equals(stasjon.StasjonsNavn))
                    {
                        nyTurRad.StartStasjon = stasjon;
                        startStasjonFunnet = true;
                    }
                }

                if (!startStasjonFunnet)
                {
                    var startStasjonRad = new Stasjon();
                    startStasjonRad.StasjonsNavn = innTur.StartStasjon;
                    nyTurRad.StartStasjon = startStasjonRad;
                }

                bool endeStasjonFunnet = false;
                foreach (var stasjon in alleStasjoner)
                {
                    if (innTur.EndeStasjon.Equals(stasjon.StasjonsNavn))
                    {
                        nyTurRad.EndeStasjon = stasjon;
                        endeStasjonFunnet = true;
                    }
                }

                if (!endeStasjonFunnet)
                {
                    var endeStasjonRad = new Stasjon();
                    endeStasjonRad.StasjonsNavn = innTur.EndeStasjon;
                    nyTurRad.EndeStasjon = endeStasjonRad;
                }

                _db.Turer.Add(nyTurRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<bool> EndreTur(Tur endreTur)
        {
            try
            {
                var tur = await _db.Turer.FindAsync(endreTur.TurId); 
                tur.Tid = endreTur.Tid; 
                tur.Dato = endreTur.Dato;
                tur.BarnePris = endreTur.BarnePris;
                tur.VoksenPris = endreTur.VoksenPris;

                bool startStasjonFunnet = false;
                List<Stasjon> alleStasjoner = await _db.Stasjoner.ToListAsync(); 
                foreach (var stasjon in alleStasjoner) 
                {
                    if (endreTur.StartStasjon.Equals(stasjon.StasjonsNavn)) 
                    {
                        tur.StartStasjon = stasjon; 
                        startStasjonFunnet = true;

                        if(endreTur.StartStasjon == endreTur.EndeStasjon)
                        {
                            return false;
                        }
                    }
                }

                if (!startStasjonFunnet)
                {
                    var startStasjonRad = new Stasjon(); 
                    startStasjonRad.StasjonsNavn = endreTur.StartStasjon; 
                    tur.StartStasjon = startStasjonRad; 
                }

                bool endeStasjonFunnet = false;
                foreach (var stasjon in alleStasjoner)
                {
                    if (endreTur.EndeStasjon.Equals(stasjon.StasjonsNavn))
                    {
                        tur.EndeStasjon = stasjon;
                        endeStasjonFunnet = true;

                        if(endreTur.EndeStasjon == endreTur.StartStasjon)
                        {
                            return false;
                        }
                    }
                }

                if (!endeStasjonFunnet)
                {
                    var endeStasjonRad = new Stasjon();
                    endeStasjonRad.StasjonsNavn = endreTur.EndeStasjon;
                    tur.EndeStasjon = endeStasjonRad;
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<bool> SlettTur(int TurId)
        {
            try
            {
                Turer enTur = await _db.Turer.FindAsync(TurId);
                _db.Turer.Remove(enTur);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

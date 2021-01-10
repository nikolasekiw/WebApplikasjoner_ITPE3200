using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KundeApp2.DAL;
using KundeApp2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/**
 * Her vises det hvordan vi skal lagdele (data access layer) denne applikasjonen.
 * 
 * Vi ønsker å flytte databasekoden vekk fra kontrolleren slik at den blir mer 
 * oversiktlig. Det vil komme mer kode etterhvert i denne, bla.  feilhåndtering. 
 * 
 * Da oppretter vi en ny klasse og et interface til denne og legger all database-kode
 * inn i denne. Så må denne klassen "injiseres" inn ved hjelp av KundeController-konstruktøren.
 * 
 * Alt som har med database å gjøre flytter vi inn i DAL. Da har vi bare Kunde.cs 
 * i Model igjen, den klasse som kommuniserer mellom klienten og tjeneren. 
**/
namespace KundeApp2.Controllers
{
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        private readonly IKundeRepository _db;

        /**
         * Ettersom vi nå har innført en dependency injection, altså at vi initierer 
         * i KundeRepository (db under), så må den derfor registreres i Startup.cs
        **/
        public KundeController(IKundeRepository db) 
        {
            _db = db;
        }

        /**
         * Så må vi lage metoder for tilsvarende kall fra klienten og da kall til
         * db/repository. 
        **/
        public async Task<bool> Lagre(Kunde innKunde)
        {
            return await _db.Lagre(innKunde);
        }

        public async Task<List<Kunde>> HentAlle()
        {
            return await _db.HentAlle();
        }

        public async Task<bool> Slett(int id)
        {
            return await _db.Slett(id);
        }

        public async Task<Kunde> HentEn(int id)
        {
            return await _db.HentEn(id);
        }

        public async Task<bool> Endre(Kunde endreKunde)
        {
            return await _db.Endre(endreKunde);
        }
    }
}

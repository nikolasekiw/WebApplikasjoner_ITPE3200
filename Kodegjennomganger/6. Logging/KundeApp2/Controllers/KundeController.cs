using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KundeApp2.DAL;
using KundeApp2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KundeApp2.Controllers
{
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        private readonly IKundeRepository _db;

        //Logger skal ta en type som er KundeController. Dette gjør vi for å få tak i logger og kunne bruke det i denne klassen.
        private ILogger<KundeController> _log;

        //Må også ta den inn i controllere, da den med i konstruktøren for å sette den. 
        public KundeController(IKundeRepository db, ILogger<KundeController> log)
        {
            _db = db;
            _log = log;
        }

        public Task<bool> Lagre (Kunde innKunde)
        {
            return _db.Lagre(innKunde);
        }

        public Task<List<Kunde>> HentAlle()
        {
            //her tester vi bare at den fungerer. Når vi kjører filen da så ser vi at det automatisk blir opprettet en mappe "Logs" med loggen.
            //Hvis serveren kjører lenger enn en dag så vil den legge til nye logger hver dag (nye filer?)
            _log.LogInformation("Hallo loggen!");
            return _db.HentAlle();
        }

        public Task<bool> Slett(int id)
        {
            return _db.Slett(id);
        }

        public Task<Kunde> HentEn(int id)
        {
            return _db.HentEn(id);
        }

        public Task<bool> Endre(Kunde endreKunde)
        {
            return _db.Endre(endreKunde);
        }
    }
}

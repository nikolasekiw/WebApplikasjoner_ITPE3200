using System.Collections.Generic;
using System.Threading.Tasks;
using KundeApp2.DAL;
using KundeApp2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KundeApp2.Controllers
{
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        private IKundeRepository _db;

        private ILogger<KundeController> _log;

        public KundeController(IKundeRepository db, ILogger<KundeController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(Kunde innKunde)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Lagre(innKunde);
                if (!returOK)
                {
                    _log.LogInformation("Kunden kunne ikke lagres!");
                    return BadRequest("Kunden kunne ikke lagres");
                }
                return Ok("Kunde lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> HentAlle()
        {
            List<Kunde> alleKunder = await _db.HentAlle();
            return Ok(alleKunder); // returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> Slett(int id)
        {
            bool returOK = await _db.Slett(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av Kunden ble ikke utført");
                return NotFound("Sletting av Kunden ble ikke utført");
            }
            return Ok("Kunde slettet");

        }


        public async Task<ActionResult> HentEn(int id)
        {
            if (ModelState.IsValid)
            {
                Kunde kunden = await _db.HentEn(id);
                if (kunden == null)
                {
                    _log.LogInformation("Fant ikke kunden");
                    return NotFound("Fant ikke kunden");
                }
                return Ok(kunden);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> Endre(Kunde endreKunde)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Endre(endreKunde);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av kunden kunne ikke utføres");
                }
                return Ok("Kunde endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> LoggInn(Bruker bruker) //dette er den modellen vi har laget.
        {
            if (ModelState.IsValid) //dette vil si hvis valideringen/regex godkjennes i modellen (Bruker.cs)
            {
                bool returnOK = await _db.LoggInn(bruker); //prøver å finne bruker.
                if (!returnOK) //hvis innloggingen ikke går ok, så logges det at innloggingen gikk feil for bruker. 
                {
                    _log.LogInformation("Innloggingen feilet for bruker"+bruker.Brukernavn);
                    return Ok(false);
                }
                return Ok(true); //returnerer true hvis validering gikk bra og hvis LoggInn metoden fant den gitte brukeren i DB
            }
            _log.LogInformation("Feil i inputvalidering"); //hvis inputvalideringen gikk galt så kommer man rett hit ved første if
            return BadRequest("Feil i inputvalidering på server");
        }
    }
}

    

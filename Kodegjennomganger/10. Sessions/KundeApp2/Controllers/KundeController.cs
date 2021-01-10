using System.Collections.Generic;
using System.Threading.Tasks;
using KundeApp2.DAL;
using KundeApp2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KundeApp2.Controllers
{
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        private IKundeRepository _db;

        private ILogger<KundeController> _log;

        //Først definerer vi denne. Dette gjøres sånn at vi kan bruke variabelen _loggetInn istedenfor å skrive inn
        //stringen "loggetInn" hver gang jeg skal bruke den nøkkelen inne i sesjonen og det fordi da slipper jeg å få noen skrivefeil
        private const string _loggetInn = "loggetInn";

        public KundeController(IKundeRepository db, ILogger<KundeController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(Kunde innKunde)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
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
            //hvis loggetInn sesjonen er null eller tom, så sender jeg tilbake en Unauthorized, HTTP kode 401, til klienten.
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            //Hvis jeg ikkke er logget inn, så gjøres ikke dette, da lister jeg ikke kundene for da er jeg ikke logget inn. 
            List<Kunde> alleKunder = await _db.HentAlle();
            return Ok(alleKunder); 
        }

        public async Task<ActionResult> Slett(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
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

        /**
         * 
        **/
        public async Task<ActionResult> LoggInn(Bruker bruker) 
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    //Hvis den ikke lykkes å logge inn, så setter jeg session _loggetInn og en tom streng. 
                    _log.LogInformation("Innloggingen feilet for bruker"+bruker.Brukernavn);
                    HttpContext.Session.SetString(_loggetInn,"");
                    return Ok(false);
                }
                //hvis innloggingen gikk bra, så setter jeg HttoContext.Session.SetString, så -loggetInn, det er
                //nøkkleen inn i session, altså session ID, også setter vi hvilken streng den skal ha, og den skal
                //ha strengen "LoggetInn".
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        /**
         * Når jeg logger ut kalles HttpContext.Session.SetStreng og _loggetInn og tom streng. 
         * Så på alle metodene, så sjekker jeg om _loggInn sesjonen er null eller empty, så sender jeg tilbake Unauthorized. 
         * Denne metodne resetter sesjonen
        **/
        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn,"");
        }
    }
}

    

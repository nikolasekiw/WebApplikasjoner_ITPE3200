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

        /**
         * Nå skal vi gjøre inputvalidering på server. Det vil si at f.eks. innKunde er av et visst format, tar regex på den for å sjekke input.
         * Vi kunne ha satt inn en regex i metoden under, f.eks. i if-setningen og sjekket om innKunde oppfylte det, men det mest vanlige er å 
         * gjøre det på modellen.
         * 
         * Når vi tar inn innKunde (og har nå fikset regex i modellen), så kan vi sjekke mot modellen om den er gyldig eller ikke. 
        **/
        public async Task<ActionResult> Lagre(Kunde innKunde)
        {
            //denne if-setningen vil si "hvis modellen er valid/ok", den skjønner at det er modellen den skal se ut ifra.
            //Og da gjør vi det som er inne i den if-setningen. Hvis man går inn der så vil det si at ModelState er valid.
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
            //Dette her er hvis ModelState ikke er valid og vi sender en BadRequest tilbake med denne informasjonen.
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
    }
}

    

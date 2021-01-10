using System.Collections.Generic;
using System.Threading.Tasks;
using KundeApp2.DAL;
using KundeApp2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KundeApp2.Controllers
{
    /**
    * Når vi feilhåndterer sender vi meldinger til klienten vha. HTTP-meldingen og
    * da kan vi også legge på meldinger til klienten. F.eks. hvis vi returnerer bool (SlettEn)
    * til klienten, men vi ønsker å gjøre dette litt fleksibelt og gjøre det mer standardisert, 
    * dvs. at vi kommuniserer med HTTP statuser. 
    * 
    * Det vi gjør er å endre på Task, og de skal ikke returnere bool, men en ActionResult.
    * ActionResult er en type som er generell og den kan returnere det meste, men i vårt
    * tilfelle så er det HTTP-meldingene den skal returnere. 
    **/
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

        public async Task<ActionResult> Lagre (Kunde innKunde)
        {
            //Den skal returnere avhengig av om den er OK eller ikke
            bool returOK = await _db.Lagre(innKunde);
            if (!returOK)
            {
                //Hvis ikke OK så skal vi logge den informasjonen
                _log.LogInformation("Kunden kunne ikke lagres!");
                //så sender vi BadRequest, altså HTTP meldingen med en statuskode (bad request) tilbake
                return BadRequest("Kunden kunne ikke lagres"); //i string: informasjon om hva som gikk galt
            }
            //Hvis det gikk bra så returnerer vi OK med en eventuell melding.
            return Ok("Kunde lagret");
        }

        /**
         * Siden vi istadenfor List<Kunde> må returnere ActionResult som da blir HTTP meldingen, så
         * må vi da skrive List<Kunde> inne i metoden for å fortsatt returnere denne.
        **/
        public async Task<ActionResult> HentAlle()
        {
            //skal returnere HTTP-statusen som er OK også skal vi returnere alle kundene.
            List<Kunde> alleKunder = await _db.HentAlle();
            return Ok(alleKunder); // returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> Slett(int id)
        {
            //her må vi også definere bool fordi vi ikke lenger returnerer bool i metodedefinisjonen
            bool returOK = await _db.Slett(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av Kunden ble ikke utført");
                //Her kjører vi HTTP-statusen NotFound
                return NotFound("Sletting av Kunden ble ikke utført");
            }
            return Ok("Kunde slettet");

        }


        public async Task<ActionResult> HentEn(int id)
        {
            Kunde kunden = await _db.HentEn(id);
            if (kunden == null)
            {
                _log.LogInformation("Fant ikke kunden");
                return NotFound("Fant ikke kunden");
            }
            return Ok(kunden);
        }

        public async Task<ActionResult> Endre(Kunde endreKunde)
        {
            bool returOK = await _db.Endre(endreKunde);
            if (!returOK)
            {
                _log.LogInformation("Endringen kunne ikke utføres");
                return NotFound("Endringen av kunden kunne ikke utføres");
            }
            return Ok("Kunde endret");
        }
    }
}

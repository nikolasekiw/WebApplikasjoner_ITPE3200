using System.Collections.Generic;
using System.Threading.Tasks;
using ITPE3200Mappe3.DAL;
using ITPE3200Mappe3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KundeSpm = ITPE3200Mappe3.DAL.KundeSpm;

namespace ITPE3200Mappe3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KundeserviceController : ControllerBase
    {
        private IKundeserviceRepository _db;
        private ILogger<KundeserviceController> _log;

        public KundeserviceController(IKundeserviceRepository db, ILogger<KundeserviceController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost]
        public async Task<ActionResult> Lagre(KundeSpm innSpm)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Lagre(innSpm);
                if (!returOK)
                {
                    _log.LogInformation("Spørsmålet kunne ikke lagres!");
                    return BadRequest();
                }
                return Ok();
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest();
        }

        [HttpGet("hentKundeSpm")]
        public async Task<ActionResult> HentAlleKundeSpm()
        {
            List<Kundespm> alleKundeSpm = await _db.HentAlleKundeSpm();
            return Ok(alleKundeSpm);
        }
        
        [HttpGet("hentAlleFAQ")]
        public async Task<ActionResult> HentAlleFAQ()
        {
            List<Faq> alleFAQ = await _db.HentAlleFAQ();
            return Ok(alleFAQ);
        }
        
        [HttpGet("hentAlleKat/{kategori}")]
        public async Task<ActionResult> Kategorier(string kategori)
        {
            List<FAQ> alleKat = await _db.Kategorier(kategori);
            return Ok(alleKat);
        }
    }
}
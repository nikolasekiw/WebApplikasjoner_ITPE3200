using System.Threading.Tasks;
using ITPE3200Mappe3.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITPE3200Mappe3.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class Rating : ControllerBase
        {
            private IKundeserviceRepository _db;
            private ILogger<Rating> _log;

            public Rating(IKundeserviceRepository db, ILogger<Rating> log)
            {
                _db = db;
                _log = log;
            }

            [HttpPost("tommelned")]
            public async Task<ActionResult> TommelNed([FromBody] int id)
            {
                bool returOK = await _db.TommelNed(id);
                if (!returOK)
                {
                    _log.LogInformation("Kunne ikke hente ratingen");
                    return BadRequest();
                }
                return Ok();
            }
            
            [HttpPost("tommelopp")]
            public async Task<ActionResult> TommelOpp([FromBody] int id)
            {
                bool returOK = await _db.TommelOpp(id);
                if (!returOK)
                {
                    _log.LogInformation("Kunne ikke hente ratingen");
                    return BadRequest();
                }
                return Ok();
            }
        }
    }

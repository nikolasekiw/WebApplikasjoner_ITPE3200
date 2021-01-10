using EF_2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KundeOrdre.Controllers
{
    [Route("[controller]/[action]")]

    public class HomeController:ControllerBase
    {
        private readonly DB _db;

        public HomeController(DB db)
        {
            _db = db;
        }

        /**
         * Det denne gjør pga. lazy-loading er returnere_db.Kunde.ToList().
         * Da får vi med oss hele strukturen. Det er en ting vi må gjøre i tillegg, 
         * og det er i Startup.cs på service.AddControllers
        **/
        public List<Kunde> index()
        {
            return _db.Kunde.ToList();
        }
    }
}

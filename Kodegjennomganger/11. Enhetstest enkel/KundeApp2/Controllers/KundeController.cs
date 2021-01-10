using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KundeApp2.DAL;
using KundeApp2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KundeApp2.Controllers
{
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        /**
         * Vi skal enhetsteste kundecontrolleren, altså de metodene som er under. Så skal vi erstatte
         * repository slik at vi ikke kaller databasen, men mokker den/stubber den. For å lage tester så 
         * lager vi et nytt prosjekt. Da høyreklikker vi på "KundeApp2" så trykker på Add --> New Project,
         * så velge Test på høyre side, så xUnit Test Project. Så går vi inn på KundeControllerTest.cs filen
        **/
        private readonly IKundeRepository _db;

        // merk: denne depedency injection må registreres i Setup.cs for å fungere!
        public KundeController(IKundeRepository db)
        {
            _db = db;
        }
              
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

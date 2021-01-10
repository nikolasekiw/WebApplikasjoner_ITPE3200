using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Angular1.KundeController
{
    [Route("api/[controller]")] // REST ved ikke å bruke [action] her 
    public class KundeController
    {
        private List<Kunde> alleKunder = new List<Kunde>();

        /**
         * Når vi kaller med et get-kall fra klienten så prøver den å finne en 
         * public list kunde som heter eller/og at den leter etter en metode som har
         * en dekoratør som heter HttpGet. Under har vi bare en list av kunde hvor vi
         * legger inn to kunder, så adderer de i alleKunder og returnerer alleKunder. 
         * Da må vi ha en modell --> Kunde.cs
        **/

        [HttpGet] // eller Get() som metodenavn
        public List<Kunde> Hent() 
        {
            var kunde1 = new Kunde()
            {
                id = 1,
                fornavn = "Ole",
                etternavn = "Hansen",
                adresse = "Osloveien 82",
                postnummer = "0270",
                poststed = "Oslo"
            };

            var kunde2 = new Kunde()
            {
                id = 2,
                fornavn = "Line",
                etternavn = "Jensen",
                adresse = "Askerveien 82",
                postnummer = "1372",
                poststed = "Asker"
            };

            alleKunder.Add(kunde1);
            alleKunder.Add(kunde2);
            return alleKunder;
        } 
    }
}

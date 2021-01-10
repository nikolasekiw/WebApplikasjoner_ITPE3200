using System.Collections.Generic;

namespace EF_2.Models
{
    //En vare kan ha en eller flere ordrelinjer
    //Altså hvis man har behov å finne en vare og hvilke ordrelinjer denne varen ligger i så må vi ha med denne.
    public class Vare
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public double Pris { get; set; }
        public virtual List<OrdreLinje> OrdreLinjer { get; set; }
    }
}
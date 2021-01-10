using System.Collections.Generic;

namespace EF_2.Models
{
    //ordre kan ha en kunde og en eller flere ordrelinjer
    public class Ordre
    {
        public int Id { get; set; }
        public string Dato { get; set; }
        public virtual Kunde Kunde { get; set; } //tilbakeknytningt til kunde. Hvis man ønsker at det skal være mulig å gå fra ordren til kunden og ikke bare fra ordren til kunden.
        public virtual List<OrdreLinje> OrdreLinjer { get; set; }
    }
}
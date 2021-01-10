namespace EF_2.Models
{
    //En ordrelinje kan ha en vare og en ordre, men en vare kan ha knytning til mange ordrelinjer
    public class OrdreLinje
    {
        public int Id { get; set; }
        public int Antall { get; set; } //antall varer vi bestiller
        public virtual Vare Vare { get; set; }
        public virtual Ordre Ordre { get; set; } //ordre er kun dersom man har behov for å gå inn i en ordrelinje og finne hvilken ordre den har

        /**
         * Disse er virtuel/lazy-loading, og det er hele poenget, at ved å lage
         * denne strukturen
        **/
    }
}
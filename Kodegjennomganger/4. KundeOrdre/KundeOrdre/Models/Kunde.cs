using System.Collections.Generic;

namespace EF_2.Models
{
    //Kunde har en eller flere ordre (en-til-mange relasjon)
    public class Kunde
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public virtual List<Ordre> Ordre { get; set; }
    }
}

//I dette prosjektet vises hvordan dette henger sammen med entity framework og kundecontext
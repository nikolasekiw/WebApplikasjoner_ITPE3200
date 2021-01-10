using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebapplikasjonerOppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public class Kunde
    {
        [Key]
        public int KId { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Telefonnummer { get; set; }
        public string Epost { get; set; }
        public string Kortnummer { get; set; }
    }
}

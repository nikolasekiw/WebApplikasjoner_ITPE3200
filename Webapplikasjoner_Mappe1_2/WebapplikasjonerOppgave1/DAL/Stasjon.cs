using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebapplikasjonerOppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public class Stasjon
    {
        [Key]
        public int SId { get; set; }
        public string StasjonsNavn { get; set; }
    }
}

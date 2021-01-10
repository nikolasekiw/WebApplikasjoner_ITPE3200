using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace WebapplikasjonerOppgave1.DAL
{
    [ExcludeFromCodeCoverage]
    public class Turer
    {
        [Key]
        public int TurId { get; set; }
        public virtual Stasjon StartStasjon { get; set; }
        public virtual Stasjon EndeStasjon { get; set; }
        public string Dato { get; set; }
        public string Tid { get; set; }
        public int BarnePris { get; set; }
        public int VoksenPris { get; set; }
    }
}

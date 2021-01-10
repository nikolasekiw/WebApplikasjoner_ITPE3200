using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebapplikasjonerOppgave1.Models
{
    [ExcludeFromCodeCoverage]
    public class BussBestilling
    {
        public int Id { get; set; }

        //Kunde
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Fornavn { get; set; }

        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$")]
        public string Etternavn { get; set; }

        [RegularExpression(@"^[0-9]{8}$")]
        public string Telefonnummer { get; set; }

        [RegularExpression(@"^\S+@\S+$")]
        public string Epost { get; set; }

        [RegularExpression(@"^[0-9]{16}$")]
        public string Kortnummer { get; set; }

        //Bestilling
        [RegularExpression(@"^[0-9]{1}$")]
        public int AntallBarn { get; set; }

        [RegularExpression(@"^[0-9]{1}$")]
        public int AntallVoksne { get; set; }

        [RegularExpression(@"^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$")]
        public string Dato { get; set; }

        public string Tid { get; set; }

        public double BarnePris { get; set; }

        public double VoksenPris { get; set; }

        public string StartStasjon { get; set; }

        public string EndeStasjon { get; set; }

    }

}

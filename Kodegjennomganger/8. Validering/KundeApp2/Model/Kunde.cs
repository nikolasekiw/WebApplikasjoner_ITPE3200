using System.ComponentModel.DataAnnotations;

namespace KundeApp2.Model
{
     public class Kunde
     {
        public int Id { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string Fornavn { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string Etternavn { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,50}")]
        public string Adresse { get; set; }

        [RegularExpression(@"[0-9]{4}")]
        public string Postnr { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,20}")]
        public string Poststed { get; set; }
     }
}

/**
 * Dette er en kunden som blir overført mellom klient og tjener. Enten en kunde eller en liste av Kunde. 
 * Det man gjør er å sette dekoratører "[]" foran en attributt og skriver RegularExpression i den. Det er en 
 * metoden som begynner med "@" og en streng. Da skal regex inn i den strengen. Regex-en skal inn i "[]" og i "{}"
 * skriver vi hvor lang den skal være. Kunne satt en "^" for start og stopp foran regex, men gjør det heller sånn
 * at det er slik vi har det i javascript. 
**/

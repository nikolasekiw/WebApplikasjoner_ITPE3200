
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebapplikasjonerOppgave1.Models {

    [ExcludeFromCodeCoverage]
    public class Bruker
    {
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ\.\\-]{2,20}$")]
        public string Brukernavn { get; set; }
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$")]
        public string Passord { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace ITPE3200Mappe3.Model
{
    public class Kundespm
    {
        public int Id { get; set; }
        
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,30}")]
        public string Fornavn { get; set; }
        
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,30}")]
        public string Etternavn { get; set; }
        
        [RegularExpression(@"[\w-\.]+@([\w-]+\.)+[\w-]{2,4}")]
        public string Epost { get; set; }
        
        [RegularExpression(@"[a-zA-ZæøåÆØÅ.,?! \-]{2,200}")]
        public string NyttSporsmal { get; set; }
    }
}
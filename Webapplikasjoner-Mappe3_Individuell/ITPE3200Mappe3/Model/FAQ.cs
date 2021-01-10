using System.ComponentModel.DataAnnotations;

namespace ITPE3200Mappe3.Model
{
    public class Faq
    {
        public int id { get; set; }
        public string sporsmal { get; set; }
        public string svar { get; set; }
        public int tommelOpp { get; set; }
        public int tommelNed { get; set; }
        public string kategori { get; set; }
    }
}
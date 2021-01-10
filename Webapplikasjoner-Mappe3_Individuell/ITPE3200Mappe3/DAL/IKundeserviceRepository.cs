using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ITPE3200Mappe3.Model;

namespace ITPE3200Mappe3.DAL
{
    public interface IKundeserviceRepository
    {
        Task<bool> Lagre(KundeSpm innSpm);
        Task<List<Faq>> HentAlleFAQ();
        Task<List<Kundespm>> HentAlleKundeSpm();
        Task<List<FAQ>> Kategorier(string kategori);
        Task<bool> TommelOpp(int id);
        Task<bool> TommelNed(int id);
    }
}
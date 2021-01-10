using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ITPE3200Mappe3.Model;

namespace ITPE3200Mappe3.DAL
{
    public class KundeserviceRepository : IKundeserviceRepository
    {
        private readonly KundeserviceContext _db;

        public KundeserviceRepository(KundeserviceContext db)
        {
            _db = db;
        }

        public async Task<bool> Lagre(KundeSpm innSpm)
        {
            try
            {
                var nyttSpm = new KundeSpm();
                nyttSpm.Fornavn = innSpm.Fornavn;
                nyttSpm.Etternavn = innSpm.Etternavn;
                nyttSpm.Epost = innSpm.Epost;
                nyttSpm.NyttSporsmal = innSpm.NyttSporsmal;

                _db.KundeSpm.Add(nyttSpm);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Faq>> HentAlleFAQ()
        {
            try
            {
                List<Faq> alleFaq = await _db.FAQ.Select(f => new Faq
                {
                    id = f.id,
                    sporsmal = f.sporsmal,
                    svar = f.svar,
                    tommelOpp = f.tommelOpp,
                    tommelNed = f.tommelNed,
                    kategori = f.kategori
                }).OrderByDescending(x => x.tommelOpp)
                    .ToListAsync();
                return alleFaq;
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<List<Kundespm>> HentAlleKundeSpm()
        {
            try
            {
                List<Kundespm> alleKundeSpm = await _db.KundeSpm.Select(k => new Kundespm
                {
                    Id = k.Id,
                    Fornavn = k.Fornavn,
                    Etternavn = k.Etternavn,
                    Epost = k.Epost,
                    NyttSporsmal = k.NyttSporsmal
                }).ToListAsync();
                return alleKundeSpm;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<FAQ>> Kategorier(string kategori)
        {
            List<FAQ> alleKat = await _db.FAQ
                .Where(x => x.kategori == kategori)
                .OrderByDescending(x => x.tommelOpp)
                .ToListAsync();
            return alleKat;
        }
        
        public async Task<bool> TommelOpp(int id)
        {
            try
            {
                var rating = _db.FAQ.FirstOrDefault(q => q.id == id);
                rating.tommelOpp += 1;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> TommelNed(int id)
        {
            try
            {
                var rating = _db.FAQ.FirstOrDefault(q => q.id == id);
                rating.tommelNed += 1;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
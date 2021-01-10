using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebapplikasjonerOppgave1.Models;

namespace WebapplikasjonerOppgave1.DAL
{
    public interface IBussBestillingRepository
    {
        Task<List<Stasjon>> HentAlleStasjoner();

        Task<List<Turer>> HentAlleTurer();
        Task<List<Stasjon>> HentEndeStasjoner(string startStasjonsNavn);
        Task<bool> Lagre(BussBestilling innBussBestilling);
        Task<bool> OpprettTur(Tur innTur);
        Task<bool> EndreTur(Tur endreTur);
        Task<bool> SlettTur(int TurId);
        Task<bool> LoggInn(Bruker bruker);
    }
}

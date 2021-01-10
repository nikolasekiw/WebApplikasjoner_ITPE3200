using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using KundeApp2.DAL;

namespace KundeApp2.Model
{
    public static class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
           
            var db = serviceScope.ServiceProvider.GetService<KundeContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var poststed1 = new Poststeder {Postnr = "0270", Poststed = "Oslo"};
            var poststed2 = new Poststeder {Postnr = "1370", Poststed = "Asker"};

            var kunde1 = new Kunder { Fornavn = "Ole", Etternavn = "Hansen", Adresse = "Olsloveien 82", Poststed = poststed1};
            var kunde2 = new Kunder { Fornavn = "Line", Etternavn = "Jensen", Adresse = "Askerveien 72", Poststed = poststed2 };

            db.Kunder.Add(kunde1);
            db.Kunder.Add(kunde2);

            // lag en påoggingsbruker
            var bruker = new Brukere(); //Brukere tabellen
            bruker.Brukernavn = "Admin"; //legger inn brukernavn i bruker. 
            var passord = "Test11"; //det kunne ha stått string her, men var går også.
            byte[] salt = KundeRepository.LagSalt(); //lager et tilfeldig salt på 24 bites i et array. 
            byte[] hash = KundeRepository.LagHash(passord, salt); //lager hash med det passordet som er oppgitt og saltet. 
            bruker.Passord = hash; //lagrer hash i bruker sitt passsord. 
            bruker.Salt = salt; //lagrer saltet i bruker sitt salt.
            db.Brukere.Add(bruker); //adder brukeren til brukere, altså selve tabellen. 

            db.SaveChanges(); //så over til KundeController.
        }
    }
       
}

using System;
using Xunit;
using Moq; // Må legge til pakken Moq.EntityFreamworkCore
using KundeApp2.Controllers; // må legge til en prosjektreferanse i Project-> Add Reference -> Project. Da lages en referanse i KundeTest til å kunne lese klasser i hoved-prosjektet
using KundeApp2.DAL;
using KundeApp2.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KundeApp2Test
{
    public class KundeControllerTest
    {
        /**
         * Her lager jeg tre kunder. 
        **/
        [Fact] //alle enhetstester har denne dekoratøren. Fast: et faktum, det er det vi definerer i testen
        public async Task HentAlle() //vi kjører async, derfor har vi ikke void her, async Task er det samme som void
        {
            //Asssert
            var kunde1 = new Kunde
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Adresse = "Askerveien 82",
                Postnr = "1370",
                Poststed = "Asker"
            };
            var kunde2 = new Kunde
            {
                Id = 2,
                Fornavn = "Ole",
                Etternavn = "Olsen",
                Adresse = "Osloveien 82",
                Postnr = "0270",
                Poststed = "Oslo"
            };
            var kunde3 = new Kunde
            {
                Id = 1,
                Fornavn = "Finn",
                Etternavn = "Finnsen",
                Adresse = "Bergensveien 82",
                Postnr = "5000",
                Poststed = "Bergen"
            };

            //oppretter en List av Kunde, kundeListe og legger til de tre kundene. 
            var kundeListe = new List<Kunde>();
            kundeListe.Add(kunde1);
            kundeListe.Add(kunde2);
            kundeListe.Add(kunde3);

            //lager mock av IKundeRepository
            var mock = new Mock<IKundeRepository>();
            //den skal returnere listen med de kundenee vi opprettet når den kalles.
            mock.Setup(k => k.HentAlle()).ReturnsAsync(kundeListe); // merk: parantesene, de må stå på rett plass ellers fås rare feilmeldinger!
            var kundeController = new KundeController(mock.Object); //tar new KundeController
            List<Kunde> resultat = await kundeController.HentAlle(); //Dette er Action
            Assert.Equal<List<Kunde>>(kundeListe,resultat); //Dette er Assert, sammenligningen?
            //Equal kan ta en type og her er det List av Kunde. resultat er en List av Kunde og kundeListe er også en List av Kunde.
            //Så disse to sammenlignes attributt for attrubitt, ved hjelp av Assert.Equal og typen, at kundeListe og resultat er av samme type.
        }

        /**
        * Vi skal ha noe som heter Arrange: betyr --> legg opp testen/forbered den. Da lager vi en kunde,
        * også lager vi en fiktiv kunde. Så må vi mocke dette KundeRepositoriet. Dvs. at vi skal late som
        * om vi kaller KundeRepository, men vi gjør ikke det, og det gjør vi med mock.
        **/
        [Fact]
        //Arrange
        public async Task Lagre()
        {
            var innKunde = new Kunde
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Adresse = "Askerveien 82",
                Postnr = "1370",
                Poststed = "Asker"
            };
            //Mock i KundeRepository. Da har vi laget en mock, en variabel som kan brukes til å erstatte kallet til repository. Skal ikke gå mot db.
            var mock = new Mock<IKundeRepository>();
            //Så setter vi opp den mock-en. Altså: vi beskriver hvordan mock skal fungere. Kallet er et lambda-uttrykk
            //k står for kunde. Kaller kunde sin lagre. Og jeg skal lagre innKunde. Etter det skriver vi ReturnAsync(true)
            //Vi returnerer true fordi lagre-metoden i repository returnerer bool. Hvis det går bra returnerer den true, hvis ikke
            //så returnerer den false. Altså: det kallet til lagre innKunde, det er kallet i kunderepository-et.
            //Dvs: når vi kaller på lagre i kundeController, så er det denne metoden her vi skal kjøre istedenfor den som er i kundeRepository.
            //Når vi kaller på IKunderepository så er det denne metoden den skal kjøre, og den skal returnere true. Det er repo som jeg faker
            mock.Setup(k => k.Lagre(innKunde)).ReturnsAsync(true); //her erstatter vi kallet til repository. Og sier at når controlleren kaller repository lagre, så skal den returnere true. 
            //Må ta new på kundeController. KundeControlleren tar et interface/objekt. Det er den mock-en som gjør at vi kan kalle den med
            //den kundeController vi opprettet her istedenfor repository. KundeController skal ta inn mock sitt objekt. Alt dette var arrange
            var kundeController = new KundeController(mock.Object);
            //Så er det "Act". Her skal vi gjøre selve kallet. Må ha await her fordi dette er et asynkront kall.
            //Dette er selve kjøringen av lagre. Dette er lagre i mock-en, altså erstatte kallet til repository.
            bool resultat = await kundeController.Lagre(innKunde); //her er selve kallet og det fører til et resultat som igjen er true eller false.
            //Assert. Her sammenligner vi. Jeg sjekker om resultatet er true. 
            Assert.True(resultat);
            //Så skal testen kjøres. Ta trykker jeg på Run --> Run Unit Tests (funket ikke). Kan da ta View --> Pads --> Unit Tests
            //Så får vi et vindu på siden til høyre. 
        }

        [Fact]
        public async Task HentEn()
        {
            //Arrange = data som skal testes
            var returKunde = new Kunde
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Adresse = "Askerveien 82",
                Postnr = "1370",
                Poststed = "Asker"
            };
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.HentEn(1)).ReturnsAsync(returKunde); //denne skal returnere den fiktive kunden vi definerte/opprettet over
            var kundeController = new KundeController(mock.Object);
            //Act = det som skal gjøres
            Kunde resultat = await kundeController.HentEn(1); //1 fordi Kunde id er 1
            //Assert = sammenligning
            Assert.Equal<Kunde>(returKunde, resultat);
            //sjekker om Assert.Equal av type Kunde.. (kundeController.HentEn(1), skal returnere en kunde
            //og resultatet vil da bli den kunden som vi mocker i repository (returnKunde), så sammenligner vi resultatet og returKunde
            //vha. Assert sin Equal kundetypen og sammenligner returnKunde og resultat attributt for attributt.
        }

        [Fact]
        public async Task Slett()
        {
            var mock = new Mock<IKundeRepository>();
            //slett skal returnere true eller false. Tar ReturAsync(true) når jeg tar slett(1), når jeg finner en.
            mock.Setup(k => k.Slett(1)).ReturnsAsync(true); 
            var kundeController = new KundeController(mock.Object);
            bool resultat = await kundeController.Slett(1); //slett returnerer en boolsk variabel
            Assert.True(resultat); //true her fordi den returnerer true.
            //Altså: vi tar metoden i KundeController, Slett(1) og returnerer true
        }

        [Fact]
        public async Task Endre()
        {
            //lager en fiktiv kunde, det er den som skal endres
            var innKunde = new Kunde
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Adresse = "Askerveien 82",
                Postnr = "1370",
                Poststed = "Asker"
            };
            var mock = new Mock<IKundeRepository>();
            //Når k sin endre, altså repository metoden Endre skal kalles med innKunde, så skal den returnere true. 
            mock.Setup(k => k.Endre(innKunde)).ReturnsAsync(true);
            var kundeController = new KundeController(mock.Object);
            bool resultat = await kundeController.Endre(innKunde); //vi tar bool resultat fordi Endre returnerer en boolsk variabel.
            //Ettersom ReturnAsync er true så skal resultat også returnere true.
            Assert.True(resultat);
        }
    }
}

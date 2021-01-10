using System;
using Xunit;
using Moq; // Må legge til pakken Moq.EntityFreamworkCore
using KundeApp2.Controllers; // må legge til en prosjektreferanse i Project-> Add Reference -> Project
using KundeApp2.DAL;
using KundeApp2.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace KundeApp2Test
{
    public class KundeControllerTest
    {
        /**
         * Her har jeg HentAlle, men jeg burde kanskje også teste om jeg fikk null tilbake,
         * altså at det ikke kom noe tilbake i det hele tatt.
        **/
        [Fact]
        public async Task HentAlle()
        {
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

            var kundeListe = new List<Kunde>();
            kundeListe.Add(kunde1);
            kundeListe.Add(kunde2);
            kundeListe.Add(kunde3);

            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.HentAlle()).ReturnsAsync(kundeListe); // merk: parantesene, de må stå på rett plass ellers fås rare feilmeldinger!
            var kundeController = new KundeController(mock.Object);
            List<Kunde> resultat = await kundeController.HentAlle();
            Assert.Equal<List<Kunde>>(kundeListe,resultat);
        }

        /**
         * Skal teste HentSlleTomListe. Vi ser at dette er to forskjellige ting. Fordi hvis vi ser på HentAlle
         * i KundeRepository, så returnerer HentAlle null i noen tulfeller, og da bør vi teste på det. Altså:
         * KundeController vil også returnere null noen ganger, hvis det er noe feil.
         * 
         * Derfor så bør vi teste om den returnerer null når vi skriver inn null i ReturAsync, altså at det kommer null fra Repository.
         * Jeg bare oppretter en tom liste av kunde. Så returnerer null fra repository og sjekker at resultatet blir null (i Assert).
         * 
        **/
        [Fact]
        public async Task HentAlleTomListe()
        {
            var kundeListe = new List<Kunde>();
            
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.HentAlle()).ReturnsAsync(()=>null); // merk: parantesene, de må stå på rett plass ellers fås rare feilmeldinger!
            var kundeController = new KundeController(mock.Object);
            List<Kunde> resultat = await kundeController.HentAlle();
            Assert.Null(resultat);
        }

        [Fact]
        public async Task LagreOK() //den er den samme som på enkel test
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
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.Lagre(innKunde)).ReturnsAsync(true);
            var kundeController = new KundeController(mock.Object);
            bool resultat = await kundeController.Lagre(innKunde);
            Assert.True(resultat);
        }

        /**
         * LagreIkkeOk, det er fordi Lagre returnerer true eller false, derfor så bør vi også teste hva som 
         * skjer når Repository returnerer false. Jo, da må resultatet fra kundeController.Lagre(innKunde) også være false.
        **/
        [Fact]
        public async Task LagreIkkeOK()
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
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.Lagre(innKunde)).ReturnsAsync(false);
            var kundeController = new KundeController(mock.Object);
            bool resultat = await kundeController.Lagre(innKunde);
            Assert.False(resultat);
        }

        [Fact]
        public async Task HentEnOK()
        {
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
            mock.Setup(k => k.HentEn(1)).ReturnsAsync(returKunde);
            var kundeController = new KundeController(mock.Object);
            Kunde resultat = await kundeController.HentEn(1);
            Assert.Equal<Kunde>(returKunde, resultat);
        }

        /**
         * Igjen, så returnerer vi enten en kunde eller null fra repository, derfor så må vi teste for
         * hva som skjer når null kommer fra repository. Jo, da skal vi ha et resultat og det resultatet skal
         * være null.
        **/
        [Fact]
        public async Task HentEnIkkeOK()
        {
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.HentEn(1)).ReturnsAsync(()=>null);
            var kundeController = new KundeController(mock.Object);
            Kunde resultat = await kundeController.HentEn(1);
            Assert.Null(resultat);
        }

        [Fact]
        public async Task SlettOK() 
        {
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.Slett(1)).ReturnsAsync(true);
            var kundeController = new KundeController(mock.Object);
            bool resultat = await kundeController.Slett(1);
            Assert.True(resultat);
        }

        [Fact]
        public async Task SlettIkkeOK() //her er det igjen true eller false. Metoden for true og false er så og si like.
        {
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.Slett(1)).ReturnsAsync(false);
            var kundeController = new KundeController(mock.Object);
            bool resultat = await kundeController.Slett(1);
            Assert.False(resultat); //Assert false når vi får false fra repository. Det kan bety at databasen er nede eller lignende.
        }

        [Fact]
        public async Task EndreOK()
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
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.Endre(innKunde)).ReturnsAsync(true);
            var kundeController = new KundeController(mock.Object);
            bool resultat = await kundeController.Endre(innKunde);
            Assert.True(resultat);
        }

        [Fact]
        public async Task EndreIkkeOK() //igjen true eller false, skriver bare false istedenfor true
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
            var mock = new Mock<IKundeRepository>();
            mock.Setup(k => k.Endre(innKunde)).ReturnsAsync(false); //returnerer false fra mock og da vil vi asserte at vi får false tilbake fra kundeController endre en kunde.
            var kundeController = new KundeController(mock.Object);
            bool resultat = await kundeController.Endre(innKunde);
            Assert.False(resultat);
        }
    }
}

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using KundeApp2.Controllers;
using KundeApp2.DAL;
using KundeApp2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KundeAppTest
{
    public class KundeAppTest
    {
        //Dette er tilsvarende som er i applikasjonen, for å sette sessions.
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        //Vi mocker i kunderepository slik at vi ikke går mot databasen.
        private readonly Mock<IKundeRepository> mockRep = new Mock<IKundeRepository>();

        //Så må vi også mocke logger-funksjonen
        private readonly Mock<ILogger<KundeController>> mockLog = new Mock<ILogger<KundeController>>();

        /**
         * Så må vi mocke sessionen, dvs. at vi kan styre om applikasjonen er logget inn eller ikke
         * dvs. at session er satt eller ikke. Og da må vi mocke HttpContext, også må vi lage
         * en ny klasse som MockHttpSession. Den MockHttpSessions.cs er lagt nede i kundeappTest
        **/
        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        /**
         * Dvs. at jeg skal hente alle kundene og logger inn først, også får jeg kundene ut igjen.
         * Lager kundene, legger de inn, setter opp hentAlle, returnerer kundelisten, 
         * mocker kundeController, så setter jeg mockSession (det er den variabelen vi definerte lenger opp.
         * Den setter jeg til loggetInn (nøkkelen loggetInn og strengen loggetInn. 
         * Så mocker jeg HttpContext med session og Returns(mockSession), returnerer altså
         * det objektet/den variabelen som jeg da har satt "loggetInn" i.
         * 
         * Så må jeg oppdatere kundeController sin ControllerContext sin HttpContext med
         * mockHttpContext (den som er en linje lenger opp) sitt objekt. Selve mockingen av HttpContext, s
         * så vi erstatter HttpContext-en også for å kunnde da sette session. 
         * 
         * På act: tar kundeController.HentAlle() as OkObjectResult; og på samme måte sjekker vi at
         * Http statuskoden er OK, og det kan vi gjøre med HttpStatusCode som er en type, sin OK, men da
         * må man caste den til en int. 
        **/
        [Fact]
        public async Task HentAlleLoggetInnOK()
        {
            // Arrange
            var kunde1 = new Kunde {Id = 1,Fornavn = "Per",Etternavn = "Hansen",Adresse = "Askerveien 82",
                                    Postnr = "1370",Poststed = "Asker"};
            var kunde2 = new Kunde {Id = 2,Fornavn = "Ole",Etternavn = "Olsen",Adresse = "Osloveien 82",
                                    Postnr = "0270",Poststed = "Oslo"};
            var kunde3 = new Kunde {Id = 1,Fornavn = "Finn",Etternavn = "Finnsen",Adresse = "Bergensveien 82",
                                    Postnr = "5000",Poststed = "Bergen"};

            var kundeListe = new List<Kunde>();
            kundeListe.Add(kunde1);
            kundeListe.Add(kunde2);
            kundeListe.Add(kunde3);

            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(kundeListe);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.HentAlle() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);
            Assert.Equal<List<Kunde>>((List<Kunde>)resultat.Value, kundeListe);
        }
        /**
         * Her kan vi lage en tom liste, men det er ikke nødvendig fordi nå kan vi gjøre dette
         * på en litt annen måte. 
        **/
        [Fact]
        public async Task HentAlleIkkeLoggetInn()
        {
            // Arrange

            //var tomListe = new List<Kunde>();

            /**
             * It.IsAny også typen. Den returnerer altså et eller annet, et objekt av type List<Kunde>
             * Det er ikke viktig hva den returnerer fordi vi skal ikke teste på det fordi jeg er ikke
             * logget inn og derfor er det ikke viktig hva som kommer ut her. Men det må være av riktig 
             * type. Da kan vi brukke mocken, altså klassen mock.It sin IsAny også typen. 
            **/
            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(It.IsAny<List<Kunde>>());

            //KundeController mocker repository og loggen. 
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //setter IKKE loggetInn i MockSession
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act, tar HentAlle på KundeController og da returnerer vi UnauthorizedObjectResult,
            //returnerer Unauthorized hvis vi ikke er logget inn. 
            var resultat = await kundeController.HentAlle() as UnauthorizedObjectResult;
           
            // Assert, sjekker at HttpStatisCode sin Unauthorized er lik resultatet sin statuskode
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            //og at vi får "Ikke logget inn" i resultatet sin value.
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        /**
         * Tilsvarende gjør vi her. Her kan jeg igjen opprette Kunde, men ettersom jeg ikke skal 
         * asserte på denne kunden etterpå, så trenger jeg ikke denne kunden.
        **/
        [Fact]
        public async Task LagreLoggetInnOK()
        {

            /*  Kan unngå denne med It.IsAny<Kunde>
            var kunde1 = new Kunde {Id = 1,Fornavn = "Per",Etternavn = "Hansen",Adresse = "Askerveien 82",
                                    Postnr = "1370",Poststed = "Asker"};
            */

            // Arrange, da kan jeg ta It.IsAny av type Kunde. Returnerer true.
            mockRep.Setup(k => k.Lagre(It.IsAny<Kunde>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //setter mockSession til loggetInn
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act, da skal vi få OK tilbake (OkObjectResult)
            var resultat = await kundeController.Lagre(It.IsAny<Kunde>()) as OkObjectResult;

            // Assert, sjekker på statuskoden
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            //Kunde lagret
            Assert.Equal("Kunde lagret", resultat.Value);
        }

        /**
         * Akkurat på samme måte her. LagreLoggetInnIkkeOK, altså feil fra db. 
         * Da returnerer vi false. 
        **/
        [Fact]
        public async Task LagreLoggetInnIkkeOK()
        {
            // Arrange
            //da returnerer vi false. Igjen vi tar inn kunde, ikke viktig hvilken kunde det er.
            //Det er en mock av en kunde. Det er som sagt fordi vi ikke skal teste på om den returnerer noe kunde
            //Den skal returnere badRequest. 
            mockRep.Setup(k => k.Lagre(It.IsAny<Kunde>())).ReturnsAsync(false);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act, vi kaller kundeControlleren lagre med en fake kunde og returnerer BadRequestObjectResult
            var resultat = await kundeController.Lagre(It.IsAny<Kunde>()) as BadRequestObjectResult;

            // Assert, sjekker om vi får badRequest som statuskode
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            // også strengen som sendes tilbake
            Assert.Equal("Kunden kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggetInnFeilModel()
        {
            // Arrange
            // Kunden er indikert feil med tomt fornavn her.
            // det har ikke noe å si, det er introduksjonen med ModelError under som tvinger frem feilen
            // kunnde også her brukt It.IsAny<Kunde>
            var kunde1 = new Kunde {Id = 1,Fornavn = "",Etternavn = "Hansen",Adresse = "Askerveien 82",
                                    Postnr = "1370",Poststed = "Asker"};

            //kunnde brukt It.IsAny<Kunde> her hvor det står kunde1 og det samme lenger ned. Dette
            //fordi vi bare skal sjekke badRequest og feil inputvalidering. 
            mockRep.Setup(k => k.Lagre(kunde1)).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Det vi gjør da er å legge til ModelState.AddModelError for å tvinge at modellen er feil
            kundeController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.Lagre(kunde1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagreIkkeLoggetInn()
        {
            //samme som før her og return true, men vi tar ikkeLoggetInn og da vil vi få
            //UnauthorizedObjectResult tilbake, hvis vi kaller den med en fake kunde
            mockRep.Setup(k => k.Lagre(It.IsAny<Kunde>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act, kaller med en fake kunde.
            var resultat = await kundeController.Lagre(It.IsAny<Kunde>()) as UnauthorizedObjectResult;

            // Assert, sjekker på Unauthorized og "Ikke logget inn". Det er da det samme for slett og hentEn
            //og endreLoggetInn og endreIkkeOK (altså feil fra db) og endreLoggetInnFeilModel og endreIkkeLoggetInn (under)
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.Slett(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunde slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(false);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.Slett(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av Kunden ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SletteIkkeLoggetInn()
        {
            mockRep.Setup(k => k.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.Slett(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentEnLoggetInnOK()
        {
            // Arrange
            var kunde1 = new Kunde
            {
                Id = 1,
                Fornavn = "Per",
                Etternavn = "Hansen",
                Adresse = "Askerveien 82",
                Postnr = "1370",
                Poststed = "Asker"
            };

            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(kunde1);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.HentEn(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Kunde>(kunde1,(Kunde)resultat.Value);
        }

        [Fact]
        public async Task HentEnLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(()=>null); // merk denne null setting!

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.HentEn(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke kunden", resultat.Value);
        }

        [Fact]
        public async Task HentEnIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(()=>null);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.HentEn(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.Endre(It.IsAny<Kunde>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.Endre(It.IsAny<Kunde>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunde endret", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.Lagre(It.IsAny<Kunde>())).ReturnsAsync(false);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.Endre(It.IsAny<Kunde>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av kunden kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnFeilModel()
        {
            // Arrange
            // Kunden er indikert feil med tomt fornavn her.
            // det har ikke noe å si, det er introduksjonen med ModelError under som tvinger frem feilen
            // kunnde også her brukt It.IsAny<Kunde>
            var kunde1 = new Kunde
            {
                Id = 1,
                Fornavn = "",
                Etternavn = "Hansen",
                Adresse = "Askerveien 82",
                Postnr = "1370",
                Poststed = "Asker"
            };

            mockRep.Setup(k => k.Endre(kunde1)).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            kundeController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.Endre(kunde1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreIkkeLoggetInn()
        {
            mockRep.Setup(k => k.Endre(It.IsAny<Kunde>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.Endre(It.IsAny<Kunde>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LoggInnOK()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert, denne skal returnere status OK, og en boolsk variabel, hvis jeg setter den
            //til _loggetInn
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            //hvis LoggetInn med feil brukernavn eller passord, så blir den false, den returnerer
            //den loggetInn fra repository
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //setter _ikkeLoggetInn
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act, vi får OK tilbake, 
            var resultat = await kundeController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            //MEN vi får resultatverdien som false, og ettersom den er boolsk, så må vi caste den, ettersom den ikke er en streng. 
            Assert.False((bool)resultat.Value);
        }

        [Fact] 
        public async Task LoggInnInputFeil() //altså at det er en feil i modellen. 
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //modellfeil
            kundeController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            //setter logget inn og får BadRequest tilbake, sjekker mot badRequest og får feil i inputvalidering
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        //denne er mye enklere, den er ikke AsyncTask, den er bare void, derfor tar vi public void LoggetUt
        [Fact]
        public void LoggUt()
        {
            //mocker kundeController
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //setter sesssion ikke logget inn
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            //og objektet til context-en
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;
         
            // Act, kaller logget ut
            kundeController.LoggUt();

            // Assert, da skal vi asserte at ikke logget inn, med mockSession logget inn
            //altså, at ikke logget inn (det er strengen) med mockSession sin nøkkel loggetInn
            //vi sjekker altså hvilken nøkkel/streng som mockSession har nå med den nøkkelen her (_loggetInn), og den skal ha "ikke logget inn".
           Assert.Equal(_ikkeLoggetInn,mockSession[_loggetInn]);
        }
    }
}

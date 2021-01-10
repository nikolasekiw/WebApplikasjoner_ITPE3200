$(function () {
    hentAlleKunder();
});

function hentAlleKunder() {
    $.get("home/index", function (kunder) {
        skrivUt(kunder);
    });
}

//Formaterte dette i document.write istedenfor å formatere dette i en tabell og legge ut en div
function skrivUt(kunder) {
    for (let kunde of kunder) {
        document.write("Kundenavn: " + kunde.navn + "<br>"); //hvis vi hadde hatt flere kunder så hadde flere kommet ut 
        for (let ordre of kunde.ordre) { //kunde.ordre er en liste av ordre, så da kan vi loope gjennom den med en ordre
            document.write("Ordre-dato: " + ordre.dato + "<br>"); //ordre.ordrelinjer er også en liste
            for (let ordreLinje of ordre.ordreLinjer) { 
                document.write("Ordrelinje-antall: " + ordreLinje.antall + "<br>");
                document.write("Vare-navn: " + ordreLinje.vare.navn + "<br>");   //det er en-til-en relasjon mellom ordrelinje og vare, derfor kan vi bare skrive ordrelinje.vare.navn
            }
        }
    }
}
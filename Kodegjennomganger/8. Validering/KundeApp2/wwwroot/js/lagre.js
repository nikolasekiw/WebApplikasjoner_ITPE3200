function validerOgLagreKunde() {
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const adresseOK = validerAdresse($("#adresse").val());
    const postnrOK = validerPostnr($("#postnr").val());
    const poststedOK = validerPoststed($("#poststed").val());
    if (fornavnOK && etternavnOK && adresseOK && postnrOK && poststedOK) {
        lagreKunde();
    }
}
/**
 * Metoden over vil si at før vi kjører lagreKunde så forsikrer vi oss om at alt er i orden.
 * Så lenge all valideringen på alle attributtene er ok, så lagrer jeg kunden ved å kalle på
 * lagreKunde() metoden.
 *
 * I lagre.html er det lagt til onChange på all input fra bruker, som da kaller på de metodene som
 * er i validering.js. Det er også lagt til en span som feilmeldingen skal komme i om input viser
 * seg å være feil/stride med regex som er satt inn.
 *
 * I det bruker skriver inn verdier i input og noe er feil så får man opp feilmelding direkte fra
 * validering.js, men om har skrevet inn alt og skal trykke på lagre-knappen så er det da
 * validerOgLagreKunde() slår inn og dobbeltsjekker alt alt er riktig. Det er en onClick på
 * validerOgLagreKunde() på knappen i lagre.html som da kaller denne metoden. 
*/

function lagreKunde() {
    const kunde = {
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        adresse: $("#adresse").val(),
        postnr: $("#postnr").val(),
        poststed: $("#poststed").val()
    }
    const url = "Kunde/Lagre";
    $.post(url, kunde, function () {
        window.location.href = 'index.html';
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere");
    });
};
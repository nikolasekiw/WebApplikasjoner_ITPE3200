function lagreKunde() {
    const kunde = {
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        adresse: $("#adresse").val(),
        postnr: $("#postnr").val(),
        poststed: $("#poststed").val()
    }
    const url = "Kunde/Lagre";
    $.post(url, kunde, function () { //hvis returnerer HTTP status OK så går den videre, hvis ikke hopper den over til .fail
        window.location.href = 'index.html';
    })
        //kunne ha hentet ut feilen som parameter i function også.
    .fail(function () { 
        $("#feil").html("Feil på server - prøv igjen senere");
    });
};

/**
 * .fail er generelt når man returnere noe feil, f.eks. BadRequest
**/
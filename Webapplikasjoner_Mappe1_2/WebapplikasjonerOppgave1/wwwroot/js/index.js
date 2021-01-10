$(function () {
    HentAlleStasjoner();
});

function HentAlleStasjoner() {
    $.get("bestilling/hentAlleStasjoner", function (stasjoner) {
        if (stasjoner) {
            listStartStasjoner(stasjoner);
        } else {
            $("#feil").html("Feil i db");
        }
    });
}

function listStartStasjoner(stasjoner) {
    let ut = "<select class='browser-default custom-select' onchange='listEndeStasjoner()' id='startstasjon'>";
    ut += "<option>Velg startstasjon</option>";
    for (let stasjon of stasjoner) {
        ut += "<option>" + stasjon.stasjonsNavn + "</option>";
    }
    ut += "</select>";
    $("#startstasjon").html(ut);
    console.log(JSON.stringify(stasjoner));
}

function listEndeStasjoner() {
    let startstasjon = $('#startstasjon option:selected').val();
    const url = "bestilling/hentEndeStasjoner?startStasjonsNavn=" + startstasjon;
    $.get(url, function (stasjoner) {
        if (stasjoner) {

            const uniq = new Set(stasjoner.map(e => JSON.stringify(e)));

            const unikeStasjoner = Array.from(uniq).map(e => JSON.parse(e));

            let ut = "<label>Jeg skal reise til</label>";
            ut += "<select class='browser-default custom-select' onchange='listDato()'>";
            ut += "<option>Velg endestasjon</option>";

            for (let stasjon of unikeStasjoner) {
                ut += "<option>" + stasjon.stasjonsNavn + "</option>";
            }

            ut += "</select>";
            $("#endestasjon").html(ut);
        } else {
            $("#feil").html("Feil i db");
        }
    });
}

function listDato() {
    let ut = "<label>Velg dato<span> (DD/MM/ÅÅÅÅ) </span></label>";
    ut += "<input class='form-control' type='text' id='datoValgt' onchange='listTidspunkt(), validerDato(this.value)'>";
    $("#dato").html(ut);
}

function listTidspunkt() {
    let dato = $('#datoValgt').val();
    let startstasjon = $('#startstasjon option:selected').val();
    let endestasjon = $('#endestasjon option:selected').val();
    const url = "bestilling/hentAlleTurer";
    $.get(url, function (turer) {
        if (turer) {
            let ut = "<label>Velg tidspunkt</label>";
            ut += "<select class='browser-default custom-select' id='tidspunkt'>";
            for (let tur of turer) {
                if (startstasjon === tur.startStasjon.stasjonsNavn && endestasjon === tur.endeStasjon.stasjonsNavn && dato === tur.dato) {
                    ut += "<option>" + tur.tid + "</option>";
                }
            }
            ut += "</select>";
            $("#tid").html(ut);
            if (document.getElementById('tidspunkt').options.length == 0) {
                $("#ikkeTurDato").html("Ingen tilgjengelige turer på valgt dato");
            }
            else {
                $("#ikkeTurDato").html("");
            }
        }
        else {
            $("#feil").html("Feil i db");
        }
    });
}

function beregnPris() {
    let dato = $('#datoValgt').val();
    let startstasjon = $('#startstasjon option:selected').val();
    let endestasjon = $('#endestasjon option:selected').val();
    let tid = $('#tid option:selected').val();
    let antallBarn = $("#antallBarn").val();
    let antallVoksne = $("#antallVoksne").val();

    let pris;
    let barnepris = 0;
    let voksenpris = 0;

    const url = "bestilling/hentAlleTurer";
    $.get(url, function (turer) {
        if (turer) {
            for (let tur of turer) {
                if (startstasjon === tur.startStasjon.stasjonsNavn && endestasjon === tur.endeStasjon.stasjonsNavn && dato === tur.dato && tid == tur.tid) {
                    barnepris = tur.barnePris;
                    voksenpris = tur.voksenPris;
                }
            }
            if (antallBarn > 0 && antallVoksne > 0) {
                pris = (barnepris * antallBarn) + (voksenpris * antallVoksne);
            }
            else if (antallBarn <= 0 && antallVoksne > 0) {
                pris = voksenpris * antallVoksne;
            }
            else if (antallVoksne <= 0 && antallBarn > 0) {
                pris = barnepris * antallBarn;
            }
            else {
                pris = 0;
            }
            if (antallBarn > 0 && antallBarn < 10) {
                $("#prisBarn").html("Pris barn: " + barnepris + " kr x " + antallBarn + " = " + barnepris * antallBarn + " kr");
            }
            else {
                $("#prisBarn").html("");
            }
            if (antallVoksne > 0 && antallVoksne < 10) {
                $("#prisVoksen").html("Pris voksen: " + voksenpris + " kr x " + antallVoksne + " = " + voksenpris * antallVoksne + " kr");
            }
            else {
                $("#prisVoksen").html("");
            }
        }
        else {
            $("#feil").html("Feil i db");
        }
    });
}

function beregnOgValiderBarn() {
    let antallBarn = $("#antallBarn").val();
    validerAntallBarn(antallBarn);
    beregnPris();
}

function beregnOgValiderVoksen() {
    let antallVoksne = $("#antallVoksne").val();
    validerAntallVoksne(antallVoksne);
    beregnPris();
}

function validerOgLagBestilling() {
    const StartstasjonOK = validerStartstasjon($("#startstasjon").val());
    const EndestasjonOK = validerEndestasjon($("#endestasjon").val());
    const TidOK = validerTid($("#tid").val());
    const FornavnOK = validerFornavn($("#fornavn").val());
    const EtternavnOK = validerEtternavn($("#etternavn").val());
    const TelefonnummerOK = validerTelefonnummer($("#telefonnr").val());
    const EpostOK = validerEpost($("#epost").val());
    const KortnummerOK = validerKortnummer($("#kortnummer").val());
    const AntallBarnOK = validerAntallBarn($("#antallBarn").val());
    const AntallVoksneOK = validerAntallVoksne($("#antallVoksne").val());
    if (StartstasjonOK && EndestasjonOK && TidOK && FornavnOK && EtternavnOK
        && TelefonnummerOK && EpostOK && KortnummerOK && AntallBarnOK && AntallVoksneOK) {
        lagMinEgenPopUp();
    }
}

function lagMinEgenPopUp() {
    const options = { show: true };
    $('#myModal').modal('show')
    formaterBestilling();
}

function formaterBestilling() {
    let dato = $('#datoValgt').val();
    let startstasjon = $('#startstasjon option:selected').val();
    let endestasjon = $('#endestasjon option:selected').val();
    let tid = $('#tid option:selected').val();
    let antallBarn = $("#antallBarn").val();
    let antallVoksne = $("#antallVoksne").val();

    let pris;
    let barnepris = 0;
    let voksenpris = 0;

    const url = "bestilling/hentAlleTurer";
    $.get(url, function (turer) {
        if (turer) {
            for (let tur of turer) {
                if (startstasjon === tur.startStasjon.stasjonsNavn && endestasjon === tur.endeStasjon.stasjonsNavn && dato === tur.dato && tid == tur.tid) {
                    console.log("tur.barnepris: " + tur.barnePris + ", antallBarn: " + antallBarn + ", tur.voksenPris: " + tur.voksenPris + ", antallVoksne: " + antallVoksne);
                    barnepris = tur.barnePris;
                    voksenpris = tur.voksenPris;
                }
            }
            if (antallBarn > 0 && antallVoksne > 0) {
                pris = (barnepris * antallBarn) + (voksenpris * antallVoksne);
            }
            else if (antallBarn <= 0 && antallVoksne > 0) {
                pris = voksenpris * antallVoksne;
            }
            else if (antallVoksne <= 0 && antallBarn > 0) {
                pris = barnepris * antallBarn;
            }
            else {
                pris = 0;
            }
        }

        let ut = "<table class='table table-striped'><tr>" +
            "<tr>Fornavn : </tr>" + $("#fornavn").val() + "<br>" +
            "<tr>Etternav : </tr>" + $("#etternavn").val() + "<br>" +
            "<tr>Telefonnummer : </tr>" + $("#telefonnr").val() + "<br>" +
            "<tr>Epost : </tr>" + $("#epost").val() + "<br>" +
            "<tr>Kortnummer : </tr>" + $("#kortnummer").val() + "<br>" +
            "<tr><br>" +
            "<tr>Antall barn : </tr>" + $("#antallBarn").val() + "<br>" +
            "<tr>Antall voksne : </tr>" + $("#antallVoksne").val() + "<br>" +
            "<tr>Totalpris : </tr>" + pris + "<br>" +
            "<tr>Startstasjon : </tr>" + $("#startstasjon option:selected").val() + "<br>" +
            "<tr>Endestasjon : </tr>" + $("#endestasjon option:selected").val() + "<br>" +
            "<tr>Dato : </tr>" + $("#datoValgt").val() + "<br>" +
            "<tr>Tid : </tr>" + $("#tid option:selected").val() +
            "</tr>";
        ut += "</table>";
        $("#innhold").html(ut);
    });
}

function lagreBestilling() {
    const bestilling = {
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        telefonnummer: $("#telefonnr").val(),
        epost: $("#epost").val(),
        kortnummer: $("#kortnummer").val(),
        antallBarn: $("#antallBarn").val(),
        antallVoksne: $("#antallVoksne").val(),
        startStasjon: $("#startstasjon option:selected").val(),
        endeStasjon: $("#endestasjon option:selected").val(),
        dato: $("#datoValgt").val(),
        tid: $("#tid option:selected").val()
    }

    const url = "bestilling/lagre";
    $.post(url, bestilling, function () {
        window.location.href = 'bekreft.html';
    })
        .fail(function () {
            $("#feil").html("Feil på server - prøv igjen senere");
        });
}
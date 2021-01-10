import {Component, OnInit} from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import {Kunde} from "./Kunde"; //importerer kunde

@Component({
    selector: "app-root", //det er denne komponenten som skal erstatte app-root taggen i index.html
    templateUrl: "SPA.html"
})
export class SPA { //klassen til komponenten

  /**
   * Vi bruker registrer skjema og endre skjema, det er to forskjellige skjemaer. Men de er implementert likt.
   * Dvs. det er noen felt i de skjemaene som er forskjellig og derfor så har jeg en boolsk variabel som jeg setter
   * i de ulike funksjonene nedover for å indikere om dette er et endringsskjema eller et registreringsskjema.
   * Så hvis det er et registreringsskjema så setter jeg det til true, hvis det er et endringsskjema så visSkjemaRegistrere
   * til valse.
   *
   * HTML i SPA.html blir delt i to, en liste-del og en skjema-del. Om vi skal vise skjema eller om vi skal vise listen
   * det er da avhengig av hva status på visListe: boolean er. Hvis den er true så vil man vise listen, og hvis den er
   * false så vil man vise et av de registreringsskjemaene eller endringsskjema.
   */
    visSkjemaRegistrere: boolean; //registreringsskjema
    visListe: boolean; //endringsskjema
    alleKunder: Array<Kunde>; //array av kunde, det populerer vi når vi kaller en metode på server, returnerer alle kunder fra db, så legger det i array kunde
    skjema: FormGroup; //skjema er en type formgroup
    laster: boolean; //boolsk variabel som vi setter før vi henter alle kunder og resetter etterpå gitt at det tar litt tid å hente alle kundene

  /**
   * Valideringsobjekt. Lager objekt her istedenfor å gjøre all valideringen inne i konstruktøren. Vi sjekker mot
   * patterns (regex) på fornavn, etternavn osv.
   */
    validering = {
      id: [""],
      fornavn: [
        null,Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
      ],
      etternavn: [
        null,Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
      ],
      adresse: [
        null,Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
      ],
      postnr: [
        null,Validators.compose([Validators.required, Validators.pattern("[0-9]{4}")])
      ],
      poststed: [
        null,Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
      ]
    }

  /**
   * Har en konstruktør hvor vi må injecte HttpClient og en FormBuilder for å kunne ta valideringen.
   */
    constructor(private _http: HttpClient, private fb: FormBuilder) {
        this.skjema = fb.group(this.validering); //her skjer valideringen, bruker objektet som vi satt over
    }


  //OnInit, dvs. at når vi kaller denne komponenten så skal vi setter laster = true, hente alle kunder og vise listen.
  ngOnInit() {
        this.laster = true;
        this.hentAlleKunder();
        this.visListe = true;
    }

  /**
   * Vi tar get og forventer at vi får ut et kunde-array tilbake. Vi tar en get til api/kunde/
   * Subscriber og returnerer alle kundene. Setter kundene til this.alleKunder-arrayet og setter laster = false
   */
  hentAlleKunder() {
        this._http.get<Kunde[]>("api/kunde/")
            .subscribe(kundene => {
                this.alleKunder = kundene;
                this.laster = false;
            },
            error => console.log(error),
            () => console.log("ferdig get-api/kunde")
        );
    };

  /**
   * På dette skjema, så hvis this.visSkjemaRegistrere så lagrer vi kunden, hvis ikke så endrer vi kunden.
   * Her skal vi altså enten lagre kunde eller endreKunde.
   */
    vedSubmit() {
        if (this.visSkjemaRegistrere) {
            this.lagreKunde();
        }
        else {
            this.endreEnKunde();
        }
    }

  //Denne kalles på når vi trykker på den blå knappen i lista.
  registrerKunde() {
        // må resette verdiene i skjema dersom skjema har blitt brukt til endringer
        this.skjema.setValue({
            id: "",
            fornavn: "",
            etternavn: "",
            adresse: "",
            postnr: "",
            poststed:""
        });
        this.skjema.markAsPristine(); //det er en måte å sette skjema som valid, at jeg ikke får feilmeldinger.
        this.visListe = false; //viser lista her
        this.visSkjemaRegistrere = true; //så visSkjemaRegistrer
    //Så når vi trykker på den blå knappen så setter vi at vi ikke skal vise lista, men vise registreringsskjema.
    }

    tilbakeTilListe() {
        this.visListe = true;
    }

  lagreKunde() {
    const lagretKunde = new Kunde(); //oppretter ny kunde

    lagretKunde.fornavn = this.skjema.value.fornavn; //setter inn verdiene
    lagretKunde.etternavn = this.skjema.value.etternavn;
    lagretKunde.adresse = this.skjema.value.adresse;
    lagretKunde.postnr = this.skjema.value.postnr;
    lagretKunde.poststed = this.skjema.value.poststed;

    this._http.post("api/kunde", lagretKunde) //tar en post og sender med kunden (objektet over)
        .subscribe(retur=> { //i returen så henter vi alle kundene igjen for å oppdatere lista
            this.hentAlleKunder();
            this.visSkjemaRegistrere = false; //tar false på denne fordi da er vi ferdig med å registrere, trenger egt. ikke denne linjen
            this.visListe = true; //true på denne fordi vi skal vise lista over alle registrerte kunder.
          },
        error => console.log(error)
        );
    };

    sletteKunde(id: number) {
    this._http.delete("api/kunde/" + id) //bruker verbet delete med kunde og id-en til kunden.
        .subscribe(retur => { //på return så henter vi bare alle kundene igjen
            this.hentAlleKunder();
        },
        error => console.log(error)
      );
    };

    //denne endreKunde er når vi trykker på endre ved siden av slett i tabellen.
    endreKunde(id: number) {
        this._http.get<Kunde>("api/kunde/"+id) //tar get for å hente kunden
          .subscribe(
            kunde => { //her kommer det en kunde tilbake. Dette for å vise verdiene til kunden
                this.skjema.patchValue({ id: kunde.id }); //så patcher vi kunden i skjema, en måte å sette verdier i skjema på i ts-filen
                this.skjema.patchValue({ fornavn: kunde.fornavn });
                this.skjema.patchValue({ etternavn: kunde.etternavn });
                this.skjema.patchValue({ adresse: kunde.adresse });
                this.skjema.patchValue({ postnr: kunde.postnr });
                this.skjema.patchValue({ poststed: kunde.poststed });
            },
          error => console.log(error)
      );
      this.visSkjemaRegistrere = false; //tar false på denne fordi det er endingsskjema vi skal vise
      this.visListe = false; //false her også, altså det skjema vi skal vise
      //Når vi tar on Submit på det skjema (vedSubmit), så hvis this.visSkjemaRegistrer er false, så er det this.endreEnKunde() vi skal kalle
    }

    endreEnKunde() {
        const endretKunde = new Kunde(); //tar new på kunde
        endretKunde.id = this.skjema.value.id; //henter ut verdien fra skjema og setter det i endretKunde
        endretKunde.fornavn = this.skjema.value.fornavn;
        endretKunde.etternavn = this.skjema.value.etternavn;
        endretKunde.adresse = this.skjema.value.adresse;
        endretKunde.postnr = this.skjema.value.postnr;
        endretKunde.poststed = this.skjema.value.poststed;

        this._http.put("api/kunde/", endretKunde) //tar en put, gjør det når vi skal endre noe i REST, med endretKunde som input
            .subscribe(
              retur => { //ved retur så henter vi alle kundene og viser listen.
                  this.hentAlleKunder();
                  this.visListe = true;
              },
              error => console.log(error)
        );
    }
}

import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Kunde } from "../Kunde";

@Component({
  templateUrl: "endre.html"
})
export class Endre {
  skjema: FormGroup;

  validering = {
    id: [""],
    fornavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    etternavn: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    adresse: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ],
    postnr: [
      null, Validators.compose([Validators.required, Validators.pattern("[0-9]{4}")])
    ],
    poststed: [
      null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])
    ]
  }

  constructor(private http: HttpClient, private fb: FormBuilder,
              private route: ActivatedRoute, private router: Router) { //må ha med private router: Router igjen
      this.skjema = fb.group(this.validering);
  }

  //Dette er litt spesielt. this.route.params.subscribe params, altså parametre, som er lagt på kallet til
  //denne komponenten og det er denne id-en og da kan vi hente ut den, kalle this.endreKunde(params.id)
  //endreKunde henter kunden som skal vises i det skjema og da kaller vi den med params.id. og det er id-en som
  //vi hadde på endre-knappen i html-filen.
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.endreKunde(params.id);
    })
  }

  vedSubmit() { //når vi trykker på submit-knappen i html-en så kalles endreEnKunde
      this.endreEnKunde();
  }

  endreKunde(id: number) { //så kaller vi endreKunde med id. Henter ut kunden og legger den i skjema.
    this.http.get<Kunde>("api/kunde/" + id)
      .subscribe(
        kunde => {
          this.skjema.patchValue({ id: kunde.id });
          this.skjema.patchValue({ fornavn: kunde.fornavn });
          this.skjema.patchValue({ etternavn: kunde.etternavn });
          this.skjema.patchValue({ adresse: kunde.adresse });
          this.skjema.patchValue({ postnr: kunde.postnr });
          this.skjema.patchValue({ poststed: kunde.poststed });
        },
        error => console.log(error)
      );
  }

  //det denne gjør er at den legger over input-verdiene i en ny kunde
  endreEnKunde() {
    const endretKunde = new Kunde();
    endretKunde.id = this.skjema.value.id;
    endretKunde.fornavn = this.skjema.value.fornavn;
    endretKunde.etternavn = this.skjema.value.etternavn;
    endretKunde.adresse = this.skjema.value.adresse;
    endretKunde.postnr = this.skjema.value.postnr;
    endretKunde.poststed = this.skjema.value.poststed;

    //kaller put og når den returnerer, this.router.navigate til liste.
    this.http.put("api/kunde/", endretKunde)
      .subscribe(
        retur => {
          this.router.navigate(['/liste']);
        },
        error => console.log(error)
      );
  }
}

import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
/**
 * Må inportere FormGroup, FormControl, Validators og FormBuilder fra forms
 */


@Component({
  selector: "app-root", //denne ligger i index og inneholder ikke noe 
  templateUrl: "app.component.html" //denne har vi sett på  
})
export class AppComponent { //så har vi app component her

  Skjema: FormGroup; //Skjema er av typen FormGroup, det er da den variabelen som tilsvarer "skjema" i app.component.html

  /**
   * Så har vi en konstruktør hvor vi initierer fb (kall det hva du vil), og den er av type
   * FormBuilde. Og da skal vi lage valideringsregler inne i den konstruktøren.
   *
   * this.Skjema = fb.group({ --> denne tar inn en parameter og det er et JS objekt/typescipt objekt
   *
   * Starter med brukernavn, variabelnavnet, også tar den et array. Første parameter er blank.
   * Vi kunne skrive noe inn her, men da ville vi få en ledetekst. Så kommer Validators.required,
   * det var bare det at man skal skrive et eller annet tegn.
   *
   * Hvis vi vil ha regex, så gjør vi det på samme måte. Vi har variabelen som er passord, det
   * er da tilsvarende navnene formControlName="passord" i app.component.html.
   * Passord og igjen et array med tom streng som første innslag og Validators.pattern og regex.
   *
   * Så onSubmit, det er den funksjonen som er øverst i app.component.html --> (ngSubmit)=onSubmit()
   * Så skriver jeg ut noe på konsolloggen. Skriver skjema, da får jeg hele formgroup.
   */
  constructor(private fb: FormBuilder) {
    this.Skjema = fb.group({ 
      brukernavn: ["", Validators.required], 
      passord: ["", Validators.pattern("[0-9]{6,15}")]
    });
  }

  onSubmit() {
    console.log("Modellbasert skjema submitted:");
    console.log(this.Skjema);
    console.log(this.Skjema.value.brukernavn); //dette er for å få ut brukernavnet  
    console.log(this.Skjema.touched); //dette er bare det at jeg har skrevete noe inn i skjema. En av mange parametre som man kan få ut informasjon fra, fra skjem/formgroup
    //Dette er en måte å få ut det som er skervet inn i brukernavnet. 
  }
}


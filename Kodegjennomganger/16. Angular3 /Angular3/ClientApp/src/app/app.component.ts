import { Component } from '@angular/core';
import { kontakt } from "./kontakt";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  navn: string;
  telefon: string;

  kontakter: Array<kontakt> = []; // opprett et tomt array
  /**
   * Kontakter array hvor jeg skal legge inn kontaktene, det er et array av
   * type kontakt også inisialiserer jeg det til et blankt array med en gang.
  **/

  /**
   * Funksjonen leggTilKontakt returnerer void. Tar new på kontakt med navn og telefonnr.
   * Når vi har skrevet noe inn i de, da kommer de verdiene, da kommer de verdiene inn i
   * navn og telefonnr.
   *
   * Det er en kontakt som blir opprettet, et objekt. Det blir så pushet inn i kontakter, så
   * resetter jeg navn og tlf slik at de blir nullt ut (går i app.component.html)
  **/
  leggTilKontakt(): void {
    var enKontakt = new kontakt(this.navn, this.telefon);
    this.kontakter.push(enKontakt);
    this.navn = "";
    this.telefon = "";
  }

  /**
   * Denne tar en kontakt inn av type kontakt, returnerer void.
   * indeks: jeg må finne hvor kontakten ligger i arrayet. Da får jeg indeksen til den posisjonen
   * som det objektet har i arrayet. Da er det en JS/typescript funksjon som heter splice.
   * this.kontakter.splic fra indeksen og ett objekt skal slettes.
   * Gå nå inn i app.module.ts
   */
  slettKontakt(enKontakt: kontakt): void {
    var indeks = this.kontakter.indexOf(enKontakt);
    this.kontakter.splice(indeks, 1);
  }
}

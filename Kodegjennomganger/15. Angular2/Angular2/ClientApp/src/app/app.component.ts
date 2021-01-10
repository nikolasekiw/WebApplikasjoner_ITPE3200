import { Component,OnInit } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  public navn: string;
  /**
   * Dette her er typescript og da må vi definere typen etter kolon istedenfor mellom i de fleste andre språk.
   * Dette er inne nødvendig, men da vil den få den verdien.
   *
   * Så kan vi lage en konstruktør som setter den. Når vi gjør det på denne måten så vil vi få Ole Hansen i input-boksen
   * og i selve output som standard nå nettsiden kjører.
   *
   * Det er vanlig å ikke bruke konstruktøren til dette når jeg gjør noe applikasjonsmessig. Vi skal bruke denne konstruktøren
   * til å initiere HTTP-kall osv. Derfor så er det vanlig å importere en ny komponent - OnInit (øverst der).
   * Dette er fra core også, slik at vi kan skrive den med komma i mellom og slipper å ha det i en egen linje.
   * Det er da en sånn som oppfører seg akkurat som en konstruktør, men som det er meningen at applikasjonsprogrammereren skal bruke
   * istedenfor konstruktøren.
  */

  ngOnInit() { //det sto konstruktør her før
    this.navn = "Ole Hansen";
  }
}

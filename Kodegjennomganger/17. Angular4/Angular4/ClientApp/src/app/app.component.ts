import { Component } from '@angular/core';
import { kunde } from "./kunde"; //komponenten må importer kunden
import { HttpClient } from '@angular/common/http'; //må også importere http client

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  public alleKunder: Array<kunde>; //å skrive public her er smak og behag
  public laster: string;

  /**
   * Så er det en konstruktør og den bruker vi å injecte HTTP client med.
   * Da skriver vi private _http: HttpClient for å tilgjengeliggjøre variabelen/objektet _http,
   * inne i komponenten. 
   */
  constructor(private _http: HttpClient) { }

  /**
   * this._http.get<kunde[]>("api/Kunde/") --> get kallet skal returnere en type
   * kunde-array. Kaller api/Kunde/. Vi kan også skrive "hent" etter "/", men det mest
   * korrekte REST messig, er å bare skrive get og forvente at man da har en get-metode
   * på Kunde som returnerer et array av kunder.
   *
   * Så .subscibe( data => { ... }) --> this.alleKunder = data. Da setter jeg alt det som
   * kommer fra server inn i alle kunder. Vi kan gjøre det fordi arrayet (lenger opp i koden)
   * er av type kunde og at vi spesifiserer i get-kallet hvilken type som skal returneres.
   * Da kan vi sette det som kommer fra server over i det arrayet som ligger på klienten.
   * this.laster blanker vi ut når kallet er kommet tilbake. Hvis feil, så får
   * jeg alert-boks med error og konsoll-loggen rapporterer at vi er ferdig med det get-kallet.
   * Med en gang kallet fra api/Kunde/ får data, så vil alle kunde få de dataene og da vil de
   * endres (rendres?) med en gang vha. for-løkka i app.component.html. Det er det som skal til
   * for å kommunisere med server.
   * 
  **/
  hentAlleKunder() {
    this.laster = "Vennligst vent"; //skriver ut denne teksten bak knappen, hvis kallet tar tid
    this._http.get<kunde[]>("api/Kunde/")
        .subscribe( data => {
            this.alleKunder = data;
            this.laster = "";
          },
          error => alert(error),
          () => console.log("ferdig get-/kunde")
        );
    }
}

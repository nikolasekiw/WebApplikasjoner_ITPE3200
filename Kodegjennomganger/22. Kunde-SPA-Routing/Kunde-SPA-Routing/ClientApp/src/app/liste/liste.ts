import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router'; //importerer router
import { Kunde } from "../Kunde";

@Component({
  templateUrl: "liste.html"
})
export class Liste {
  alleKunder: Array<Kunde>;
  laster: boolean;

  //her må vi initiere denne ruteren. Og dette er kun fordi vi skal bruke denne routingen i slettKunde
  constructor(private http: HttpClient,private router: Router) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleKunder();
  }

  //Dette er akkurat det samme som før
  hentAlleKunder() {
    this.http.get<Kunde[]>("api/kunde/")
      .subscribe(kundene => {
        this.alleKunder = kundene;
        this.laster = false;
      },
       error => console.log(error)
      );
  };

  /**
   * Denne er litt spesiell. Vi kaller delete og når den returneres så hentAlleKunder igjen og router.navigate
   * med vanlige parenteser og firkantparenteser, så liste inni.
   */
  sletteKunde(id: number) {
    this.http.delete("api/kunde/" + id)
      .subscribe(retur => {
        this.hentAlleKunder();
        this.router.navigate(['/liste']);
      },
       error => console.log(error)
      );
  };
}

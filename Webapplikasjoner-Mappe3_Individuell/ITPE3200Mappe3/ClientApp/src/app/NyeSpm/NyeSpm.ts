import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import {Kundespm} from "../Kundespm";

@Component({
  templateUrl: "NyeSpm.html"
})

export class NyeSpm {
  alleKundeSpm: Array<Kundespm>;
  laster: boolean;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleKundeSpm();
  }

  hentAlleKundeSpm(){
    this.http.get<Kundespm[]>("api/kundeService/hentKundeSpm/")
      .subscribe(spm => {
          this.alleKundeSpm = spm;
          this.laster = false;
          console.log(spm)
        },
        error => console.log(error)
      );
  }
}

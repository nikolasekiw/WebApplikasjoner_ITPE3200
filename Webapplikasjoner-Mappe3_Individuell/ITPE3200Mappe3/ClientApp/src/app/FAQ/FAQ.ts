import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import {Faq} from "../Faq";

@Component({
  templateUrl: "FAQ.html"
})

export class FAQ {
  alleFAQ: Array<Faq>;
  laster: boolean;
  buttons = Array(15).fill(false);

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.laster = true;
    this.hentAlleFAQ();
  }

  hentAlleFAQ(){
    this.http.get<Faq[]>("api/kundeService/hentAlleFAQ")
      .subscribe(data => {
        this.alleFAQ = data;
        this.laster = false;
        },
        error => console.log(error)
      );
  }

  alleKat(kategori: string){
    this.http.get<Faq[]>("api/kundeService/hentAlleKat/"+ kategori)
      .subscribe(data => {
          this.alleFAQ = data;
          this.laster = false;
        },
        error => console.log(error)
      );
  }

  TommelOpp(id: number){
      let rating = this.alleFAQ.find(f => f.id === id);
      if (rating != null) {
        rating.tommelOpp++;
      }
    this.http.post("api/rating/tommelopp", id)
        .subscribe(retur => {},
          error => console.log(error)
        );
    }

  TommelNed(id: number){
      let rating = this.alleFAQ.find(f => f.id === id);
      if (rating != null) {
        rating.tommelNed++;
      }
      this.http.post("api/rating/tommelned", id)
        .subscribe(retur => {},
          error => console.log(error)
        );
    }
}
